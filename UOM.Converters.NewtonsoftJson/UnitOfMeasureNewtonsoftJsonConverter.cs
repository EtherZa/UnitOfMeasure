namespace UOM.Converters
{
    using System;
    using System.Collections.Concurrent;
    using System.Reflection;
    using Newtonsoft.Json;

    public class UnitOfMeasureNewtonsoftJsonConverter : JsonConverter
    {
        private static readonly ConcurrentDictionary<Type, (bool Nullable, Func<string, object> factory)> _lookup;

        static UnitOfMeasureNewtonsoftJsonConverter()
        {
            _lookup = new ConcurrentDictionary<Type, (bool Nullable, Func<string, object> factory)>();
        }

        public override bool CanConvert(Type objectType)
        {
            objectType = Nullable.GetUnderlyingType(objectType) ?? objectType;

            return objectType.IsGenericType
                && objectType.GetGenericTypeDefinition() == typeof(Quantity<>);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var (isNullable, factory) = _lookup.GetOrAdd(objectType, type =>
            {
                var underlyingQuantityType = Nullable.GetUnderlyingType(objectType);
                var isNullable = underlyingQuantityType != null;
                underlyingQuantityType ??= objectType;
                var method = underlyingQuantityType.GetMethod(nameof(Quantity<Unit>.Parse));

                return (isNullable, (string value) => method.Invoke(null, new[] { value, null }));
            });

            if (reader.TokenType == JsonToken.Null)
            {
                if (isNullable)
                {
                    return null;
                }

                throw new FormatException("Parsed value is null, but target is non-nullable");
            }

            if (reader.TokenType != JsonToken.String)
            {
                throw new FormatException($"Unable to parse value of type '{reader.TokenType}'");
            }

            var value = (string)reader.Value;
            if (string.IsNullOrWhiteSpace(value))
            {
                if (isNullable)
                {
                    return null;
                }

                throw new FormatException("Parsed value is empty, but target is non-nullable");
            }

            try
            {
                return factory(value);
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            writer.WriteValue(value.ToString());
        }
    }
}
