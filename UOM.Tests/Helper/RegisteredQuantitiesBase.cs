namespace UOM.Tests.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class RegisteredQuantitiesBase
    {
        protected static IEnumerable<object[]> RegisteredQuantities
        {
            get
            {
                foreach (var unitType in UnitRegistration.GetUnits().Select(x => x.GetType()))
                {
                    yield return new object[] { typeof(Quantity<>).MakeGenericType(unitType), unitType };
                }
            }
        }

        protected static IEnumerable<object[]> RegisteredNullableQuantities
        {
            get
            {
                foreach (var unitType in UnitRegistration.GetUnits().Select(x => x.GetType()))
                {
                    var type = typeof(Quantity<>).MakeGenericType(unitType);
                    yield return new object[] { typeof(Nullable<>).MakeGenericType(type), unitType };
                }
            }
        }

        protected static IEnumerable<object[]> AllRegisteredQuantities => RegisteredQuantities.Union(RegisteredNullableQuantities);
    }
}
