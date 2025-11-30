namespace CompresorUniversal.Core.Algoritmos.Huffman
{
    public class BitReader
    {
        private byte[] data;
        private int byteIndex = 0;
        private int bitIndex = 0;

        public BitReader(byte[] data) => this.data = data;

        public bool TryReadBit(out bool bit)
        {
            if (byteIndex >= data.Length)
            {
                bit = false;
                return false;
            }

            bit = (data[byteIndex] & (1 << (7 - bitIndex))) != 0;

            bitIndex++;
            if (bitIndex == 8)
            {
                bitIndex = 0;
                byteIndex++;
            }

            return true;
        }
    }
}