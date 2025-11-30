using System.Globalization;
using CompresorUniversal.Core.Interfaces;
using System.Text;

namespace CompresorUniversal.Core.Algoritmos.Huffman
{
    public class HuffmanCompressor : ICompresor
    {
        
        private IEnumerable<string> SplitSymbols(string input)
        {
            List<string> list = new();
            var enumerator = StringInfo.GetTextElementEnumerator(input);

            while (enumerator.MoveNext())
                list.Add(enumerator.GetTextElement());

            return list;
        }

        public byte[] Compress(string input)
        {
            var symbols = SplitSymbols(input).ToList();

            HuffmanTree tree = new();
            tree.Build(symbols);

           
            BitWriter writer = new();

            foreach (var sym in symbols)
                writer.WriteBits(tree.Codes[sym]);

            byte[] compressedBits = writer.GetBytes();

            using MemoryStream ms = new();
            using BinaryWriter bw = new(ms, Encoding.UTF8, true);

            
            SaveTree(tree.Root!, bw);

           
            bw.Write(symbols.Count);

       
            bw.Write(compressedBits.Length);

          
            bw.Write(compressedBits);

            return ms.ToArray();
        }

        public string Decompress(byte[] data)
        {
            using MemoryStream ms = new(data);
            using BinaryReader br = new(ms, Encoding.UTF8);

        
            Huffman_nodo root = LoadTree(br);

           
            int totalSymbols = br.ReadInt32();

       
            int len = br.ReadInt32();
            byte[] compressedBits = br.ReadBytes(len);

            BitReader reader = new(compressedBits);

            StringBuilder output = new();
            Huffman_nodo current = root;

            for (int i = 0; i < totalSymbols; i++)
            {
                while (!current.IsLeaf)
                {
                    reader.TryReadBit(out bool bit);
                    current = bit ? current.Right! : current.Left!;
                }

                output.Append(current.Symbol);
                current = root;
            }

            return output.ToString();
        }


        private void SaveTree(Huffman_nodo node, BinaryWriter bw)
        {
            if (node.IsLeaf)
            {
                bw.Write((byte)1);
                bw.Write(node.Symbol);
            }
            else
            {
                bw.Write((byte)0);
                SaveTree(node.Left!, bw);
                SaveTree(node.Right!, bw);
            }
        }

        private Huffman_nodo LoadTree(BinaryReader br)
        {
            byte flag = br.ReadByte();

            if (flag == 1)
                return new Huffman_nodo { Symbol = br.ReadString() };

            return new Huffman_nodo
            {
                Left = LoadTree(br),
                Right = LoadTree(br)
            };
        }
    }
}
