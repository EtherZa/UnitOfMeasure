namespace UOM
{
    using System;
    using System.Globalization;
    using System.Linq;

    public struct Quantity<T>
        where T : Unit
    {
        public Quantity(decimal value, T unit)
        {
            this.Value = value;
            this.Unit = unit;
        }

        public string Symbol => this.Unit?.Symbol;

        public decimal Value { get; }

        public T Unit { get; }

        public static Quantity<T> Parse(string value, IFormatProvider formatProvider = null)
        {
            if (!TryParse(value, out var result, formatProvider))
            {
                var ex = new FormatException($"Unable to parse value as '{typeof(T)}'");
                ex.Data.Add("type", typeof(T).FullName);
                throw ex;
            }

            return result;
        }

        public static bool TryParse(string value, out Quantity<T> quantity, IFormatProvider formatProvider = null)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            value = value.Trim();

            var numFormat = formatProvider != null
                ? (NumberFormatInfo)formatProvider.GetFormat(typeof(NumberFormatInfo)) ?? throw new InvalidOperationException($"No number format was found for the given format provider: {formatProvider}")
                : NumberFormatInfo.CurrentInfo;

            foreach (var unit in UnitRegistration.GetUnits<T>().OrderByDescending(x => x.Symbol.Length))
            {
                if (value.EndsWith(unit.Symbol))
                {
                    var trimmed = value.Substring(0, value.Length - unit.Symbol.Length).TrimEnd();
                    if (decimal.TryParse(trimmed, NumberStyles.Number | NumberStyles.Float | NumberStyles.AllowExponent, formatProvider, out var result))
                    {
                        quantity = new Quantity<T>(result, unit);
                        return true;
                    }
                }
            }

            quantity = default;
            return false;
        }

        public Quantity<T> Add(Quantity<T> value)
        {
            decimal workingValue;
            if (this.Unit.Equals(value.Unit))
            {
                workingValue = this.Value + value.Value;
            }
            else
            {
                var converted = Math.Abs(this.Unit.Offset) + value.As(this.Unit).Value;
                workingValue = this.Value + converted;
            }

            return new Quantity<T>(workingValue, this.Unit);
        }

        public Quantity<T> Subtract(Quantity<T> value)
        {
            decimal workingValue;
            if (this.Unit.Equals(value.Unit))
            {
                workingValue = this.Value - value.Value;
            }
            else
            {
                var converted = Math.Abs(this.Unit.Offset) + value.As(this.Unit).Value;
                workingValue = this.Value - converted;
            }

            return new Quantity<T>(workingValue, this.Unit);
        }

        public Quantity<T> As(T target)
        {
            if (this.Unit.Equals(target))
            {
                return this;
            }

            var baseValue = BaseValue(this);
            var value = (baseValue * target.Factor) + target.Offset;

            return new Quantity<T>(value, target);
        }

        internal static decimal BaseValue(Quantity<T> quantity)
        {
            return (quantity.Value - quantity.Unit.Offset) / quantity.Unit.Factor;
        }

        public override bool Equals(object obj)
        {
            return
                obj is Quantity<T> quantity
                && (BaseValue(this) == BaseValue(quantity));
        }

        public override int GetHashCode()
        {
            var bashValue = BaseValue(this);
            return HashCode.Combine(this.Unit.GetType(), bashValue);
        }

        public override string ToString()
        {
            return $"{this.Value}{this.Symbol}";
        }

        public static Quantity<T> operator +(Quantity<T> a, Quantity<T> b) => a.Add(b);

        public static Quantity<T> operator -(Quantity<T> a, Quantity<T> b) => a.Subtract(b);
    }
}
