namespace UOM
{
    public sealed class Density : Unit
    {
        public static readonly Density GramPerCm3 = new Density("g/cm³", 1000m);
        public static readonly Density KilogramPerM3 = new Density("kg/m³", 1000m);
        public static readonly Density TonsPerM3 = new Density("t/m³", 1m);

        public static readonly Density SI = GramPerCm3;

        private Density(string symbol, decimal factor, decimal offset = 0)
            : base(symbol, factor, offset)
        {
        }
    }
}
