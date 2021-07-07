namespace UOM
{
    public sealed class Length : Unit
    {
        public static readonly Length Millimetre = new Length("mm", 1000m);
        public static readonly Length Centimetre = new Length("cm", 100m);
        public static readonly Length Decimetre = new Length("dm", 10m);
        public static readonly Length Metre = new Length("m", 1m);
        public static readonly Length Kilometre = new Length("km", 0.001m);

        public static readonly Length Inch = new Length("in", 1/0.0254m);
        public static readonly Length Feet = new Length("ft", 1/0.3048m);
        public static readonly Length Yards = new Length("yd", 1.0936132983m);
        public static readonly Length Miles = new Length("mi", 0.0006213711922m);

        public static readonly Length SI = Metre;

        private Length(string symbol, decimal factor, decimal offset = 0)
            : base(symbol, factor, offset)
        {
        }
    }
}
