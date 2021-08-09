namespace UOM.Converters
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class UnitOfMeasureJsonConverterFactory : JsonConverterFactory
    {
        private readonly ConcurrentDictionary<Type, JsonConverter> _converters;

        public UnitOfMeasureJsonConverterFactory()
        {
            this._converters = new ConcurrentDictionary<Type, JsonConverter>();
        }

        public override bool CanConvert(Type typeToConvert)
        {
            typeToConvert = Nullable.GetUnderlyingType(typeToConvert) ?? typeToConvert;

            return typeToConvert.IsGenericType
                && typeToConvert.GetGenericTypeDefinition() == typeof(Quantity<>);
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return this._converters.GetOrAdd(
                typeToConvert,
                type =>
                {
                    var underlyingQuantityType = Nullable.GetUnderlyingType(type);
                    var isNullable = underlyingQuantityType != null;
                    underlyingQuantityType ??= type;
                    var underlyingUnitType = underlyingQuantityType.GetGenericArguments().Single();

                    var converterType = isNullable
                        ? typeof(NullableUnitOfMeasureJsonConverter<>)
                        : typeof(UnitOfMeasureJsonConverter<>);

                    var typedConverter = converterType.MakeGenericType(underlyingUnitType);
                    return (JsonConverter)Activator.CreateInstance(typedConverter);
                });
        }
    }
}
