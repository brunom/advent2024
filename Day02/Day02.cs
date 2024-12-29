using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Day02
{
    public class Day02
    {
        [Fact]
        public void Test1()
        {
            var q =
                from line in File.ReadLines("input.txt")
                let levels = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToList()
                where Increasing(levels) || Increasing(levels.AsEnumerable().Reverse().ToList())
                select levels;

            Assert.Equal(218, q.Count());
        }
        static bool Increasing(int level0, int level1) => 1 <= level1 - level0 && level1 - level0 <= 3;
        static bool Increasing(List<int> levels, int i = 0)
        {
            for (; i < levels.Count - 1; ++i)
            {
                if (!Increasing(levels[i], levels[i + 1]))
                    return false;
            }
            return true;
        }
        static bool IncreasingBut(List<int> levels)
        {
            // TODO IEnumerable parameter
            if (levels.Count < 3) return true;
            int i = 0;
            int level0 = levels[i++];
            int level1 = levels[i++];
            int level2 = levels[i++];
            bool inc01 = Increasing(level0, level1);
            bool inc12 = Increasing(level1, level2);
            if (!inc01)
            {
                return (inc12 || Increasing(level0, level2)) && Increasing(levels, i - 1);
            }
            while (true)
            {
                if (i == levels.Count) return true;
                int level3 = levels[i++];
                bool inc23 = Increasing(level2, level3);
                if (inc12)
                {
                    level0 = level1;
                    level1 = level2;
                    level2 = level3;
                    inc01 = inc12;
                    inc12 = inc23;
                }
                else
                {
                    return (Increasing(level1, level3) || Increasing(level0, level2) && inc23) && Increasing(levels, i - 1);
                }
            }
        }
        [Fact]
        public void Test2()
        {
            int count = 0;
            foreach (var line in File.ReadLines("input.txt"))
            {
                var levelsA =
                    line
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => int.Parse(s))
                    .ToList();
                var levelsB =
                    levelsA
                    .AsEnumerable()
                    .Reverse()
                    .ToList();
                if (IncreasingBut(levelsA))
                    count++;
                else if (IncreasingBut(levelsB))
                    count++;
                else
                    "".ToString();
            }
            Assert.Equal(290, count);
        }
    }
}
