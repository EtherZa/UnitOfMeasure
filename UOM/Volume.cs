namespace UOM
{
    public sealed class Volume : Unit
    {
        public static readonly Volume CubicMillimetre = new Volume("mm�", 1_000_000_000m);
        public static readonly Volume CubicCentimetre = new Volume("cm�", 1_000_000m);
        public static readonly Volume CubicDecimetre = new Volume("dm�", 1_000m);
        public static readonly Volume CubicMetre = new Volume("m�", 1m);
        public static readonly Volume CubicKilometre = new Volume("km�", 1/1_000_000_000m);

        public static readonly Volume SI = CubicMetre;

        private Volume(string symbol, decimal factor, decimal offset = 0)
            : base(symbol, factor, offset)
        {
        }
    }
}
