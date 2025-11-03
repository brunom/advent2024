using System.Collections.Immutable;
using System.Diagnostics;
using System.IO.MemoryMappedFiles;
using System.Text;

namespace Day06
{
    public class Day06
    {
        readonly string dirs = "^>v<";
        static (int row, int col) Proj((int row, int col, char dir) x)
        {
            return (x.row, x.col);
        }
        static (int row, int col, char dir) Forward((int row, int col, char dir) x)
        {
            return x.dir switch
            {
                '^' => (x.row - 1, x.col + 0, x.dir),
                'v' => (x.row + 1, x.col + 0, x.dir),
                '<' => (x.row + 0, x.col - 1, x.dir),
                '>' => (x.row + 0, x.col + 1, x.dir),
                _ => throw new InvalidDataException(),
            };
        }
        static char Turn(char dir)
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
            bool Outside((int row, int col) x) => x.row < 0 || x.row >= rows || x.col < 0 || x.col >= cols;
            char ReadMap((int row, int col) x) => map[x.row][x.col];
            (int row, int col, char dir) curr = default;
            for (int row = 0; row < rows; ++row)
            {
                for (int col = 0; col < cols; ++col)
                {
                    char ch = map[row][col];
                    if (dirs.Contains(ch))
                        curr = (row, col, ch);
                }
            }
            HashSet<(int row, int col, char dir)> route = [];
            bool RouteContainsAny((int row, int col) x) => dirs.Any(dir => route.Contains((x.row, x.col, dir)));
            (int row, int col, char dir)? obstruction = null;
            HashSet<(int row, int col, char dir)> route_after_obstruction = [];
            int nobstructions = 0;
            while (true)
            {
                if (obstruction == null)
                {
                    // Shouldn't happen because only obstructions generate loops.
                    if (!route.Add(curr))
                        break;
                }
                else
                {
                    if (route.Contains(curr) || !route_after_obstruction.Add(curr))
                    {
                        ++nobstructions;
                        curr = obstruction.Value;
                        obstruction = null;
                        route_after_obstruction.Clear();
                        continue;
                    }
                }
                var forward = Forward(curr);
                if (Outside(Proj(forward)))
                {
                    if (obstruction == null)
                        break;
                    curr = obstruction.Value;
                    obstruction = null;
                    route_after_obstruction.Clear();
                    continue;
                }
                char ch = ReadMap(Proj(forward));
                if (ch == '#' ||
                    obstruction != null && Proj(obstruction.Value) == Proj(forward))
                {
                    curr.dir = Turn(curr.dir);
                    continue;
                }
                // Don't put obstruction where we already tried
                if (RouteContainsAny(Proj(forward)) || obstruction != null)
                {
                    curr = forward;
                    continue;
                }
                obstruction = forward;
                curr.dir = Turn(curr.dir);
            }
            //Assert.Equal(41, route.Select(Proj).Distinct().Count());
            //Assert.Equal(6, nobstructions);

            Assert.Equal(4454, route.Select(Proj).Distinct().Count());
            Assert.Equal(1503, nobstructions);
        }
    }
}
