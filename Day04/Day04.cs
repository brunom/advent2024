using System.IO.MemoryMappedFiles;
using System.Text.RegularExpressions;

namespace Day04
{
    public unsafe class Day04
    {
        [Fact]
        public void Test1()
        {
            using var mmf = MemoryMappedFile.CreateFromFile("input.txt");
            using var view = mmf.CreateViewAccessor();
            byte* pointer = null;
            view.SafeMemoryMappedViewHandle.AcquirePointer(ref pointer);
            ReadOnlySpan<byte> file = new(
                pointer, (int)view.SafeMemoryMappedViewHandle.ByteLength);

            // works with extra garbage like eol and zeroes at the end
            int cols = file.IndexOf((byte)'\n') + 1;
            int rows = file.Length / cols;

            (int r, int c)[] ms = [(1, 0), (0, 1), (1, 1), (-1, 1),];

            var q =
                from r in Enumerable.Range(0, rows)
                from c in Enumerable.Range(0, cols)
                from m in ms
                group (r, c) by (m, r * m.r + c * m.c) into g
                select string.Join("",
                    from e in g
                    orderby e.c, e.r
                    select (char)pointer[e.r * cols + e.c]);

            Assert.Equal(2603, q.Select(s => Regex.Matches(s, "XMAS").Count() + Regex.Matches(s, "SAMX").Count()).Sum());

            int count = 0;
            for (int r = 1; r < rows - 1; r++)
            {
                for (int c = 1; c < cols - 1; ++c)
                {
                    if (file[r * cols + c] != (byte)'A')
                        continue;
                    byte NE = file[(r - 1) * cols + c + 1];
                    byte SE = file[(r + 1) * cols + c + 1];
                    byte SW = file[(r + 1) * cols + c - 1];
                    byte NW = file[(r - 1) * cols + c - 1];

                    if (
                        (NE == (byte)'M' && SW == (byte)'S' || NE == (byte)'S' && SW == (byte)'M') &&
                        (SE == (byte)'M' && NW == (byte)'S' || SE == (byte)'S' && NW == (byte)'M'))
                        count += 1;
                }
            }
            Assert.Equal(1965, count);
        }
    }
}
