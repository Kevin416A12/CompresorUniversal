namespace CompresorUniversal.Core.Algoritmos.LZ78
{
    public class LZ78Entry
    {
        public int Index { get; set; }
        public char NextChar { get; set; }

        public override string ToString()
        {
            return $"{Index},{NextChar}";
        }

        public static LZ78Entry FromString(string s)
        {
            var parts = s.Split(',');
            return new LZ78Entry
            {
                Index = int.Parse(parts[0]),
                NextChar = parts[1][0]
            };
        }
    }
}