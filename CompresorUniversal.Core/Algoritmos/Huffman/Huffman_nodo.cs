namespace CompresorUniversal.Core.Algoritmos.Huffman
{
    public class Huffman_nodo
    {
        public string Symbol { get; set; } = "";
        public int Frequency { get; set; }

        public Huffman_nodo? Left { get; set; }
        public Huffman_nodo? Right { get; set; }

        public bool IsLeaf => Left == null && Right == null;
    }
}