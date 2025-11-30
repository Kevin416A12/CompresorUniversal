namespace CompresorUniversal.Core.Algoritmos.Huffman
{
    public class BitWriter
    {
        private List<byte> buffer = new();
        private int bitIndex = 0;
        private byte current = 0;

        public void WriteBit(bool bit)
        {
            if (bit)
                current |= (byte)(1 << (7 - bitIndex));

            bitIndex++;

            if (bitIndex == 8)
            {
                buffer.Add(current);
                current = 0;
                bitIndex = 0;
            }
        }

        public void WriteBits(string bits)
        {
            foreach (char c in bits)
                WriteBit(c == '1');
        }

        public byte[] GetBytes()
        {
            if (bitIndex > 0)
                buffer.Add(current);

            return buffer.ToArray();
        }
    }
}