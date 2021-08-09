namespace UOM
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reflection;

    public static class UnitRegistration
    {
        private static readonly object _synclock = new object();

        private static IDictionary<Type, ReadOnlyCollection<Unit>> _unitRegistrations;
        private static IDictionary<Type, IDictionary<string, Unit>> _symbolRegistrations;

        public static T GetUnit<T>(string symbol)
            where T : Unit
        {
            EnsureUnitLoaded();

            if (_symbolRegistrations.TryGetValue(typeof(T), out var dictionary) && dictionary.TryGetValue(symbol, out var unit))
            {
                return unit as T;
            }

            return null;
        }

        public static IEnumerable<Unit> GetUnits()
        {
            EnsureUnitLoaded();
            return _unitRegistrations.SelectMany(x => x.Value);
        }

        public static IEnumerable<T> GetUnits<T>()
            where T : Unit
        {
            EnsureUnitLoaded();

            var type = typeof(T);
            if (_unitRegistrations.TryGetValue(type, out var units))
            {
                return units.OfType<T>();
            }

            var ex = new InvalidOperationException($"Unit '{type}' is not registered.");
            ex.Data.Add("type", type.Name);
            throw ex;
        }

        private static void EnsureUnitLoaded()
        {
            if (_symbolRegistrations != null)
            {
                return;
            }

            lock (_synclock)
            {
                if (_symbolRegistrations != null)
                {
                    return;
                }

                var types = new[]
                        {
                        typeof(Density),
                        typeof(EnergyPerMass),
                        typeof(Length),
                        typeof(Mass),
                        typeof(MassFraction),
                        typeof(Percent),
                        typeof(Pressure),
                        typeof(Temperature),
                        typeof(Volume)
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

                _unitRegistrations = q.GroupBy(x => x.type)
                    .ToDictionary(x => x.Key, x => x.Select(z => z.instance).ToList().AsReadOnly());

                _symbolRegistrations = _unitRegistrations.ToDictionary(x => x.Key, v => (IDictionary<string, Unit>)v.Value.GroupBy(x => x.Symbol).ToDictionary(x => x.Key, x => x.First()));
            }
        }
    }
}