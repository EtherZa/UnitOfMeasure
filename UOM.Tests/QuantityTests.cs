namespace UOM.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using FluentAssertions;
    using NUnit.Framework;

    public static class QuantityTests
    {
        public class EqualsTests
        {
            [Test]
            public void NonQualityObject_DoesNotMatch()
            {
                var target = new Quantity<Mass>(1, Mass.Kilogram);
                Assert.AreNotEqual(target, new object());
            }

            [Test]
            public void DifferentUnit_OfSameValue_AreEqual()
            {
                var left = new Quantity<Mass>(1, Mass.Kilogram);
                var right = left.As(Mass.Gram);

                Assert.AreEqual(left, right);
            }

            [Test]
            public void SameUnit_OfDifferentValue_AreNotEqual()
            {
                var left = new Quantity<Mass>(1, Mass.Kilogram);
                var right = new Quantity<Mass>(2, Mass.Kilogram);

                Assert.AreNotEqual(left, right);
            }

            [Test]
            public void SameUnit_OfSameValue_AreEqual()
            {
                var left = new Quantity<Mass>(1, Mass.Kilogram);
                var right = new Quantity<Mass>(1, Mass.Kilogram);

                Assert.AreEqual(left, right);
            }
        }

        public class GetHasCodeTests
        {
            [Test]
            public void DifferentUnit_OfSameValue_AreEqual()
            {
                var left = new Quantity<Mass>(1, Mass.Kilogram);
                var right = left.As(Mass.Gram);

                Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
            }

            [Test]
            public void SameUnit_OfDifferentValue_AreNotEqual()
            {
                var left = new Quantity<Mass>(1, Mass.Kilogram);
                var right = new Quantity<Mass>(2, Mass.Kilogram);

                Assert.AreNotEqual(left.GetHashCode(), right.GetHashCode());
            }

            [Test]
            public void SameUnit_OfSameValue_AreEqual()
            {
                var left = new Quantity<Mass>(1, Mass.Kilogram);
                var right = new Quantity<Mass>(1, Mass.Kilogram);

                Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
            }
        }

        public class ParseTests
        {
            private static IEnumerable<Unit> RegisteredUnits => UnitRegistration.GetUnits();

            [Test]
            public void Value_IsNull_ThrowsException()
            {
                Assert.Throws<ArgumentNullException>(() => Quantity<Mass>.Parse(null, NumberFormatInfo.CurrentInfo));
            }

            [Test]
            public void Value_IsMalformed_ThrowsException()
            {
                Assert.Throws<FormatException>(() => Quantity<Mass>.Parse("malformed", NumberFormatInfo.CurrentInfo));
            }

            [Test]
            public void NumberFormat_IsSpecified_Parses()
            {
                var numberFormatInfo = new NumberFormatInfo()
                {
                    NumberDecimalSeparator = "~~"
                };

                const decimal expected = 123456.54321m;

                const string unitFormat = "{0} kg";
                var defaultFormat = string.Format(unitFormat, expected.ToString(NumberFormatInfo.CurrentInfo));
                var testFormat = string.Format(unitFormat, expected.ToString(numberFormatInfo));

                Console.WriteLine($"Parsing: '{testFormat}'");

                if (defaultFormat.Equals(testFormat, StringComparison.Ordinal))
                {
                    Assert.Inconclusive("Custom number format produces a string that is the same as the default number format");
                }

                var actual = Quantity<Mass>.Parse(testFormat, numberFormatInfo);

                Assert.AreEqual(expected, actual.Value);
            }

            [TestCaseSource(nameof(RegisteredUnits))]
            public void Value_WithSpaceBeforeSymbol<T>(T unit)
                where T : Unit
            {
                const decimal value = 54.321M;
                var formatted = $"{value}{unit.Symbol}";

                var actual = Quantity<T>.Parse(formatted, NumberFormatInfo.CurrentInfo);

                actual.Unit.Should().Be(unit);
                actual.Value.Should().Be(value);
            }

            [TestCaseSource(nameof(RegisteredUnits))]
            public void Value_WithoutSpaceBeforeSymbol<T>(T unit)
                where T : Unit
            {
                const decimal value = 123.45M;
                var formatted = $"{value} {unit.Symbol}";

                var actual = Quantity<T>.Parse(formatted, NumberFormatInfo.CurrentInfo);

                actual.Unit.Should().Be(unit);
                actual.Value.Should().Be(value);
            }
        }

        public class OperatorTests
        {
            [Test]
            public void Add()
            {
                var unit = new TestUnit("S", 1, 0);

                var q1 = new Quantity<TestUnit>(10, unit);
                var q2 = new Quantity<TestUnit>(20, unit);

                var expected = q1.Value + q2.Value;

                var actual = q1 + q2;

                actual.Unit.Should().Be(q1.Unit);
                Assert.AreEqual(expected, actual.Value);
            }

            [Test]
            public void Subtract()
            {
                var unit = new TestUnit("S", 1, 0);

                var q1 = new Quantity<TestUnit>(30, unit);
                var q2 = new Quantity<TestUnit>(20, unit);

                var expected = q1.Value - q2.Value;

                var actual = q1 - q2;

                actual.Unit.Should().Be(q1.Unit);
                Assert.AreEqual(expected, actual.Value);
            }

            private sealed class TestUnit : Unit
            {
                public TestUnit(string symbol, decimal factor, decimal offset)
                    : base(symbol, factor, offset)
                {
                }
            }

            private sealed class TestUnit2 : Unit
            {
                public TestUnit2(string symbol, decimal factor, decimal offset)
                    : base(symbol, factor, offset)
                {
                }
            }
        }

        public class ToStringTests
        {
            [Test]
            public void AppendSymbol()
            {
                const string expected = "1g/t";

                var target = new Quantity<MassFraction>(1, MassFraction.GramsPerTon);
                var actual = target.ToString();

                Assert.AreEqual(expected, actual);
            }
        }

        public class ValueTests
        {
            [Test]
            public void Value_IsImmutable()
            {
                var type = typeof(Quantity<Unit>);

                var property = type.GetProperties(BindingFlags.Default | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.Public | BindingFlags.Instance)
                    .SingleOrDefault(x => x.Name.Equals(nameof(Quantity<Unit>.Value)));

                Assert.NotNull(property);
                Assert.False(property.CanWrite);
            }
        }
    }
}
