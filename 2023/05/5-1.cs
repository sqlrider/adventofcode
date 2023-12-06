
string[] puzzleinput = File.ReadAllLines(@"C:\Study\adventofcode\2023\05\input.txt");

string[] seeds_str = puzzleinput[0].Split(':')[1].Trim().Split(' ');

// Global seed struct to track updated destinations for a given line of mappings
Seed[] seed_map = new Seed[seeds_str.Length];

for(int i = 0; i < seeds_str.Length; i++)
{
    seed_map[i].source = Convert.ToUInt32(seeds_str[i]);
    seed_map[i].dest = Convert.ToUInt32(seeds_str[i]); 
}

foreach(string line in puzzleinput)
{
    Console.WriteLine(line);

    if(line.Contains(':'))
        continue;

    // If we're on a blank line we've just finished a set of mappings, so update seed source/dests
    if(line == "")
    {
        for(int i = 0; i < seed_map.Length; i++)
        {
            seed_map[i].source = seed_map[i].dest;
        }

        continue;
    }
    
    string[] seed_to_soil_map = line.Trim().Split(' ');

    uint source_start = Convert.ToUInt32(seed_to_soil_map[1]);
    uint dest_start = Convert.ToUInt32(seed_to_soil_map[0]);
    uint range = Convert.ToUInt32(seed_to_soil_map[2]);
    uint source_end = source_start + range - 1;
    uint dest_end = dest_start + range - 1;

    for(int i = 0; i < seed_map.Length; i++)
    {
        if(seed_map[i].source >= source_start && seed_map[i].source <= source_end)
        {
            uint diff = seed_map[i].source - source_start;
            uint dest = dest_start + diff;
            //Console.WriteLine($"Seed {seed_map[i].source} in range, dest: {dest}");
            seed_map[i].dest = dest;
        }
        else
        {
            //Console.WriteLine($"Seed {seed_map[i].source} has no mapping, dest: {seed_map[i].dest}");
        }

    }
}

uint lowest_number = uint.MaxValue;

for(int i = 0; i < seed_map.Length; i++)
{
    Console.WriteLine($"Seed: {seed_map[i].source} dest: {seed_map[i].dest}");
    if(seed_map[i].dest < lowest_number)
        lowest_number = seed_map[i].dest;
}

Console.WriteLine($"Lowest number: {lowest_number}");

struct Seed
{
    public uint source;
    public uint dest;
};
