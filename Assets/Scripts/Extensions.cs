namespace BeeClicker
{
    public static class Extensions
    {
        public static string KiloFormat(this float num)
        {
            return ((uint)num).KiloFormat();
        }
        public static string KiloFormat(this int num)
        {
            return ((uint)num).KiloFormat();
        }
        public static string KiloFormat(this uint num)
        {
            float fnum = num;

            if(num >= 1000000000) return $"{fnum / 1000000000:0.#}B";
            if(num >= 1000000 * 100) return $"{fnum / 1000000:#}M";
            if(num >= 1000000) return $"{fnum / 1000000:0.#}M";
            if(num >= 1000*100) return $"{fnum / 1000:#}K";
            if(num >= 1000) return $"{fnum / 1000:0.#}K";

            return num.ToString("0");
        }
    }
}
