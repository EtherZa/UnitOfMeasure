namespace UOM.Tests.Helper
{
using System.Globalization;
    using FluentAssertions;
    using UOM;

    public abstract class BaseUnitTest<T>
        where T : Unit
    {
        const decimal _precision = 0.000_000_001M;

        public virtual void Add(string left, string right, string expected)
        {
            var leftUnit = Unit.Parse<T>(left, NumberFormatInfo.InvariantInfo);
            var rightUnit = Unit.Parse<T>(right, NumberFormatInfo.InvariantInfo);
            var expectedUnit = Unit.Parse<T>(expected, NumberFormatInfo.InvariantInfo);

            var actual = leftUnit.Add(rightUnit);

            actual.Unit.Should().Be(leftUnit.Unit);

            var convertedActual = actual.As(expectedUnit.Unit);
            convertedActual.Unit.Should().BeEquivalentTo(expectedUnit.Unit);
            convertedActual.Value.Should().BeApproximately(expectedUnit.Value, _precision);
        }

        public virtual void Conversion(string source, string expected)
        {
            var expectedUnit = Unit.Parse<T>(expected, NumberFormatInfo.InvariantInfo);
            var actual = Unit.Parse<T>(source, NumberFormatInfo.InvariantInfo).As(expectedUnit.Unit);

            actual.Unit.Should().BeEquivalentTo(expectedUnit.Unit);
            actual.Value.Should().BeApproximately(expectedUnit.Value, _precision);
        }

        public virtual void Subtract(string left, string right, string expected)
        {
            var leftUnit = Unit.Parse<T>(left, NumberFormatInfo.InvariantInfo);
            var rightUnit = Unit.Parse<T>(right, NumberFormatInfo.InvariantInfo);
            var expectedUnit = Unit.Parse<T>(expected, NumberFormatInfo.InvariantInfo);

            var actual = leftUnit.Subtract(rightUnit);

            actual.Unit.Should().Be(leftUnit.Unit);

            var convertedActual = actual.As(expectedUnit.Unit);
            convertedActual.Unit.Should().BeEquivalentTo(expectedUnit.Unit);
            convertedActual.Value.Should().BeApproximately(expectedUnit.Value, _precision);
        }
    }
}
