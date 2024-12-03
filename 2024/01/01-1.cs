// https://adventofcode.com/2024/day/1

string[] puzzleinput = File.ReadAllLines(@"..\input.txt");

int list_length = puzzleinput.Length;

var a = new List<int>();
var b = new List<int>();
int total_difference = 0;

for(int i = 0; i < list_length; i++)
{
    string[] x = puzzleinput[i].Split("   ");
    a.Add(Convert.ToInt32(x[0]));
    b.Add(Convert.ToInt32(x[1]));
}

a.Sort();
b.Sort();

for(int i = 0; i < list_length; i++)
{
    total_difference += Math.Abs(a[i] - b[i]);
}

Console.WriteLine(total_difference);
