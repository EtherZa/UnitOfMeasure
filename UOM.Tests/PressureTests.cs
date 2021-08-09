namespace UOM.Tests
{
    using UOM.Tests.Helper;
    using NUnit.Framework;

    public class PressureTests : BaseUnitTest<Pressure>
    {
        [TestCase("101325pa", "1atm")]
        [TestCase("100000pa", "1bar")]
        [TestCase("100000pa", "29.529980164712inhg")]
        [TestCase("100000pa", "14.503773773021psi")]
        [TestCase("100000pa", "750.06168270417torr")]
        [TestCase("1000pa", "1kpa")]
        public override void Conversion(string source, string expected)
        {
            base.Conversion(source, expected);
        }
    }
}
