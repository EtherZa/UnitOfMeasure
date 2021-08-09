namespace UOM.Tests
{
    using Helper;
    using NUnit.Framework;

    public class TemperatureTests : BaseUnitTest<Temperature>
    {
        [TestCase("0°C", "273.15K")]
        [TestCase("0°C", "32°F")]
        [TestCase("0°C", "491.67°R")]
        [TestCase("100K", "-173.15°C")]
        [TestCase("100K", "-279.67°F")]
        [TestCase("100K", "180°R")]
        [TestCase("113°F", "45°C")]
        [TestCase("113°F", "318.15K")]
        [TestCase("113°F", "572.67°R")]
        public override void Conversion(string source, string expected)
        {
            base.Conversion(source, expected);
        }

        [TestCase("10°C", "15°C", "25°C")]
        [TestCase("10°C", "15K", "25°C")]
        [TestCase("15K", "10°C", "25°C")]
        [TestCase("-20°C", "15K", "-5°C")]
        public override void Add(string left, string right, string expected)
        {
            base.Add(left, right, expected);
        }

        [TestCase("25°C", "10°C", "15°C")]
        [TestCase("25°C", "10°C", "288.15K")]
        [TestCase("25°C", "15K", "10°C")]
        [TestCase("-5°C", "-20°C", "15°C")]
        [TestCase("-20°C", "15K", "238.15K")]
        public override void Subtract(string left, string right, string expected)
        {
            base.Subtract(left, right, expected);
        }
    }
}
