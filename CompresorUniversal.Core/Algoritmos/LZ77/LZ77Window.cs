namespace CompresorUniversal.Core.Algoritmos.LZ77
{
    public class LZ77Window
    {
        public int WindowSize { get; }

        public LZ77Window(int size = 1024)
        {
            WindowSize = size;
        }

        public (int offset, int length) FindLongestMatch(string text, int currentIndex)
        {
            int maxLength = 0;
            int bestOffset = 0;

            int start = Math.Max(0, currentIndex - WindowSize);

            for (int i = start; i < currentIndex; i++)
            {
                int length = 0;

                while (currentIndex + length < text.Length &&
                       text[i + length] == text[currentIndex + length])
                {
                    length++;
                    if (i + length >= currentIndex) break;
                }

                if (length > maxLength)
                {
                    maxLength = length;
                    bestOffset = currentIndex - i;
                }
            }

            return (bestOffset, maxLength);
        }
    }
}