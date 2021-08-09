namespace UOM
{
    using System;

    public class Unit
    {
        protected Unit(string symbol, decimal factor, decimal offset)
        {
            this.Offset = offset;
            this.Factor = factor;
            this.Symbol = symbol;
        }

        internal decimal Offset { get; }
        internal decimal Factor { get; }
        public string Symbol { get; }

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
