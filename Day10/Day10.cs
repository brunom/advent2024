namespace Day10
{
    public class Day10
    {
        [Fact]
        public void Test1()
        {
            var lines = File.ReadAllLines("input.txt");
            var lines = lines;
            int rows = lines.Length;
            int cols = lines[0].Length;
            bool Inside(int r, int c)
            {
                return
                    0 <= r && r < rows &&
                    0 <= c && c < cols;
            }
            static IEnumerable<(int r, int c)> Next(int r, int c)
            {
                yield return (r, c + 1);
                yield return (r, c - 1);
                yield return (r + 1, c);
                yield return (r - 1, c);
            }
            IEnumerable<(int r, int c)> Find(int height)
            {
                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < cols; c++)
                    {
                        if (lines[r][c] == height)
                            yield return (c, r);
                    }
                }
            }
            var score = Find('9').ToDictionary(rc => rc, rc => 1);
            for (int height = '
        }
    }
}
