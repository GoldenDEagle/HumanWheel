namespace Assets.Codebase.Utils.Helpers
{
    public static class NumberConverter
    {
        public static string Convert(float value)
        {
            return value switch
            {
                < 10000f => $"{System.Convert.ToInt32(value)}",
                < 1000000f => $"{(int)(value / 1000)}к",
                < 1000000000f => $"{(int)(value / 1000000)}.{(int)(value % 1000000) / 10000}м",
                < 1000000000000f => $"{(int)(value / 1000000000)}.{(int)(value % 1000000000) / 10000000}мм",
                < 1000000000000000f => $"{(int)(value / 1000000000000)}.{(int)(value % 1000000000000) / 10000000000}т",
                _ => value.ToString()
            };
        }
    }
}
