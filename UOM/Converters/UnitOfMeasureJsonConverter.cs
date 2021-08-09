namespace UOM.Converters
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using UOM;

    public class UnitOfMeasureJsonConverter<T> : JsonConverter<Quantity<T>>
        where T : Unit
    {
        public override Quantity<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var value = reader.GetString();
                if (Quantity<T>.TryParse(value, out var quantity))
                {
                    return quantity;
                }
            }

            throw new InvalidCastException($"Unable to parse '{reader.GetString()}' as {nameof(Quantity<T>)}");
        }

        public override void Write(Utf8JsonWriter writer, Quantity<T> value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
