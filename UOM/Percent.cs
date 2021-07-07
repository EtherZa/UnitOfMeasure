namespace UOM
{
    public class Percent : Unit
    {
        public static readonly Percent Default = new Percent("%", 0m);

        private Percent(string symbol, decimal factor, decimal offset = 0)
            : base(symbol, factor, offset)
        {
        }
    }
}
