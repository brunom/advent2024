[TestClass]
public sealed class Day01
{
    [TestMethod]
    public void TestMethod1()
    {
        var actual =
            from line in File.ReadLines("input.txt")
            let s = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            group s by 0 into g
            let left = g.Select(s => int.Parse(s[0]))
            let right = g.Select(s => int.Parse(s[1]))
            select left.Order().Zip(right.Order()).Select(p => Math.Abs(p.First - p.Second)).Sum();

        Assert.AreEqual(2066446, actual.Single());
    }
    [TestMethod]
    public void TestMethod2()
    {
        var actual =
            from line in File.ReadLines("input.txt")
            let s = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            group s by 0 into g
            let left = g.Select(s => int.Parse(s[0]))
            let right = g.Select(s => int.Parse(s[1]))
            select left.GroupJoin(right, x => x, x => x, (l, rs) => l * rs.Count()).Sum();

        Assert.AreEqual(24931009, actual.Single());
    }
}
