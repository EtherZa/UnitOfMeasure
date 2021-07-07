namespace UOM.Tests
{
    using NUnit.Framework;
    using UOM;
    using UOM.Tests.Helper;

    public class DensityTests : BaseUnitTest<Density>
    {
        [TestCase("1g/cm³", "1kg/m³")]
        public override void Conversion(string source, string expected)
        {
            base.Conversion(source, expected);
        }

        [TestCase("3g/cm³", "2g/cm³", "5g/cm³")]
        [TestCase("3g/cm³", "2kg/m³", "5g/cm³")]
        [TestCase("3g/cm³", "2kg/m³", "5kg/m³")]
        [TestCase("3kg/m³", "2kg/m³", "5kg/m³")]
        public override void Add(string left, string right, string expected)
        {
            base.Add(left, right, expected);
        }

        [TestCase("5g/cm³", "3g/cm³", "2g/cm³")]
        [TestCase("5kg/m³", "3kg/m³", "2kg/m³")]
        [TestCase("5kg/m³", "3g/cm³", "2kg/m³")]
        [TestCase("5kg/m³", "3g/cm³", "2g/cm³")]
        public override void Subtract(string left, string right, string expected)
        {
            base.Subtract(left, right, expected);
        }
    }
}
