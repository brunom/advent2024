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
                where new[] { -1, 1 }.Any(mul =>
                    levels.Zip(levels.Skip(1)).All(p => 1 <= mul * (p.First - p.Second) && mul * (p.First - p.Second) <= 3))
                select levels;

            Assert.Equal(218, q.Count());
        }
        struct Dampener
        {
            int value0;
            int value1;
            int valueCount;
            public int Removed { get; private set; }
            public void OnNext(int value)
            {
                if (valueCount == 0)
                {
                    value1 = value;
                    valueCount = 1;
                }
                else
                {
                    //value1 - value
                }
                throw new NotImplementedException();
            }
        }
        [Fact]
        unsafe public void Test2()
        {
            using var mmf = MemoryMappedFile.CreateFromFile("input.txt");
            using var view = mmf.CreateViewAccessor();
            byte* pointer = null;
            view.SafeMemoryMappedViewHandle.AcquirePointer(ref pointer);
            ReadOnlySpan<byte> file = new(pointer, (int)view.SafeMemoryMappedViewHandle.ByteLength);
            int count = 0;
            foreach (var rFile in file.Split((byte)'\n'))
            {
                var line = file[rFile];
                Dampener inc = new();
                Dampener dec = new();
                foreach (var rLine in line.Split((byte)' '))
                {
                    int level = int.Parse(line[rLine]);
                    inc.OnNext(+level);
                    dec.OnNext(-level);
                }
                if (inc.Removed <= 1 || dec.Removed <= 1)
                    ++count;
            }
        }
    }
}
