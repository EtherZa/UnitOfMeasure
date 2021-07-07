namespace UOM.Tests
{
    using NUnit.Framework;
    using UOM;
    using UOM.Tests.Helper;

    public class MassFractionTest : BaseUnitTest<MassFraction>
    {
        [TestCase("4g/t", "0.0004%")]
        [TestCase("1g/t", "1ppm")]
        [TestCase("1%", "10000g/t")]
        [TestCase("1%", "10000000ppb")]
        public override void Conversion(string source, string expected)
        {
            base.Conversion(source, expected);
        }

        [TestCase("4g/t", "0.000354%", "7.54g/t")]
        [TestCase("4g/t", "5g/t", "9g/t")]
        [TestCase("0.000355%", "5g/t", "8.55ppm")]
        [TestCase("0.000355%", "5g/t", "8550ppb")]
        public override void Add(string left, string right, string expected)
        {
            base.Add(left, right, expected);
        }

        [TestCase("7.54g/t", "4g/t", "0.000354%")]
        [TestCase("9g/t", "5g/t", "4g/t")]
        [TestCase("8.55ppm", "0.000355%", "5g/t")]
        [TestCase("8550ppb", "0.000355%", "5g/t")]
        public override void Subtract(string left, string right, string expected)
        {
            base.Subtract(left, right, expected);
        }
    }
}
