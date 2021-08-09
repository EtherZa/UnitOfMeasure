namespace UOM.Tests
{
    using UOM.Tests.Helper;
    using NUnit.Framework;

    public class EnergyPerMassTests : BaseUnitTest<EnergyPerMass>
    {
        [TestCase("500kWh/kg", "500kWh/kg")]
        [TestCase("0.5kWh/t", "500kWh/kg")]
        [TestCase("1102.3113109243879036148690068kWh/lb", "500kWh/kg")]
        [TestCase("0.5511556554621939518074345kWh/sht", "500kWh/kg")]
        [TestCase("0.49210326380553031411378kWh/lt", "500kWh/kg")]
        [TestCase("1000kWh/kg", "1000kWh/kg")]
        [TestCase("1kWh/t", "1000kWh/kg")]
        [TestCase("2204.62262184876kWh/lb", "1000kWh/kg")]
        [TestCase("1.10231131092439kWh/sht", "1000kWh/kg")]
        [TestCase("0.98420652761106kWh/lt", "1000kWh/kg")]
        public override void Conversion(string source, string expected)
        {
            base.Conversion(source, expected);
        }
    }
}
