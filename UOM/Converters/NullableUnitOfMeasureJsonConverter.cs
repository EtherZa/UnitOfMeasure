namespace UOM.Converters
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using UOM;

    public class NullableUnitOfMeasureJsonConverter<T> : JsonConverter<Quantity<T>?>
        where T : Unit
    {
        public override Quantity<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.Null:
                    return null;

                case JsonTokenType.String:
                    var value = reader.GetString();
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        return null;
                    }

                    if (Quantity<T>.TryParse(value, out var quantity))
                    {
                        return quantity;
                    }

                    break;
            }

            throw new InvalidCastException($"Unable to parse '{reader.GetString()}' as {nameof(Quantity<T>)}");
        }

        public override void Write(Utf8JsonWriter writer, Quantity<T>? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
            {
                writer.WriteStringValue(value.ToString());
            }
        }
    }
}
