// https://adventofcode.com/2024/day/1#part2

string[] puzzleinput = File.ReadAllLines(@"..\input.txt");

int list_length = puzzleinput.Length;

var a = new List<int>();
var b = new Dictionary<int,int>();

int total_difference = 0;

for(int i = 0; i < list_length; i++)
{
    string[] x = puzzleinput[i].Split("   ");
    
    a.Add(Convert.ToInt32(x[0]));

    int n2 = Convert.ToInt32(x[1]);

    if(b.ContainsKey(n2))
    {  
        b[n2]++;
    }
    else
    {
        b.Add(n2, 1);
    }
}

int multiplier = 0;

foreach(var num in a)
{
    multiplier = 0;
    if(b.ContainsKey(num))
    {
        multiplier = b[num];
    }

    total_difference += num * multiplier;
}

Console.WriteLine(total_difference);
