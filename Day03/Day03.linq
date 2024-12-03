<Query Kind="Statements" />

string input = @"xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";
bool enabled = true;
int count = 0;
foreach (var m in Regex.Matches(input, @"mul\((\d{1,3}),(\d{1,3})\)|do\(\)|don't\(\)").Cast<Match>())
{
	if (m.Value == "do()")
		enabled = true;
	else if (m.Value == "don't()")
		enabled = false;
	else if (enabled)
		count += int.Parse(m.Groups[1].Value) * int.Parse(m.Groups[2].Value);
}
count.Dump();


