namespace UOM.Tests.Helper
{
    using System.Globalization;
    using FluentAssertions;

    public abstract class BaseUnitTest<T>
        where T : Unit
    {
        const decimal _precision = 0.000_000_001M;

        public virtual void Add(string left, string right, string expected)
        {
            var leftQuantity = Quantity<T>.Parse(left, NumberFormatInfo.InvariantInfo);
            var rightQuantity = Quantity<T>.Parse(right, NumberFormatInfo.InvariantInfo);
            var expectedUnit = Quantity<T>.Parse(expected, NumberFormatInfo.InvariantInfo);

            var actual = leftQuantity.Add(rightQuantity);

            actual.Unit.Should().Be(leftQuantity.Unit);

            var convertedActual = actual.As(expectedUnit.Unit);
            convertedActual.Unit.Should().BeEquivalentTo(expectedUnit.Unit);
            convertedActual.Value.Should().BeApproximately(expectedUnit.Value, _precision);
        }

        public virtual void Conversion(string source, string expected)
        {
            var expectedQuantity = Quantity<T>.Parse(expected, NumberFormatInfo.InvariantInfo);
            var actual = Quantity<T>.Parse(source, NumberFormatInfo.InvariantInfo).As(expectedQuantity.Unit);

            actual.Unit.Should().BeEquivalentTo(expectedQuantity.Unit);
            actual.Value.Should().BeApproximately(expectedQuantity.Value, _precision);
        }

        public virtual void Subtract(string left, string right, string expected)
        {
            var leftQuantity = Quantity<T>.Parse(left, NumberFormatInfo.InvariantInfo);
            var rightQuantity = Quantity<T>.Parse(right, NumberFormatInfo.InvariantInfo);
            var expectedQuantity = Quantity<T>.Parse(expected, NumberFormatInfo.InvariantInfo);

            var actual = leftQuantity.Subtract(rightQuantity);

            actual.Unit.Should().Be(leftQuantity.Unit);

            var convertedActual = actual.As(expectedQuantity.Unit);
            convertedActual.Unit.Should().BeEquivalentTo(expectedQuantity.Unit);
            convertedActual.Value.Should().BeApproximately(expectedQuantity.Value, _precision);
        }
    }
}
