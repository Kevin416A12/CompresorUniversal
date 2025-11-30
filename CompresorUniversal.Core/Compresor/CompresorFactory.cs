using CompresorUniversal.Core.Algoritmos.Huffman;
using CompresorUniversal.Core.Algoritmos.LZ77;
using CompresorUniversal.Core.Algoritmos.LZ78;
using CompresorUniversal.Core.Interfaces;

namespace CompresorUniversal.Core.Compresor
{
    public static class CompressorFactory
    {
        public static ICompresor Create(AlgorithmType type)
        {
            return type switch
            {
                AlgorithmType.Huffman => new HuffmanCompressor(),
                AlgorithmType.LZ77 => new LZ77Compressor(),
                AlgorithmType.LZ78 => new LZ78Compressor(),
                _ => throw new ArgumentException("Algoritmo no soportado.")
            };
        }

        // Para el MyZipWriter (identificación por nombre)
        public static AlgorithmType GetAlgorithmType(ICompresor compressor)
        {
            return compressor switch
            {
                HuffmanCompressor => AlgorithmType.Huffman,
                LZ77Compressor => AlgorithmType.LZ77,
                LZ78Compressor => AlgorithmType.LZ78,
                _ => throw new ArgumentException("Compresor desconocido.")
            };
        }
    }
}