namespace UOM
{
    public sealed class Temperature : Unit
    {
        public static readonly Temperature Kelvin = new Temperature("K", 1m);
        public static readonly Temperature Celcius = new Temperature("°C", 1M, -273.15m);
        public static readonly Temperature Fahrenheit = new Temperature("°F", 9 / 5m, (-273.15m * (9 / 5m)) + 32m);
        public static readonly Temperature Rankine = new Temperature("°R", 9 / 5m);

        public static readonly Temperature SI = Kelvin;

        private Temperature(string symbol, decimal factor, decimal offset = 0)
            : base(symbol, factor, offset)
        {
        }
    }
}
