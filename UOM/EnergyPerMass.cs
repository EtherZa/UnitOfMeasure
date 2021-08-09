namespace UOM
{
    public sealed class EnergyPerMass : Unit
    {
        public static readonly EnergyPerMass KiloWattHourPerKilogram = new EnergyPerMass("kWh/kg", 1);
        public static readonly EnergyPerMass KiloWattHourPerTon = new EnergyPerMass("kWh/t", 0.001m);

        public static readonly EnergyPerMass KiloWattHourPerPound = new EnergyPerMass("kWh/lb", 100_000_000 / 45_359_237m);
        public static readonly EnergyPerMass KiloWattHourPerShortTon = new EnergyPerMass("kWh/sht", 50_000 / 45_359_237m);
        public static readonly EnergyPerMass KiloWattHourPerLongTon = new EnergyPerMass("kWh/lt", 312_500 / 317_514_659m);

        private EnergyPerMass(string symbol, decimal factor, decimal offset = 0)
            : base(symbol, factor, offset)
        {
        }
    }
}
