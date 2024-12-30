// https://adventofcode.com/2024/day/3

using System.Text.RegularExpressions;

string[] puzzleinput = File.ReadAllLines(@"..\input.txt");

int sum = 0;
Regex r = new Regex(@"mul\(\d{1,3},\d{1,3}\)");

foreach(string s in puzzleinput)
{
    var matches = r.Matches(s);

    foreach(Match m in matches)
    {
        Regex r2 = new Regex(@"\d{1,3}");
        var matches2 = r2.Matches(m.Value);

        sum += Convert.ToInt32(matches2[0].Value) * Convert.ToInt32(matches2[1].Value);
    }
}

Console.WriteLine(sum);