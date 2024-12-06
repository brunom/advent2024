using System.Diagnostics;
using System.IO.MemoryMappedFiles;

namespace Day05
{
    public class Day05
    {
        [Fact]
        public unsafe void Test1()
        {
            using var fileStream = File.Open("input.txt", FileMode.Open, FileAccess.Read, FileShare.Read);
            using var mmf = MemoryMappedFile.CreateFromFile(fileStream, null, 0, MemoryMappedFileAccess.Read, HandleInheritability.None, true);
            using var view = mmf.CreateViewAccessor(0, 0, MemoryMappedFileAccess.Read);
            byte* pointer = null;
            view.SafeMemoryMappedViewHandle.AcquirePointer(ref pointer);
            ReadOnlySpan<byte> file = new(pointer, checked((int)fileStream.Length));

            var iEmptyLine = file.IndexOf([(byte)'\n', (byte)'\n']);
            var rules = file[..iEmptyLine]; // Don't include final eol
            var updates = file[(2 + iEmptyLine)..(file.Length-1)];
            int sum = 0;
            foreach (var rupdates in updates.Split((byte)'\n'))
            {
                var update = updates[rupdates];
                foreach (var rrules in rules.Split((byte)'\n'))
                {
                    var rule = rules[rrules];
                    Trace.Assert(rule.Length == 5);
                    var fst = rule[0..2];
                    var snd = rule[3..5];
                    int ifst = update.IndexOf(fst);
                    int isnd = update.IndexOf(snd);
                    if (ifst != -1 && isnd != -1 && isnd < ifst)
                        goto unordered;
                }
                sum += int.Parse(update[(update.Length / 2-1)..(update.Length / 2+1)]);
            unordered: { }
            }
            Assert.Equal(5991, sum);
        }
    }
}
