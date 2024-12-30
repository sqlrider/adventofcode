// https://adventofcode.com/2024/day/16

string[] puzzleinput = File.ReadAllLines(@"..\input.txt");

int height = puzzleinput.Length;
int width = puzzleinput[0].Length;

char[][] grid = new char[height][];

for(int i = 0; i < height; i++)
{
    grid[i] = new char[width];
    for(int j = 0; j < width; j++)
    {
        grid[i][j] = puzzleinput[i][j];
    }
}

List<Node> nodes = new List<Node>();

// Add nodes
for(int i = 0; i < height; i++)
{
    for(int j = 0; j < width; j++)
    {
        if(grid[i][j] == 'S')
            nodes.Add(new Node(i,j,0,false,'-'));
        else if(grid[i][j] == 'E')
            nodes.Add(new Node(i,j,Int32.MaxValue, true, null));  
        else if(grid[i][j] == '.')
            nodes.Add(new Node(i,j,Int32.MaxValue, false, null));
    }
}

// Calculate edges/neighbours
foreach(Node n1 in nodes)
{
    foreach(Node n2 in nodes)
    {
        if(n1 == n2)
            continue;

        if((n1.y == n2.y && Math.Abs(n2.x - n1.x) == 1) || (n1.x == n2.x && Math.Abs(n2.y - n1.y) == 1))
            n1.neighbours.Add(n2);
    }
}


// Remove inaccessible nodes
for(int i = 0; i < nodes.Count; i++)
{
    if(nodes[i].neighbours.Count == 0)
    {
        nodes.RemoveAt(i);
        i--;
    }
}

// Create priority queue
NonBrokenPriorityQueue unvisited = new NonBrokenPriorityQueue();
foreach(Node n1 in nodes)
{
    unvisited.Add(n1);
}

bool found = Dijkstra(unvisited);

Node current = null;

foreach(Node n in nodes)
{
    if(n.target == true)
        current = n;
}

List<(int y,int x)> route = new List<(int y,int x)>();

while(true)
{
    route.Add((current.y, current.x));

    if(current.previous == null)
        break;

    current = current.previous;
}

// Alter grid to show route
for(int i = 0; i < height; i++)
{
    for(int j = 0; j < width; j++)
    {
        if(route.Contains((i,j)) == true)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("o");
            Console.ForegroundColor = ConsoleColor.White;
        }
        else
            Console.Write(grid[i][j]);

    }
    Console.WriteLine();
}
Console.WriteLine();

bool Dijkstra(NonBrokenPriorityQueue unvisited)
{
    while(unvisited.GetCount() > 0)
    {
        Node current = unvisited.GetLowestNode();
        if(current == null)
        {
            Console.WriteLine($"No more nodes accessible.");
            return false;
        }

        if(current.target == true && current.previous is not null)
        {
            Console.WriteLine($"Found an exit - distance = {current.distance}");
            break;
        }

        foreach(Node neighbour in current.neighbours)
        {
            if(neighbour.x == current.x && current.entered_angle == '|')
            {
                if(current.distance + 1 < neighbour.distance)
                {
                    neighbour.distance = current.distance + 1;
                    neighbour.previous = current;
                    neighbour.entered_angle = '|';
                }
            }
            else if(neighbour.y == current.y && current.entered_angle == '-')
            {
                if(current.distance + 1 < neighbour.distance)
                {
                    neighbour.distance = current.distance + 1;
                    neighbour.previous = current;
                    neighbour.entered_angle = '-';
                }
            }
            else
            {
                if(current.distance + 1001 < neighbour.distance)
                {
                    neighbour.distance = current.distance + 1001;
                    neighbour.previous = current;
                    if(current.entered_angle == '-')
                        neighbour.entered_angle = '|';
                    if(current.entered_angle == '|')
                        neighbour.entered_angle = '-';
                }
            }
        }

        unvisited.Remove(current);
    }

    Console.WriteLine($"Exiting - unvisited.Count == {unvisited.GetCount()}");

    return false;
}

void DrawGrid()
{
    for(int i = 0; i < height; i++)
    {
        for(int j = 0; j < width; j++)
        {
            Console.ForegroundColor = ConsoleColor.White;
            if(grid[i][j] == 'o')
                Console.ForegroundColor = ConsoleColor.Red;

            Console.Write(grid[i][j]);
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}

class Node
{
    public Node(int y, int x, int distance, bool target, char? entered_angle)
    {
        this.y = y;
        this.x = x;
        this.distance = distance;
        this.target = target;
        this.entered_angle = entered_angle;
        neighbours = new List<Node>();
        previous = null;
    }

    public int y;
    public int x;
    public bool target;
    public int distance;
    public char? entered_angle;
    public List<Node> neighbours;
    public Node? previous;

}

class NonBrokenPriorityQueue
{
    public NonBrokenPriorityQueue()
    {
        nodes = new List<Node>();
    }

    public void Add(Node node)
    {
        nodes.Add(node);
    }

    public void Remove(Node node)
    {
        nodes.Remove(node);
    }

    public int GetCount()
    {
        return nodes.Count;
    }

    public Node GetLowestNode()
    {
        if(nodes.Count > 0)
        {
            int min = Int32.MaxValue;
            Node lowestnode = null;

            foreach(Node n in nodes)
            {
                if(n.distance < min)
                {
                    lowestnode = n;
                    min = n.distance;
                }
            }
            if(lowestnode != null)
                return lowestnode;
            else
                return null!;
        }
        else
            return null!;
        
    }
    public List<Node> nodes;
}