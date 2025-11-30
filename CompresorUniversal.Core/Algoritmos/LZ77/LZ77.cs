using System.Text;
using CompresorUniversal.Core.Interfaces;

namespace CompresorUniversal.Core.Algoritmos.LZ77
{
    public class LZ77Compressor : ICompresor
    {
        private const int DEFAULT_WINDOW = 1024;

        public byte[] Compress(string input)
        {
            var window = new LZ77Window(DEFAULT_WINDOW);
            List<LZ77Tupla> tuples = new();

            int index = 0;

            while (index < input.Length)
            {
                var (offset, length) = window.FindLongestMatch(input, index);

                char nextChar = (index + length < input.Length)
                    ? input[index + length]
                    : '\0';

                tuples.Add(new LZ77Tupla
                {
                    Offset = offset,
                    Length = length,
                    NextChar = nextChar
                });

                index += length + 1;
            }

            StringBuilder sb = new();
            foreach (var t in tuples)
                sb.Append(t + ";");

            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        public string Decompress(byte[] data)
        {
            string compressed = Encoding.UTF8.GetString(data);

            var entries = compressed.Split(';',
                StringSplitOptions.RemoveEmptyEntries);

            List<LZ77Tupla> tuples = entries
                .Select(LZ77Tupla.FromString)
                .ToList();

            StringBuilder output = new();

            foreach (var t in tuples)
            {
                if (t.Offset == 0 && t.Length == 0)
                {
                    output.Append(t.NextChar);
                    continue;
                }

                int start = output.Length - t.Offset;

                for (int i = 0; i < t.Length; i++)
                    output.Append(output[start + i]);

                if (t.NextChar != '\0')
                    output.Append(t.NextChar);
            }

            return output.ToString();
        }
    }
}