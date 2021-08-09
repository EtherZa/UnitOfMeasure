namespace UOM.Converters
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class QuantitySurrogate<T>
        where T : Unit
    {
        [DataMember(Order = 1)]
        public decimal Value { get; set; }

        [DataMember(Order = 2)]
        public string Symbol { get; set; }

        public static implicit operator Quantity<T>(QuantitySurrogate<T> surrogate)
        {
            if (surrogate == null)
            {
                return default;
            }

            var unit = UnitRegistration.GetUnit<T>(surrogate.Symbol);
            if (unit == null)
            {
                throw new InvalidOperationException($"Unable to parse symbol '{surrogate.Symbol}'");
            }

            return new Quantity<T>(surrogate.Value, unit);
        }

        public static implicit operator QuantitySurrogate<T>(Quantity<T> source)
        {
            return new QuantitySurrogate<T>
            {
                Value = source.Value,
                Symbol = source.Symbol
            };
        }
    }
}
