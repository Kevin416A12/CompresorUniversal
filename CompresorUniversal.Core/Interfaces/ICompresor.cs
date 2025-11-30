namespace CompresorUniversal.Core.Interfaces
{
    public interface ICompresor
    {
        byte[] Compress(string input);
        string Decompress(byte[] data);
    }
}