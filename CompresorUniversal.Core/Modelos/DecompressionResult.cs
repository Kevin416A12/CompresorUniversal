namespace CompresorUniversal.Core.Modelos
{
    public class DecompressionResult
    {
        public string OutputFolder { get; set; } = "";
        public Statistics Stats { get; set; } = new();
    }
}