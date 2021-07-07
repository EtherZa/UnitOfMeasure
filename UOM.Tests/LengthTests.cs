namespace UOM.Tests
{
    using NUnit.Framework;
    using UOM;
    using UOM.Tests.Helper;

    public class LengthTests : BaseUnitTest<Length>
    {
        [TestCase("400cm", "4m")]
        [TestCase("4m", "0.004km")]
        [TestCase("4m", "40dm")]
        [TestCase("4m", "400cm")]
        [TestCase("4m", "4000mm")]
        [TestCase("10in", "25.4cm")]
        [TestCase("8ft", "2.4384m")]
        [TestCase("10yd", "9.144m")]
        [TestCase("10mi", "16.09344km")]
        public override void Conversion(string source, string expected)
        {
            base.Conversion(source, expected);
        }

        [TestCase("5cm", "10cm", "0.15m")]
        [TestCase("5m", "10m", "15m")]
        [TestCase("5m", "0.010km", "15m")]
        [TestCase("12in", "2ft", "3ft")]
        public override void Add(string left, string right, string expected)
        {
            base.Add(left, right, expected);
        }

        [TestCase("0.15m", "5cm", "10cm")]
        [TestCase("15m", "5m", "10m")]
        [TestCase("15m", "5m", "0.010km")]
        [TestCase("3ft", "12in", "2ft")]
        public override void Subtract(string left, string right, string expected)
        {
            base.Subtract(left, right, expected);
        }
    }
}
