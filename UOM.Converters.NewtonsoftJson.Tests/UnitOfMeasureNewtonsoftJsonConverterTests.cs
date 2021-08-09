namespace UOM.Converters.NewtonsoftJson.Tests
{
    using System;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using UOM.Converters;
    using UOM.Converters.NewtonsoftJson.Tests.Helper;

    public static class UnitOfMeasureNewtonsoftJsonConverterTests
    {
        public class CanConvertTests : RegisteredQuantitiesBase
        {
            [TestCaseSource(nameof(AllRegisteredQuantities))]
            public void RegisteredQuantity_MarkedAsSerializable(Type quantityType, Type _)
            {
                var target = new UnitOfMeasureNewtonsoftJsonConverter();
                var actual = target.CanConvert(quantityType);

                Assert.True(actual);
            }

            [Test]
            public void NonQuantity_WillNotBeserialized()
            {
                var target = new UnitOfMeasureNewtonsoftJsonConverter();
                var actual = target.CanConvert(typeof(CanConvertTests));

                Assert.False(actual);
            }
        }

        public class WriteJsonTests
        {
            [Test]
            public void CanWrite()
            {
                const string expected = "\"123kg\"";
                var value = new Quantity<Mass>(123, Mass.SI);

                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new UnitOfMeasureNewtonsoftJsonConverter());

                var actual = JsonConvert.SerializeObject(value, settings);

                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void NullValue()
            {
                const string expected = "null";
                Quantity<Mass>? value = new Quantity<Mass>?();

                var actual = JsonConvert.SerializeObject(value, new UnitOfMeasureNewtonsoftJsonConverter());

                Assert.AreEqual(expected, actual);
            }
        }

        public class ReadJsonTests
        {
            [Test]
            public void ParseValidString()
            {
                var expected = new Quantity<Mass>(123, Mass.Kilogram);

                const string serializedValue = "{\"Value\":\"123kg\"}";

                var actual = JsonConvert.DeserializeObject<Host<Quantity<Mass>>>(serializedValue, new UnitOfMeasureNewtonsoftJsonConverter()).Value;

                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void ParseNullString()
            {
                var expected = new Quantity<Mass>?();

                const string serializedValue = "{\"Value\":null}";

                var actual = JsonConvert.DeserializeObject<Host<Quantity<Mass>?>>(serializedValue, new UnitOfMeasureNewtonsoftJsonConverter()).Value;

                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void NullValue_OnNonNullableType_ThrowsException()
            {
                const string serializedValue = "{\"Value\":null}";
                Assert.Throws<FormatException>(() => JsonConvert.DeserializeObject<Host<Quantity<Mass>>>(serializedValue, new UnitOfMeasureNewtonsoftJsonConverter()));
            }

            [Test]
            public void ParseMissingValueAsNull()
            {
                var expected = new Quantity<Mass>?();

                const string serializedValue = "{}";

                var actual = JsonConvert.DeserializeObject<Host<Quantity<Mass>?>>(serializedValue, new UnitOfMeasureNewtonsoftJsonConverter()).Value;

                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void InvalidValue_ThrowsException()
            {
                const string serializedValue = "{\"Value\":\"invalid value\"}";

                Assert.Throws<FormatException>(() => JsonConvert.DeserializeObject<Host<Quantity<Mass>?>>(serializedValue, new UnitOfMeasureNewtonsoftJsonConverter()));
            }
        }

        public class Host<T>
        {
            public Host()
                : this(default)
            {
            }

            public Host(T value)
            {
                Value = value;
            }

            public T Value { get; set; }
        }
    }
}