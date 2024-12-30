// https://adventofcode.com/2024/day/12#part2

string[] puzzleinput = File.ReadAllLines(@"..\input.txt");

int height = puzzleinput.Length;
int width = puzzleinput[0].Length;

string[][] grid = new string[height+2][];

for(int i = 0; i < height + 2; i++)
{
    grid[i] = new string[width+2];
    Array.Fill<string>(grid[i],"*");
}

bool[][] treaded = new bool[height + 2][];
for(int i = 0; i < height + 2; i++)
{
    treaded[i] = new bool[width + 2];
    Array.Fill<bool>(treaded[i], true);
}

for(int i = 1; i < height + 1; i++)
{
    for(int j = 1; j < width + 1; j++)
    {
        grid[i][j] = puzzleinput[i-1][j-1].ToString();
        treaded[i][j] = false;
    }
}

int fences ;
int area;
string plant_type;
int total_price = 0;
List<Edge> edges = new List<Edge>(); 


int edgecount;
bool found;
for(int i = 1; i < height + 1; i++)
{
    for(int j = 1; j < width + 1; j++)
    {
        if(!treaded[i][j])
        {
            plant_type = grid[i][j];
            fences = 0;
            area = 0;
            edges.Clear();
            
            PathFind(i,j);

            // Calculate edges
            edgecount = edges.Count;
            for(int a = 1; a < height + 1; a++)
            {
                for(int b = 1; b < width + 1; b++)
                {
                    foreach(Edge e in edges)
                    {
                        if(!e.removed)
                        {
                            if(e.y == a && e.x == b && (e.facing == "north" || e.facing == "south"))
                            {
                                // Try going east if not at edge
                                if(b < width + 1)
                                {
                                    for(int c = b + 1; c < width + 1; c++)
                                    {
                                        found = false;

                                        foreach(Edge e2 in edges)
                                        {
                                            if(e2.y == a && e2.x == c && e2.facing == e.facing && !e2.removed)
                                            {
                                                e2.removed = true;
                                                edgecount--;
                                                found = true;
                                            }

                                            
                                        }

                                        if(!found)
                                            break;
                                    }
                                }

                                // Try going west if not at edge
                                if(b > 1)
                                {
                                    for(int c = b - 1; c > 0; c--)
                                    {
                                        found = false;

                                        foreach(Edge e2 in edges)
                                        {
                                            if(e2.y == a && e2.x == c && e2.facing == e.facing && !e2.removed)
                                            {
                                                e2.removed = true;
                                                edgecount--;
                                                found = true;
                                            }
                                        }

                                        if(!found)
                                            break;
                                    }
                                }
                            }

                            // n/s
                            if(e.y == a && e.x == b && (e.facing == "east" || e.facing == "west"))
                            {
                                // Try going south if not at edge
                                if(a < height + 1)
                                {
                                    //Console.WriteLine("Trying south");
                                    for(int c = a + 1; c < height + 1; c++)
                                    {
                                        found = false;

                                        foreach(Edge e2 in edges)
                                        {
                                            
                                            if(e2.y == c && e2.x == b && e2.facing == e.facing && !e2.removed)
                                            {   
                                                e2.removed = true;
                                                edgecount--;
                                                found = true;
                                            }
                                        }
                                        
                                        if(!found)
                                            break;
                                    }
                                }

                                // Try going north if not at edge
                                if(a > 1)
                                {
                                    for(int c = a - 1; c > 0; c--)
                                    {
                                        found = false;

                                        foreach(Edge e2 in edges)
                                        {
                                            if(e2.y == c && e2.x == b && e2.facing == e.facing && !e2.removed)
                                            {
                                                e2.removed = true;
                                                edgecount--;
                                                found = true;
                                            }
                                        }

                                        if(!found)
                                            break;
                                    }
                                }
                            }
                    
                        }
                        
                    }
                }
            }

            //Console.WriteLine($"Type = {plant_type}, Area = {area}, edgecount = {edgecount}");
            total_price += area * edgecount;
        }
    }
}

Console.WriteLine($"Total price = {total_price}");

void PathFind(int y, int x)
{
    treaded[y][x] = true;
    area++;

    // Check north
    if(grid[y-1][x] == "*" || grid[y-1][x] != plant_type)
    {
        fences++;
        edges.Add(new Edge(y,x,"north",false));
    }
    else if(grid[y-1][x] == plant_type && !treaded[y-1][x])
        PathFind(y-1,x);
    
    // Check south
    if(grid[y+1][x] == "*" || grid[y+1][x] != plant_type)
    {
        fences++;
        edges.Add(new Edge(y,x,"south",false));
    }
    else if(grid[y+1][x] == plant_type && !treaded[y+1][x])
        PathFind(y+1,x);

    // Check west
    if(grid[y][x-1] == "*" || grid[y][x-1] != plant_type)
    {
        fences++;
        edges.Add(new Edge(y,x,"west",false));
    }
    else if(grid[y][x-1] == plant_type && !treaded[y][x-1])
        PathFind(y,x-1);

    // Check east
    if(grid[y][x+1] == "*" || grid[y][x+1] != plant_type)
    {
        fences++;
        edges.Add(new Edge(y,x,"east",false));
    }
    else if(grid[y][x+1] == plant_type && !treaded[y][x+1])
        PathFind(y,x+1);
}

/*
void DisplayGrid()
{
    for(int i = 0; i < height + 2; i++)
    {
        for(int j = 0; j < width + 2; j++)
        {
            if(treaded[i][j] && grid[i][j] != "*")
                Console.ForegroundColor = ConsoleColor.Red;
            else
                Console.ForegroundColor = ConsoleColor.Black;

            Console.Write(grid[i][j]);
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}*/

class Edge
{
    public Edge(int y, int x, string facing, bool removed)
    {
        this.y = y;
        this.x = x;
        this.facing = facing;
        this.removed = removed;
    }
    public int y {get;set;}
    public int x {get;set;}
    public string facing {get;set;}
    public bool removed {get;set;}
}