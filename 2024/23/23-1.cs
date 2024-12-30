// https://adventofcode.com/2024/day/23

string[] puzzleinput = File.ReadAllLines(@"..\input.txt");

Dictionary<string,Computer> computers = new Dictionary<string,Computer>();
List<List<Computer>> sets_of_three = new List<List<Computer>>();

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

// Brute force
foreach(var kvp in computers)
{
    // For each computer connected to computer 1
    foreach(var conn1 in kvp.Value.connected_computers)
    {
        // For each computer (2) connected to computer 1
        foreach(var conn2 in conn1.connected_computers)
        {
            // For each computer (3) connected to computer 2, is it computer 1 ? (circular link)
            foreach(var conn3 in conn2.connected_computers)
            {
                if(conn3.id == kvp.Key)
                {
                    List<Computer> tmp = [kvp.Value, conn1, conn2];
                    sets_of_three.Add(tmp);
                }
            }
        }

    }
}

// Remove duplicate sets
for(int i = 0; i < sets_of_three.Count; i++)
{
    for(int j = i + 1; j < sets_of_three.Count; j++)
    {
        if(!sets_of_three[i].Except(sets_of_three[j]).Any())
        {
            sets_of_three.RemoveAt(j);
            j--;
        }
    }
}

int starts_with_t_count = 0;

foreach(var set in sets_of_three)
{
    if(set[0].id.StartsWith('t') || set[1].id.StartsWith('t') || set[2].id.StartsWith('t'))
        starts_with_t_count++;
}

Console.WriteLine($"Count = {starts_with_t_count}");

public class Computer
{
    public Computer(string id)
    {
        this.id = id;
        connected_computers = new List<Computer>();
    }
    public string id;
    public List<Computer> connected_computers;
}
