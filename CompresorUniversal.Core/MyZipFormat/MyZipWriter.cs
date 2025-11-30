using CompresorUniversal.Core.Interfaces;
using CompresorUniversal.Core.Modelos;
using System.Diagnostics;
using System.Text;

namespace CompresorUniversal.Core.MyZipFormat
{
    public static class MyZipWriter
    {
        public static CompressionResult CompressFiles(
            List<string> files,
            ICompresor compressor)
        {
            Stopwatch watch = new();
            watch.Start();

            long memBefore = GC.GetTotalMemory(true);

            List<FileEntry> entries = new();

            foreach (string path in files)
            {
                string text = File.ReadAllText(path);
                byte[] compressed = compressor.Compress(text);

                entries.Add(new FileEntry
                {
                    FileName = Path.GetFileName(path),
                    OriginalSize = Encoding.UTF8.GetByteCount(text),
                    CompressedSize = compressed.Length,
                    CompressedData = compressed
                });
            }

            byte[] finalBytes = BuildMyZip(compressor, entries);

            long memAfter = GC.GetTotalMemory(true);

            watch.Stop();

            double originalTotal = entries.Sum(e => e.OriginalSize);
            Console.WriteLine(originalTotal);
            double compressedTotal = entries.Sum(e => e.CompressedSize);
            Console.WriteLine(compressedTotal);
            return new CompressionResult
            {
                FileBytes = finalBytes,
                Stats = new Statistics
                {
                    TimeMilliseconds = watch.ElapsedMilliseconds,
                    MemoryBytes = memAfter - memBefore,
                    CompressionRatio = (1  - (compressedTotal  / originalTotal)) * 100
                }
            };
        }

        private static byte[] BuildMyZip(
            ICompresor compressor,
            List<FileEntry> entries)
        {
            using MemoryStream ms = new();
            using BinaryWriter bw = new(ms);

            // Algoritmo (según orden del enum)
            byte algo = (byte)Enum.Parse(
                typeof(AlgorithmType),
                compressor.GetType().Name.Replace("Compressor","")
            );

            bw.Write(algo);

            // Número de archivos
            bw.Write(entries.Count);

            foreach (var entry in entries)
            {
                byte[] nameBytes = Encoding.UTF8.GetBytes(entry.FileName);

                bw.Write(nameBytes.Length);
                bw.Write(nameBytes);

                bw.Write(entry.OriginalSize);
                bw.Write(entry.CompressedSize);
                bw.Write(entry.CompressedData);
            }

            return ms.ToArray();
        }
    }
}
