// https://adventofcode.com/2024/day/23#part2

string[] puzzleinput = File.ReadAllLines(@"..\input.txt");

Dictionary<string,Computer> computers = new Dictionary<string,Computer>();
HashSet<Computer> computers_set = new HashSet<Computer>();

for(int i = 0; i < puzzleinput.Length; i++)
{
    string[] splitted = puzzleinput[i].Split('-');
    if(!computers.ContainsKey(splitted[0]))
        computers[splitted[0]] = new Computer(splitted[0]);

    if(!computers.ContainsKey(splitted[1]))
        computers[splitted[1]] = new Computer(splitted[1]);
}

for(int i = 0; i < puzzleinput.Length; i++)
{
    string[] splitted = puzzleinput[i].Split('-');
    computers[splitted[0]].connected_computers.Add(computers[splitted[1]]);
    computers[splitted[1]].connected_computers.Add(computers[splitted[0]]);
}

foreach(var kvp in computers)
    computers_set.Add(kvp.Value);


HashSet<Computer> R = new HashSet<Computer>();  
HashSet<Computer> P = new HashSet<Computer>(computers_set); 
HashSet<Computer> X = new HashSet<Computer>(); 
HashSet<Computer> biggest_set = new HashSet<Computer>();

BronKerbosch(R, P, X, ref biggest_set);

foreach(Computer c in biggest_set.Order())
{
    Console.Write($"{c.id},");
}
Console.WriteLine();

// Bron-Kerbosch algo 
/*
algorithm BronKerbosch1(R, P, X) is
    if P and X are both empty then
        report R as a maximal clique
    for each vertex v in P do
        BronKerbosch1(R ⋃ {v}, P ⋂ N(v), X ⋂ N(v))
        P := P \ {v}
        X := X ⋃ {v}
*/
void BronKerbosch(HashSet<Computer> R, HashSet<Computer> P, HashSet<Computer> X, ref HashSet<Computer> biggest_set)
{
    // If P and X both empty then a max set R has been found
    if(P.Count == 0 && X.Count == 0)
    {
        // Only want largest set for this puzzle
        if(R.Count > biggest_set.Count)
            biggest_set = new HashSet<Computer>(R);
        
        return;
    }

    // foreach computer in candidate list P
    foreach(var comp in P)
    {
        R.Add(comp);

        var new_P = new HashSet<Computer>(P.Intersect(comp.connected_computers));
        var new_X = new HashSet<Computer>(X.Intersect(comp.connected_computers));

        BronKerbosch(R, new_P, new_X, ref biggest_set);

        P.Remove(comp);
        X.Add(comp);
        R.Remove(comp);
    }
}


public class Computer : IComparable<Computer>
{
    public Computer(string id)
    {
        this.id = id;
        connected_computers = new List<Computer>();
    }
    public string id;
    public List<Computer> connected_computers;
    public int CompareTo(Computer? other_computer)
    {
        return this.id.CompareTo(other_computer!.id);
    }
}
