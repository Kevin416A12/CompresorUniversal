namespace CompresorUniversal.Core.Modelos
{
    public class CompressionResult
    {
        public byte[] FileBytes { get; set; } = Array.Empty<byte>();
        public Statistics Stats { get; set; } = new();
    }
}