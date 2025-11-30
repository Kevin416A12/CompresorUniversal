namespace CompresorUniversal.Core.Algoritmos.LZ77
{
    public class LZ77Tupla
    {
        public int Offset { get; set; }
        public int Length { get; set; }
        public char NextChar { get; set; }

        public override string ToString()
        {
            return $"{Offset},{Length},{NextChar}";
        }

        public static LZ77Tupla FromString(string s)
        {
            var parts = s.Split(',');
            return new LZ77Tupla
            {
                Offset = int.Parse(parts[0]),
                Length = int.Parse(parts[1]),
                NextChar = parts[2][0]
            };
        }
    }
}