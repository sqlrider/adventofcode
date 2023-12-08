
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

string currentpos = "AAA";
int steps = 0;

for(int i = 0; i < turns.Count; i++)
{
    if(turns[i] == 'L')
        currentpos = map[currentpos].Item1;
    else
        currentpos = map[currentpos].Item2;

    steps++;

    if(currentpos == "ZZZ")
        break;

    if(i == turns.Count - 1)
        i = -1;
}

Console.WriteLine($"Steps: {steps}");

