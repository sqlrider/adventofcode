// https://adventofcode.com/2024/day/18

string[] puzzleinput = File.ReadAllLines(@"..\input.txt");

const int height = 71;
const int width = 71;

const int source_y = 0;
const int source_x = 0;

const int target_y = 70;
const int target_x = 70;

char[][] grid = new char[height][];

for(int i = 0; i < height; i++)
{
    grid[i] = new char[width];

    for(int j = 0; j < width; j++)
    {
        grid[i][j] = '.';
    }
}

for(int n = 0; n < 1024; n++)
{
    string[] bytepos = puzzleinput[n].Split(',');
    int byte_y = Convert.ToInt32(bytepos[1]);
    int byte_x = Convert.ToInt32(bytepos[0]);

    grid[byte_y][byte_x] = '#';
}

//DrawGrid();

List<Node> visited = new List<Node>();
List<Node> nodes = new List<Node>();

// Add nodes
for(int i = 0; i < height; i++)
{
    for(int j = 0; j < width; j++)
    {
        if(i == source_y && j == source_x)
            nodes.Add(new Node(i,j,0,false));
        else if(i == target_y && j == target_x)
            nodes.Add(new Node(i,j,Int32.MaxValue, true));  
        else if(grid[i][j] == '.')
            nodes.Add(new Node(i,j,Int32.MaxValue, false));
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

Dijkstra();

void Dijkstra()
{
    while(unvisited.GetCount() > 0)
    {
        Node current = unvisited.GetLowestNode();
        if(current == null)
        {
            Console.WriteLine($"List empty. Aborting");
            break;
        }

        if(current.target == true)
        {
            Console.WriteLine($"Found exit! Distance = {current.distance}");
            break;
        }

        foreach(Node neighbour in current.neighbours)
        {
            if(current.distance + 1 < neighbour.distance)
                neighbour.distance = current.distance + 1;
        }

        visited.Add(current);
        unvisited.Remove(current);
    }
}


void DrawGrid()
{
    for(int i = 0; i < height; i++)
    {
        for(int j = 0; j < width; j++)
        {
            Console.Write(grid[i][j]);
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}

class Node
{
    public Node(int y, int x, int distance, bool target)
    {
        this.y = y;
        this.x = x;
        this.distance = distance;
        this.target = target;
        neighbours = new List<Node>();
        previous = null;

    }

    public int y;
    public int x;
    public bool target;
    public int distance;
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
            Node lowestnode = nodes[0]; // Default value

            foreach(Node n in nodes)
            {
                if(n.distance < min)
                {
                    lowestnode = n;
                    min = n.distance;
                }
            }
            
            return lowestnode;
        }
        else
            return null!;
        
    }
    public List<Node> nodes;
}