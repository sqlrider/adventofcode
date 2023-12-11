
global using Galaxy = (int y, int x);

string[] puzzleinput = File.ReadAllLines(@"C:\Study\adventofcode\2023\11\input.txt");

int galaxy_count = 1;
var x_spaces = new List<int>();
var y_spaces = new List<int>();
bool is_empty_x = true;
bool is_empty_y = true;
var galaxies = new List<Galaxy>();

// Display grid, number galaxies and record empty row numbers for y-axis
for(int i = 0; i < puzzleinput.Length; i++)
{
    is_empty_y = true;

    for(int j = 0; j < puzzleinput[0].Length; j++)
    {
        if(puzzleinput[i][j] == '#')
        {
            Console.Write(galaxy_count);
            galaxy_count++;
            is_empty_y = false;
            galaxies.Add((i,j));
        }
        else
            Console.Write('.');
    }
    Console.WriteLine();

    if(is_empty_y)
        y_spaces.Add(i);
}

// Record empty row numbers for x-axis
for(int j = 0; j < puzzleinput[0].Length; j++)
{
    is_empty_x = true;

    for(int i = 0; i < puzzleinput.Length; i++)
    {
        if(puzzleinput[i][j] == '#')
            is_empty_x = false;
    }

    if(is_empty_x)
        x_spaces.Add(j);
}

// Create distinct pairs of galaxies
var galaxy_pairs = new List<Tuple<Galaxy, Galaxy>>();
for(int i = 0; i < galaxies.Count; i++)
{
    for(int j = i + 1; j < galaxies.Count; j++)
    {
        var pair = new Tuple<Galaxy,Galaxy>(galaxies[i], galaxies[j]);
        galaxy_pairs.Add(pair);
    }
}

int total_distance = 0;

foreach(var pair in galaxy_pairs)
{
    int dist = Math.Abs(pair.Item1.y - pair.Item2.y) + Math.Abs(pair.Item1.x - pair.Item2.x);

    // Distance with space expansion
    int expansions = 0;
    int min_y = Math.Min(pair.Item1.y, pair.Item2.y);
    int max_y = Math.Max(pair.Item1.y, pair.Item2.y);

    for(int i = min_y; i < max_y; i++)
    {
        if(y_spaces.Contains(i))
            expansions++;   
    }

    int min_x = Math.Min(pair.Item1.x, pair.Item2.x);
    int max_x = Math.Max(pair.Item1.x, pair.Item2.x);

    for(int i = min_x; i < max_x; i++)
    {
        if(x_spaces.Contains(i))
            expansions++;
    }

    dist += expansions;
    total_distance += dist;

    //Console.WriteLine($"({pair.Item1.y},{pair.Item1.x}) - ({pair.Item2.y},{pair.Item2.x}) dist: {dist}");
}

Console.WriteLine($"Pairs: {galaxy_pairs.Count}");
Console.WriteLine($"Total distance: {total_distance}");