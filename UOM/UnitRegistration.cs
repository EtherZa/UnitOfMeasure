namespace UOM
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reflection;

    public static class UnitRegistration
    {
        private static readonly Lazy<IDictionary<Type, ReadOnlyCollection<Unit>>> _unitRegistrations;

        static UnitRegistration()
        {
            _unitRegistrations = new Lazy<IDictionary<Type, ReadOnlyCollection<Unit>>>(
                () =>
                {
                    var types = new[]
                    {
                        typeof(Density),
                        typeof(Length),
                        typeof(Mass),
                        typeof(MassFraction),
                        typeof(Percent),
                        typeof(Temperature)
                    };

                    var q = from type in types.AsParallel()
                            from t in type.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField)
                            where type.IsAssignableFrom(t.FieldType)
                            let instance = (Unit)t.GetValue(null)
                            select new
                            {
                                type,
                                instance
                            };

                    return q.GroupBy(x => x.type)
                        .ToDictionary(x => x.Key, x => x.Select(z => z.instance).ToList().AsReadOnly());
                }, true);
        }

        public static IEnumerable<Unit> GetUnits()
        {
            return _unitRegistrations.Value.SelectMany(x => x.Value);
        }

        public static IEnumerable<T> GetUnits<T>()
            where T : Unit
        {
            var type = typeof(T);
            if (_unitRegistrations.Value.TryGetValue(type, out var units))
            {
                return units.OfType<T>();
            }

            var ex = new InvalidOperationException($"Unit '{type}' is not registered.");
            ex.Data.Add("type", type.Name);
            throw ex;
        }
    }
}
