namespace CompresorUniversal.Core.Modelos
{
    public class FileEntry
    {
        public string FileName { get; set; } = "";
        public int OriginalSize { get; set; }
        public int CompressedSize { get; set; }
        public byte[] CompressedData { get; set; } = Array.Empty<byte>();
    }
}