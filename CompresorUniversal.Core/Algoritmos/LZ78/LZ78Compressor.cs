using CompresorUniversal.Core.Interfaces;
using System.Text;
using CompresorUniversal.Core.Algoritmos.LZ78;

namespace CompresorUniversal.Core.Algoritmos.LZ78
{
    public class LZ78Compressor : ICompresor
    {
        public byte[] Compress(string input)
        {
            Dictionary<string, int> dictionary = new();
            List<LZ78Entry> entries = new();

            int dictIndex = 1;
            int i = 0;

            while (i < input.Length)
            {
                string current = input[i].ToString();
                int prevIndex = 0;

                // Extender mientras exista en el diccionario
                while (dictionary.ContainsKey(current))
                {
                    prevIndex = dictionary[current];
                    i++;

                    if (i >= input.Length)
                        break;

                    current += input[i];
                }

                char nextChar;

                if (current.Length == 1 && !dictionary.ContainsKey(current))
                {
                    nextChar = current[0];
                }
                else
                {
                    nextChar = current[^1];
                }

                entries.Add(new LZ78Entry
                {
                    Index = prevIndex,
                    NextChar = nextChar
                });

                dictionary[current] = dictIndex++;
                i++;
            }

            StringBuilder sb = new();
            foreach (var e in entries)
                sb.Append(e + ";");

            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        public string Decompress(byte[] data)
        {
            string compressed = Encoding.UTF8.GetString(data);

            var tokens = compressed.Split(';',
                StringSplitOptions.RemoveEmptyEntries);

            List<LZ78Entry> entries = tokens
                .Select(LZ78Entry.FromString)
                .ToList();

            List<string> dictionary = new() { "" };
            StringBuilder output = new();

            foreach (var e in entries)
            {
                string entryString;

                if (e.Index == 0)
                {
                    entryString = e.NextChar.ToString();
                }
                else
                {
                    entryString = dictionary[e.Index] + e.NextChar;
                }

                dictionary.Add(entryString);
                output.Append(entryString);
            }

            return output.ToString();
        }
    }
}
