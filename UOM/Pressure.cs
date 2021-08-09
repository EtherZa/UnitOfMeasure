namespace UOM
{
    public sealed class Pressure : Unit
    {
        public static readonly Pressure Atmosphere = new Pressure("atm", 1 / 101_325m);
        public static readonly Pressure Bar = new Pressure("bar", 1 / 100_000m);
        public static readonly Pressure InchOfMercury = new Pressure("inhg", 1_000 / 3_386_389m);
        public static readonly Pressure Pascal = new Pressure("pa", 1m);
        public static readonly Pressure KiloPascal = new Pressure("kpa", 0.001m);
        public static readonly Pressure PoundPerSquareInch = new Pressure("psi", 1_290_320_000 / 8_896_443_230_521m);
        public static readonly Pressure Torr = new Pressure("torr", 152 / 20_265m);

        public static readonly Pressure SI = Pascal;

        private Pressure(string symbol, decimal factor, decimal offset = 0)
            : base(symbol, factor, offset)
        {
        }
    }
}
