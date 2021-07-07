namespace UOM.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using FluentAssertions;
    using NUnit.Framework;

    public static class UnitTests
    {
        public class ParseTests
        {
            private static IEnumerable<Unit> RegisteredUnits => UnitRegistration.GetUnits();

            [Test]
            public void Value_IsNull_ThrowsException()
            {
                Assert.Throws<ArgumentNullException>(() => Unit.Parse<Mass>(null, NumberFormatInfo.CurrentInfo));
            }

            [Test]
            public void Value_IsMalformed_ThrowsException()
            {
                Assert.Throws<FormatException>(() => Unit.Parse<Mass>("malformed", NumberFormatInfo.CurrentInfo));
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

                var actual = Unit.Parse<Mass>(testFormat, numberFormatInfo);

                Assert.AreEqual(expected, actual.Value);
            }

            [TestCaseSource(nameof(RegisteredUnits))]
            public void Value_WithSpaceBeforeSymbol<T>(T unit)
                where T : Unit
            {
                const decimal value = 54.321M;
                var formatted = $"{value}{unit.Symbol}";

                var actual = Unit.Parse<T>(formatted, NumberFormatInfo.CurrentInfo);

                actual.Unit.Should().Be(unit);
                actual.Value.Should().Be(value);
            }

            [TestCaseSource(nameof(RegisteredUnits))]
            public void Value_WithoutSpaceBeforeSymbol<T>(T unit)
                where T : Unit
            {
                const decimal value = 123.45M;
                var formatted = $"{value} {unit.Symbol}";

                var actual = Unit.Parse<T>(formatted, NumberFormatInfo.CurrentInfo);

                actual.Unit.Should().Be(unit);
                actual.Value.Should().Be(value);
            }
        }

        public class EqualsTests
        {
            [Test]
            public void DifferentUnits_WithSameSymbolOffsetAndFactor_AreEqual()
            {
                const string symbol = "S";
                const decimal factor = 100;
                const decimal offset = 123;

                var left = new TestUnit(symbol, factor, offset);
                var right = new TestUnit2(symbol, factor, offset);

                Assert.AreEqual(left, right);
            }

            [Test]
            public void SameUnits_WithDifferentFactor_AreNotEqual()
            {
                const string symbol = "S";
                const decimal factor = 100;
                const decimal offset = 123;

                var left = new TestUnit(symbol, factor, offset);
                var right = new TestUnit(symbol, factor + 1, offset);

                Assert.AreNotEqual(left, right);
            }

            [Test]
            public void SameUnits_WithDifferentOffset_AreNotEqual()
            {
                const string symbol = "S";
                const decimal factor = 100;
                const decimal offset = 123;

                var left = new TestUnit(symbol, factor, offset);
                var right = new TestUnit(symbol, factor, offset + 1);

                Assert.AreNotEqual(left, right);
            }

            [Test]
            public void SameUnits_WithSameSymbolOffsetAndFactor_AreEqual()
            {
                const string symbol = "S";
                const decimal factor = 100;
                const decimal offset = 123;

                var left = new TestUnit(symbol, factor, offset);
                var right = new TestUnit(symbol, factor, offset);

                Assert.AreEqual(left, right);
            }
        }

        public class GetHashCodeTests
        {
            [Test]
            public void DifferentUnits_WithSameSymbolOffsetAndFactor_AreEqual()
            {
                const string symbol = "S";
                const decimal factor = 100;
                const decimal offset = 123;

                var left = new TestUnit(symbol, factor, offset);
                var right = new TestUnit2(symbol, factor, offset);

                Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
            }

            [Test]
            public void SameUnits_WithDifferentFactor_AreNotEqual()
            {
                const string symbol = "S";
                const decimal factor = 100;
                const decimal offset = 123;

                var left = new TestUnit(symbol, factor, offset);
                var right = new TestUnit(symbol, factor + 1, offset);

                Assert.AreNotEqual(left.GetHashCode(), right.GetHashCode());
            }

            [Test]
            public void SameUnits_WithDifferentOffset_AreNotEqual()
            {
                const string symbol = "S";
                const decimal factor = 100;
                const decimal offset = 123;

                var left = new TestUnit(symbol, factor, offset);
                var right = new TestUnit(symbol, factor, offset + 1);

                Assert.AreNotEqual(left.GetHashCode(), right.GetHashCode());
            }

            [Test]
            public void SameUnits_WithSameSymbolOffsetAndFactor_AreEqual()
            {
                const string symbol = "S";
                const decimal factor = 100;
                const decimal offset = 123;

                var left = new TestUnit(symbol, factor, offset);
                var right = new TestUnit(symbol, factor, offset);

                Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
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
}