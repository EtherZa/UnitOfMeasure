namespace UOM.Tests.Converters
{
    using System;
    using System.Text.Json;
    using UOM.Converters;
    using NUnit.Framework;

    public static class NullableUnitOfMeasureJsonConverterTests
    {
        public class ReadTests
        {
            [Test]
            public void String_IsDeserialized()
            {
                const string serializedValue = "\"123kg\"";
                var expected = new Quantity<Mass>(123, Mass.SI);

                var options = new JsonSerializerOptions();
                options.Converters.Add(new NullableUnitOfMeasureJsonConverter<Mass>());

                var actual = JsonSerializer.Deserialize<Quantity<Mass>?>(serializedValue, options);

                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void EmptyString_ReturnsNull()
            {
                const string empty = "\"\"";
                var options = new JsonSerializerOptions();
                options.Converters.Add(new NullableUnitOfMeasureJsonConverter<Mass>());

                Quantity<Mass>? expected = new Quantity<Mass>?();

                var actual = JsonSerializer.Deserialize<Quantity<Mass>?>(empty, options);

                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void InvalidString_ThrowsException()
            {
                const string invalid = "\"invalid\"";

                var options = new JsonSerializerOptions();
                options.Converters.Add(new NullableUnitOfMeasureJsonConverter<Mass>());

                Assert.Throws<InvalidCastException>(() => JsonSerializer.Deserialize<Quantity<Mass>?>(invalid, options));
            }
        }

        public class WriteTests
        {
            [Test]
            public void Value_IsSerialized()
            {
                const string expected = "{\"Value\":\"123kg\"}";
                var tuple = new Host<Quantity<Mass>?>(new Quantity<Mass>(123, Mass.SI));

                var options = new JsonSerializerOptions();
                options.Converters.Add(new NullableUnitOfMeasureJsonConverter<Mass>());
                options.IgnoreNullValues = false;

                var actual = JsonSerializer.Serialize(tuple, options);

                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void NullValue_IsSerialized()
            {
                const string expected = "{\"Value\":null}";

                var options = new JsonSerializerOptions();
                options.Converters.Add(new NullableUnitOfMeasureJsonConverter<Mass>());
                options.IgnoreNullValues = false;

                var tuple = new Host<Quantity<Mass>?>(null);

                var actual = JsonSerializer.Serialize(tuple, options);

                Assert.AreEqual(expected, actual);
            }

            public sealed class Host<T>
            {
                public Host(T value)
                {
                    this.Value = value;
                }

                public T Value { get; set; }
            }
        }
    }
}
