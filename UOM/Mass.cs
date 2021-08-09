namespace UOM
{
    public sealed class Mass : Unit
    {
        public static readonly Mass Gram = new Mass("g", 1000m);
        public static readonly Mass Kilogram = new Mass("kg", 1m);
        public static readonly Mass Tonne = new Mass("t", 0.001m);

        public static readonly Mass Ounce = new Mass("oz", 1_600_000_000 / 45_359_237m);
        public static readonly Mass Pound = new Mass("lb", 100_000_000 / 45_359_237m);
        public static readonly Mass ShortTon = new Mass("sht", 50_000 / 45_359_237m);
        public static readonly Mass LongTon = new Mass("lt", 312_500 / 317_514_659m);

        public static readonly Mass SI = Kilogram;

        private Mass(string symbol, decimal factor, decimal offset = 0)
            : base(symbol, factor, offset)
        {
        }
    }
}
