using System.Diagnostics;

namespace Day09
{
    public class Day09
    {
        [Fact]
        public void Test1()
        {
            string file = File.ReadAllText("input.txt");
            Trace.Assert(file.Length % 2 == 1); // starts and ends with a file
            int IDs = file.Length / 2;
            int iBlock = 0;
            Int64 checksum = 0;
            for (int ID = 0; ID < IDs; ++ID)
            {
                checksum += ID * 
            }
            Assert.Equal(-1, checksum);

        }
    }
}
