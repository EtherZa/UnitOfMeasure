namespace UOM
{
    public sealed class Volume : Unit
    {
        public static readonly Volume CubicMillimetre = new Volume("mm³", 1_000_000_000m);
        public static readonly Volume CubicCentimetre = new Volume("cm³", 1_000_000m);
        public static readonly Volume CubicDecimetre = new Volume("dm³", 1_000m);
        public static readonly Volume CubicMetre = new Volume("m³", 1m);
        public static readonly Volume CubicKilometre = new Volume("km³", 1/1_000_000_000m);

        public static readonly Volume SI = CubicMetre;

        private Volume(string symbol, decimal factor, decimal offset = 0)
            : base(symbol, factor, offset)
        {
        }
    }
}
