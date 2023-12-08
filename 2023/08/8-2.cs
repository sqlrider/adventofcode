
string[] puzzleinput = File.ReadAllLines(@"C:\Study\adventofcode\2023\08\input.txt");

var turns = new List<char>();

for(int i = 0; i < puzzleinput[0].Length; i++)
{
    turns.Add(puzzleinput[0][i]);
}

var map = new Dictionary<string, Tuple<string, string>>();

for(int i = 2; i < puzzleinput.Length; i++)
{
    var location = puzzleinput[i].Substring(0,3);
    var dest_tuple = new Tuple<string,string>(puzzleinput[i].Substring(7,3),puzzleinput[i].Substring(12,3));

    map.Add(location,dest_tuple);
}

var current_positions = new List<string>();
var current_steps_to_z = new List<int>();

foreach(var k_v in map)
{
    if(k_v.Key.EndsWith("A"))
    {
        current_positions.Add(k_v.Key);
        current_steps_to_z.Add(0);
    }
}

int steps = 0;
bool all_ends_with_z_found = true;

for(int i = 0; i < turns.Count; i++)
{
    if(turns[i] == 'L')
    {
        for(int j = 0; j < current_positions.Count; j++)
        {
            current_positions[j] = map[current_positions[j]].Item1;
        }
    }
    else
    {
        for(int j = 0; j < current_positions.Count; j++)
        {
            current_positions[j] = map[current_positions[j]].Item2;
        }
    }

    steps++;

    all_ends_with_z_found = true;

    for(int k = 0; k < current_positions.Count; k++)
    {
        if(current_positions[k].EndsWith("Z") && current_steps_to_z[k] == 0)
            current_steps_to_z[k] = steps;

        if(current_steps_to_z[k] == 0)
            all_ends_with_z_found = false;
    }

    if(all_ends_with_z_found)
        break;
    
    if(i == turns.Count - 1)
        i = -1;
}

for(int i = 0; i < current_positions.Count; i++)
{
    Console.WriteLine($"Start point {i}: {current_steps_to_z[i]} steps");
}

long[] steps_arr = new long[current_steps_to_z.Count];

for(int i = 0; i < current_steps_to_z.Count; i++)
    steps_arr[i] = current_steps_to_z[i];

long steps_final = LCM(steps_arr);

Console.WriteLine($"Steps: {steps_final}");


static long LCM(long[] numbers)
{
    return numbers.Aggregate(lcm);
}
static long lcm(long a, long b)
{
    return Math.Abs(a * b) / GCD(a, b);
}
static long GCD(long a, long b)
{
    return b == 0 ? a : GCD(b, a % b);
}


