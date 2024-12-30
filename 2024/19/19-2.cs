// https://adventofcode.com/2024/day/19#part2

string[] puzzleinput = File.ReadAllLines(@"..\input.txt");

string[] tmp =  puzzleinput[0].Split(',');
string[] towels = new string[tmp.Length];
for(int i = 0; i < tmp.Length; i++)
    towels[i] = tmp[i].Trim();

List<string> designs = new List<string>();
for(int i = 2; i< puzzleinput.Length; i++)
{
    designs.Add(puzzleinput[i]);
}

Dictionary<string,long> cache = new Dictionary<string,long>();

long total = 0;

foreach(string design in designs)
{
    //Console.WriteLine($"Checking {design}");
    total += Recurse(design);
}

Console.Write($"Total combinations = {total}");

long Recurse(string target)
{
    if(target == "")
        return 1;
    else if(cache.ContainsKey(target))  // Don't check further and just return how many combinations for the remaining string
        return cache[target];

    // Start a count of possible combinations from further recursion
    long pattern_combinations = 0;
    foreach(string t in towels)
    {
        if(target.StartsWith(t))
            pattern_combinations += Recurse(target.Substring(t.Length, target.Length - t.Length));
    }

    // If already stored the remaining string in cache, increment by how many found via this recursion path, else add an entry for it
    if(cache.ContainsKey(target))
        cache[target] += pattern_combinations;
    else
        cache[target] = pattern_combinations;

    // Return number of combos back up the chain
    return pattern_combinations;
}
