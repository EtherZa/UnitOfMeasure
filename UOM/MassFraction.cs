namespace UOM
{
    public sealed class MassFraction : Unit
    {
        public static readonly MassFraction GramsPerTon = new MassFraction("g/t", 1);
        public static readonly MassFraction PartsPerMillion = new MassFraction("ppm", 1);
        public static readonly MassFraction PartsPerBillion = new MassFraction("ppb", 1000);
        public static readonly MassFraction Percent = new MassFraction("%", 0.0001m);

        private MassFraction(string symbol, decimal factor, decimal offset = 0)
            : base(symbol, factor, offset)
        {
        }
    }
}
