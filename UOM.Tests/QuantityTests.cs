using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace UOM.Tests
{
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

        public class ToStringTests
        {
            [Test]
            public void AppendSymbol()
            {
                const string expected = "1 g/t";

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
