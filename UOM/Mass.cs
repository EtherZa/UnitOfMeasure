namespace UOM
{
    public sealed class Mass : Unit
    {
        public static readonly Mass Gram = new Mass("g", 1000m);
        public static readonly Mass Kilogram = new Mass("kg", 1m);
        public static readonly Mass Tonne = new Mass("t", 0.001m);

        public static readonly Mass Ounce = new Mass("oz", 35.2739619495804m);
        public static readonly Mass Pound = new Mass("lb", 2.20462262184878m);

        public static readonly Mass SI = Kilogram;

        private Mass(string symbol, decimal factor, decimal offset = 0)
            : base(symbol, factor, offset)
        {
        }
    }
}
