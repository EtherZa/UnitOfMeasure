namespace UOM.Converters.ProtbufNet.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;
    using UOM.Converters;
    using NUnit.Framework;

    public static class UnitOfMeasureJsonConverterFactoryTests
    {
        public class CanConvertTests : UnitOfMeasureJsonConverterFactoryBase
        {
            [TestCaseSource(nameof(RegisteredQuantities))]
            public void Quantity_CanBeConverted(Type quantityType, Type unitType)
            {
                var target = new UnitOfMeasureJsonConverterFactory();

                target.CanConvert(quantityType);
            }

            [TestCaseSource(nameof(RegisteredNullableQuantities))]
            public void NullableQuantity_CanBeConverted(Type quantityType, Type unitType)
            {
                var target = new UnitOfMeasureJsonConverterFactory();
                var actual = target.CanConvert(quantityType);

                Assert.True(actual);
            }

            [Test]
            public void NonQuantity_CanNotBeConverted()
            {
                var type = typeof(int);

                var target = new UnitOfMeasureJsonConverterFactory();
                var actual = target.CanConvert(type);

                Assert.False(actual);
            }
        }

        public class CreateConverterTests : UnitOfMeasureJsonConverterFactoryBase
        {
            [TestCaseSource(nameof(AllRegisteredQuantities))]
            public void CanGetConverterForQuantity(Type quantityType, Type unitType)
            {
                var options = new JsonSerializerOptions();

                var target = new UnitOfMeasureJsonConverterFactory();
                var actual = target.CreateConverter(quantityType, options);

                Assert.NotNull(actual);
                var actualType = actual.GetType();

                Assert.True(actualType.IsGenericType);

                var converterType = actualType.GetGenericArguments().Single();
                Assert.AreEqual(unitType, converterType);
            }
        }

        public abstract class UnitOfMeasureJsonConverterFactoryBase
        {
            protected static IEnumerable<object[]> RegisteredQuantities
            {
                get
                {
                    foreach (var unitType in UnitRegistration.GetUnits().Select(x => x.GetType()))
                    {
                        yield return new object[] { typeof(Quantity<>).MakeGenericType(unitType), unitType };
                    }
                }
            }

            protected static IEnumerable<object[]> RegisteredNullableQuantities
            {
                get
                {
                    foreach (var unitType in UnitRegistration.GetUnits().Select(x => x.GetType()))
                    {
                        var type = typeof(Quantity<>).MakeGenericType(unitType);
                        yield return new object[] { typeof(Nullable<>).MakeGenericType(type), unitType };
                    }
                }
            }

            protected static IEnumerable<object[]> AllRegisteredQuantities => RegisteredQuantities.Union(RegisteredNullableQuantities);
        }
    }
}
