namespace UOM.Tests.Converters
{
    using System;
    using System.Text.Json;
    using UOM.Converters;
    using NUnit.Framework;

    public static class UnitOfMeasureJsonConverterTests
    {
        public class ReadTests
        {
            [Test]
            public void String_IsDeserialized()
            {
                const string serializedValue = "\"123kg\"";
                var expected = new Quantity<Mass>(123, Mass.SI);

                var options = new JsonSerializerOptions();
                options.Converters.Add(new UnitOfMeasureJsonConverter<Mass>());

                var actual = JsonSerializer.Deserialize<Quantity<Mass>>(serializedValue, options);

                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void EmptyString_ThrowsException()
            {
                const string empty = "\"\"";
                var options = new JsonSerializerOptions();
                options.Converters.Add(new UnitOfMeasureJsonConverter<Mass>());

                Assert.Throws<InvalidCastException>(() => JsonSerializer.Deserialize<Quantity<Mass>>(empty, options));
            }

            [Test]
            public void InvalidString_ThrowsException()
            {
                const string invalid = "\"invalid\"";

                var options = new JsonSerializerOptions();
                options.Converters.Add(new UnitOfMeasureJsonConverter<Mass>());

                Assert.Throws<InvalidCastException>(() => JsonSerializer.Deserialize<Quantity<Mass>>(invalid, options));
            }
        }

        public class WriteTests
        {
            [Test]
            public void Value_IsSerialized()
            {
                const string expected = "\"123kg\"";
                var tuple = new Quantity<Mass>(123, Mass.SI);

                var options = new JsonSerializerOptions();
                options.Converters.Add(new UnitOfMeasureJsonConverter<Mass>());

                var actual = JsonSerializer.Serialize(tuple, options);

                Assert.AreEqual(expected, actual);
            }
        }
    }
}
