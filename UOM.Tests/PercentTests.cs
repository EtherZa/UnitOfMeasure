namespace UOM.Tests
{
    using NUnit.Framework;
    using UOM;
    using UOM.Tests.Helper;

    public class PercentTests : BaseUnitTest<Percent>
    {
        [TestCase("20%", "30%", "50%")]
        public override void Add(string left, string right, string expected)
        {
            base.Add(left, right, expected);
        }

        [TestCase("90%", "45%", "45%")]
        public override void Subtract(string left, string right, string expected)
        {
            base.Subtract(left, right, expected);
        }
    }
}
