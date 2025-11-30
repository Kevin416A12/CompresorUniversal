using CompresorUniversal.Core.Interfaces;
using CompresorUniversal.Core.Modelos;
using CompresorUniversal.Core.Compresor;
using System.Diagnostics;
using System.Text;

namespace CompresorUniversal.Core.MyZipFormat
{
    public static class MyZipReader
    {
        public static DecompressionResult Decompress(string filePath)
        {
            Stopwatch watch = new();
            watch.Start();

            long memBefore = GC.GetTotalMemory(true);

            byte[] data = File.ReadAllBytes(filePath);

            using MemoryStream ms = new(data);
            using BinaryReader br = new(ms);

            // algoritmo
            byte algo = br.ReadByte();
            ICompresor compressor = CompressorFactory.Create((AlgorithmType)algo);

            int fileCount = br.ReadInt32();

            string outputFolder = Path.Combine(
                Path.GetDirectoryName(filePath)!,
                Path.GetFileNameWithoutExtension(filePath) + "_unpacked"
            );

            Directory.CreateDirectory(outputFolder);

            for (int i = 0; i < fileCount; i++)
            {
                int nameSize = br.ReadInt32();
                string name = Encoding.UTF8.GetString(br.ReadBytes(nameSize));

                int originalSize = br.ReadInt32();
                int compressedSize = br.ReadInt32();

                byte[] compressedData = br.ReadBytes(compressedSize);

                string decompressed = compressor.Decompress(compressedData);

                File.WriteAllText(Path.Combine(outputFolder, name), decompressed);
            }

            long memAfter = GC.GetTotalMemory(true);

            watch.Stop();

            return new DecompressionResult
            {
                OutputFolder = outputFolder,
                Stats = new Statistics
                {
                    TimeMilliseconds = watch.ElapsedMilliseconds,
                    MemoryBytes = memAfter - memBefore,
                    CompressionRatio = 0 // no aplica a descompresión
                }
            };
        }
    }
}
