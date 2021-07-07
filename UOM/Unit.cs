namespace UOM
{
    using System;
    using System.Globalization;
    using System.Linq;

    public class Unit
    {
        protected Unit(string symbol, decimal factor, decimal offset)
        {
            this.Offset = offset;
            this.Factor = factor;
            this.Symbol = symbol;
        }

        protected decimal Offset { get; }
        protected decimal Factor { get; }
        public string Symbol { get; }

        public static Quantity<T> Parse<T>(string value, IFormatProvider formatProvider = null)
            where T : Unit
        {
            if (!TryParse<T>(value, out var result, formatProvider))
            {
                var ex = new FormatException($"Unable to parse value as '{typeof(T)}'");
                ex.Data.Add("type", typeof(T).FullName);
                throw ex;
            }

            return result;
        }

        public static bool TryParse<T>(string value, out Quantity<T> quantity, IFormatProvider formatProvider = null)
            where T : Unit
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

        public virtual Quantity<T> As<T>(Quantity<T> quantity, T target)
            where T : Unit
        {
            if (this.Equals(target))
            {
                return new Quantity<T>(quantity.Value, quantity.Unit);
            }

            var baseValue = BaseValue(quantity);
            var value = (baseValue * target.Factor) + target.Offset;

            return new Quantity<T>(value, target);
        }

        internal static decimal BaseValue<T>(Quantity<T> quantity)
            where T : Unit
        {
            return (quantity.Value - quantity.Unit.Offset) / quantity.Unit.Factor;
        }

        internal Quantity<T> Add<T>(Quantity<T> left, Quantity<T> right)
            where T : Unit
        {
            decimal value;
            if (left.Unit.Equals(right.Unit))
            {
                value = left.Value + right.Value;
            }
            else
            {
                var converted = Math.Abs(left.Unit.Offset) + right.As(left.Unit).Value;
                value = left.Value + converted;
            }

            return new Quantity<T>(value, left.Unit);
        }

        internal Quantity<T> Subtract<T>(Quantity<T> left, Quantity<T> right)
            where T : Unit
        {
            decimal value;
            if (left.Unit.Equals(right.Unit))
            {
                value = left.Value - right.Value;
            }
            else
            {
                var converted = Math.Abs(left.Unit.Offset) + right.As(left.Unit).Value;
                value = left.Value - converted;
            }

            return new Quantity<T>(value, left.Unit);
        }

        public override bool Equals(object obj)
        {
            return
                obj is Unit unit
                && this.Symbol.Equals(unit.Symbol)
                && this.Offset.Equals(unit.Offset)
                && this.Factor.Equals(unit.Factor);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Symbol, this.Offset, this.Factor);
        }
    }
}
