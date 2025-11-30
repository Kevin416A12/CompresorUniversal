using System.Collections.Generic;
using System.Linq;

namespace CompresorUniversal.Core.Algoritmos.Huffman
{
    public class HuffmanTree
    {
        public Huffman_nodo? Root { get; private set; }
        public Dictionary<string, string> Codes { get; private set; } = new();

        public void Build(IEnumerable<string> symbols)
        {
            var freq = symbols
                .GroupBy(s => s)
                .ToDictionary(g => g.Key, g => g.Count());

            var nodes = freq
                .Select(f => new Huffman_nodo { Symbol = f.Key, Frequency = f.Value })
                .ToList();

            while (nodes.Count > 1)
            {
                var ordered = nodes.OrderBy(n => n.Frequency).Take(2).ToList();

                var parent = new Huffman_nodo
                {
                    Symbol = "",
                    Frequency = ordered[0].Frequency + ordered[1].Frequency,
                    Left = ordered[0],
                    Right = ordered[1]
                };

                nodes.Remove(ordered[0]);
                nodes.Remove(ordered[1]);
                nodes.Add(parent);
            }

            Root = nodes.First();
            Codes.Clear();
            BuildCode(Root, "");
        }

        private void BuildCode(Huffman_nodo? node, string prefix)
        {
            if (node == null) return;

            if (node.IsLeaf)
            {
                Codes[node.Symbol] = prefix;
                return;
            }

            BuildCode(node.Left, prefix + "0");
            BuildCode(node.Right, prefix + "1");
        }
    }
}