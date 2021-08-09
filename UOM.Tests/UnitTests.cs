namespace UOM.Tests
{
    using NUnit.Framework;

    public static class UnitTests
    {
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