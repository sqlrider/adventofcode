// https://adventofcode.com/2024/day/8#part2

string[] puzzleinput = File.ReadAllLines(@"..\input.txt");

int height = puzzleinput.Length;
int width = puzzleinput[0].Length;

List<Node> nodes = new List<Node>();
List<Node> antinodes = new List<Node>();

for(int i = 0; i < height; i++)
{
    for(int j = 0; j < width; j++)
    {
        if(puzzleinput[i][j] != '.')
            nodes.Add(new Node(puzzleinput[i][j].ToString(),i,j));
    }
}

// For each node, calculate antinodes
foreach(var n in nodes)
{
    foreach(var n2 in nodes)
    {
        // Found a resonant
        if(n2.letter == n.letter && !(n2.y == n.y && n2.x == n.x))
        {
            int ydiff = n2.y - n.y;
            int xdiff = n2.x - n.x;

            int new_y = n.y;
            int new_x = n.x;

            while(true)
            {
                new_y += ydiff;
                new_x += xdiff;

                // Check antinode within array bounds
                if(new_y >= 0 && new_y < height && new_x >= 0 && new_x < width)
                    antinodes.Add(new Node("#",new_y,new_x));
                else
                    break;
            }
        }
    }
}

// Display/calculate
int total = 0;
bool found;
for(int i = 0; i < height; i++)
{
    for(int j = 0; j < width; j++)
    {
        found = false;
        foreach(var a in antinodes)
        {
            if(a.y == i && a.x == j)
            {
                //Console.Write("#");
                found = true;
                total++;
                break;   
            }
        }

        foreach(var n in nodes)
        {
            if(n.y == i && n.x == j)
            {
                if(!found)
                {
                    found = true;
                    //Console.Write(n.letter);
                    break;
                }
            }
        }
        /*
        if(!found)
        {
            Console.Write(".");
        }*/
    }
    //Console.WriteLine();
}

Console.WriteLine($"Total = {total}");

struct Node(string letter, int y, int x)
{
    public string letter = letter;
    public int y = y;
    public int x = x;
}