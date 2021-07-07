namespace UOM
{
    using System;

    public struct Quantity<T>
        where T : Unit
    {
        public Quantity(decimal value, T unit)
        {
            this.Value = value;
            this.Unit = unit;
        }

        public string Symbol => Unit.Symbol;

        public decimal Value { get; }

        internal T Unit { get; }

        public Quantity<T> Add(Quantity<T> value)
        {
            return this.Unit.Add(this, value);
        }

        public Quantity<T> Subtract(Quantity<T> value)
        {
            return this.Unit.Subtract(this, value);
        }

        public Quantity<T> As(T target)
        {
            return this.Unit.As(this, target);
        }

        public override bool Equals(object obj)
        {
            return
                obj is Quantity<T> quantity
                && (UOM.Unit.BaseValue(this) == UOM.Unit.BaseValue(quantity));
        }

        public override int GetHashCode()
        {
            var bashValue = UOM.Unit.BaseValue(this);
            return HashCode.Combine(this.Unit.GetType(), bashValue);
        }

        public override string ToString()
        {
            return $"{this.Value} {this.Symbol}";
        }

        public static Quantity<T> operator +(Quantity<T> a, Quantity<T> b) => a.Add(b);

        public static Quantity<T> operator -(Quantity<T> a, Quantity<T> b) => a.Subtract(b);
    }
}
