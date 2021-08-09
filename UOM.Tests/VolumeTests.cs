namespace UOM.Tests
{
    using UOM.Tests.Helper;
    using NUnit.Framework;

    public class VolumeTests : BaseUnitTest<Volume>
    {
        [TestCase("1m³", "1000000000mm³")]
        [TestCase("1m³", "1000000cm³")]
        [TestCase("1m³", "1000dm³")]
        [TestCase("1m³", "0.000000001km³")]
        public override void Conversion(string source, string expected)
        {
            base.Conversion(source, expected);
        }

        [TestCase("3m³", "2m³", "5m³")]
        [TestCase("1m³", "100cm³", "1.0001m³")]
        public override void Add(string left, string right, string expected)
        {
            base.Add(left, right, expected);
        }

        [TestCase("5m³", "3m³", "2m³")]
        [TestCase("1.0001m³", "1m³", "100cm³")]
        public override void Subtract(string left, string right, string expected)
        {
            base.Subtract(left, right, expected);
        }
    }
}