namespace Day02
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var q =
                from line in File.ReadLines("input.txt")
                let levels = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToList()
                where levels.Zip(levels.Skip(1)).All(p => 1 <= p.First - p.Second && p.First - p.Second <= 3)
                || levels.Zip(levels.Skip(1)).All(p => 1 <= p.Second - p.First && p.Second - p.First <= 3)
                select levels;

            Assert.Equal(218, q.Count());
        }
        [Fact]
        public void Test2()
        {
            var q =
                from line in File.ReadLines("input.txt")
                let levels = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToList()
                let inc = levels.Zip(levels.Skip(1)).Where(p => 1 <= p.First - p.Second && p.First - p.Second <= 3).Count()
                let dec = levels.Zip(levels.Skip(1)).Where(p => 1 <= p.Second - p.First && p.Second - p.First <= 3).Count()
                where levels.Count <= inc + 1 || levels.Count <= dec + 1
                select levels;

            Assert.Equal(-1, q.Count());
        }
    }
}
