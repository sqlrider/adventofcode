// https://adventofcode.com/2024/day/3#part2

using System.Text.RegularExpressions;

string[] puzzleinput = File.ReadAllLines(@"..\input.txt");

var commands = new List<string>();

int sum = 0;
Regex r = new Regex(@"mul\(\d{1,3},\d{1,3}\)|do\(\)|don't\(\)");

foreach(string s in puzzleinput)
{
    var matches = r.Matches(s);

    foreach(Match m in matches)
    {
        commands.Add(m.Value);
    }
}

bool enabled = true;

foreach(string c in commands)
{
    if(c == "do()")
        enabled = true;
    else if(c == "don't()")
        enabled = false;
    else
    {
        if(enabled)
        {
            Regex r2 = new Regex(@"\d{1,3}");
            var matches2 = r2.Matches(c);
            sum += Convert.ToInt32(matches2[0].Value) * Convert.ToInt32(matches2[1].Value);
        }
    }
}

Console.WriteLine(sum);