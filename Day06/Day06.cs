using System.Collections.Immutable;
using System.Diagnostics;
using System.IO.MemoryMappedFiles;
using System.Text;

namespace Day06
{
    public class Day06
    {
        (int row, int col) Forward(int row, int col, char dir)
        {
            return dir switch
            {
                '^' => (row - 1, col + 0),
                'v' => (row + 1, col + 0),
                '<' => (row + 0, col - 1),
                '>' => (row + 0, col + 1),
                _ => throw new InvalidDataException(),
            };
        }
        char Turn(char dir)
        {
            return dir switch
            {
                '^' => '>',
                '>' => 'v',
                'v' => '<',
                '<' => '^',
                _ => throw new InvalidDataException(),
            };
        }
        [Fact]
        public void Test1()
        {
            var map =
                File.ReadLines("input.txt")
                .Select(line => line.Where(ch => !char.IsWhiteSpace(ch)).ToArray())
                .ToArray();
            int rows = map.Length;
            int cols = map[0].Length;
            bool Outside(int row, int col) => row < 0 || row >= rows || col < 0 || col >= cols;
            (int row, int col) pos = (0, 0);
            for (int row = 0; row < rows; ++row)
            {
                for (int col = 0; col < cols; ++col)
                {
                    switch (map[row][col])
                    {
                        case '^':
                            pos = (row, col);
                            break;
                    }
                }
            }
            char dir = '^'; // starts up
            HashSet<(int row, int col, char dir)> route = [];
            while (true)
            {
                if (!route.Add((pos.row, pos.col, dir)))
                    break;
                var forward = Forward(pos.row, pos.col, dir);
                if (Outside(forward.row, forward.col))
                    break;
                if (map[forward.row][forward.col] == '#')
                {
                    dir = Turn(dir);
                }
                else
                {
                    pos = forward;
                }
            }
            Assert.Equal(4454, route.Select(x => (x.row, x.col)).Distinct().Count());
        }
    }
}
