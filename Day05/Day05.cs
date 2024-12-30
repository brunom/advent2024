using System.Collections.Immutable;
using System.Diagnostics;
using System.IO.MemoryMappedFiles;
using System.Text;

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
            var updates = file[(2 + iEmptyLine)..(file.Length - 1)];
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
                sum += int.Parse(update[(update.Length / 2 - 1)..(update.Length / 2 + 1)]);
            unordered: { }
            }
            Assert.Equal(5991, sum);
        }
        [Fact]
        public unsafe void Test2()
        {
            var rules =
                File.ReadLines("input.txt")
                .TakeWhile(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => (int.Parse(line.Split('|')[0]), int.Parse(line.Split('|')[1])))
                .ToImmutableList();
            var updates =
                File.ReadLines("input.txt")
                .SkipWhile(line => !string.IsNullOrWhiteSpace(line))
                .Skip(1)
                .Select(line => line.Split(',').Select(p => int.Parse(p)).ToList())
                .ToImmutableList();
            int sum = 0;
            foreach (var update in updates)
            {
                List<int> ordered = new();
                while (ordered.Count < update.Count)
                {
                    int next =
                        update
                        .Except(ordered)
                        .Where(p => rules.All(r => r.Item2 != p || ordered.Contains(r.Item1) || !update.Contains(r.Item1)))
                        .Single();
                    ordered.Add(next);
                }
                if (!ordered.SequenceEqual(update))
                {
                    sum += ordered[ordered.Count / 2];
                }
            }
            Assert.Equal(5479, sum);

        }
    }
}
