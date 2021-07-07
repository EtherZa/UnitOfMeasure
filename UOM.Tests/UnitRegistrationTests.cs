namespace UOM.Tests
{
    using System;
    using NUnit.Framework;

    public static class UnitRegistrationTests
    {
        public class GetUnitsTests
        {
            [Test]
            public void UnitNotRegistered_ThrowException()
            {
                Assert.Throws<InvalidOperationException>(() => UnitRegistration.GetUnits<Unit>());
            }
        }
    }
}
