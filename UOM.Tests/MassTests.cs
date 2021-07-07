namespace UOM.Tests
{
    using NUnit.Framework;
    using UOM;
    using UOM.Tests.Helper;

    public class MassTests : BaseUnitTest<Mass>
    {
        [TestCase("1000g", "1kg")]
        [TestCase("1000g", "0.001t")]
        [TestCase("1000g", "35.2739619495804oz")]
        [TestCase("1000g", "2.20462262184878lb")]
        [TestCase("2kg", "2000g")]
        [TestCase("1000kg", "1t")]
        [TestCase("1kg", "35.2739619495804oz")]
        [TestCase("1kg", "2.20462262184878lb")]
        [TestCase("1t", "1000000g")]
        [TestCase("1t", "1000kg")]
        [TestCase("1t", "35273.9619495804oz")]
        [TestCase("1t", "2204.62262184878lb")]
        [TestCase("1oz", "28.349523125g")]
        [TestCase("100oz", "2.8349523125kg")]
        [TestCase("100oz", "0.0028349523125t")]
        [TestCase("100oz", "6.25lb")]
        public override void Conversion(string source, string expected)
        {
            base.Conversion(source, expected);
        }

        [TestCase("2kg", "2kg", "4kg")]
        [TestCase("250kg", "750kg", "1t")]
        [TestCase("1kg", "500g", "1.5kg")]
        [TestCase("1kg", "100oz", "3.8349523125kg")]
        public override void Add(string left, string right, string expected)
        {
            base.Add(left, right, expected);
        }

        [TestCase("4kg", "2kg", "2kg")]
        [TestCase("1t", "250kg", "750kg")]
        [TestCase("1.5kg", "1kg", "500g")]
        [TestCase("3.8349523125kg", "1kg", "100oz")]
        public override void Subtract(string left, string right, string expected)
        {
            base.Subtract(left, right, expected);
        }
    }
}
