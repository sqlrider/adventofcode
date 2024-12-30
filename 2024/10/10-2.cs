// https://adventofcode.com/2024/day/10-2

string[] puzzleinput = File.ReadAllLines(@"..\input.txt");

int width = puzzleinput[0].Length;
int height = puzzleinput.Length;

int[][] map = new int[height + 2][];

for(int i = 0; i < map.Length; i++)
{
    map[i] = new int[width + 2];
}

// Fill grid
for(int i = 0; i < height + 2; i++)
{
    Array.Fill<int>(map[i],99);
}

for(int i = 1; i < height + 1; i++)
{
    for(int j = 1; j < width + 1; j++)
    {
        map[i][j] = Convert.ToInt32(puzzleinput[i-1][j-1].ToString());
    }
}

List<(int y,int x)> trailheads = new List<(int y,int x)>();

// Get trailheads
for(int i = 1; i < height + 1; i++)
{
    for(int j = 1; j < width + 1; j++)
    {
        if(map[i][j] == 0)
            trailheads.Add((i,j));
    }
}

// global points var
int points = 0;

foreach(var th in trailheads)
{
    List<(int y,int x)> summits = new List<(int,int)>();

    pathfind(th.y, th.x, ref summits);
}

Console.WriteLine($"Points = {points}");

void pathfind(int y, int x, ref List<(int,int)> summits)
{
    if(map[y][x] == 9)
    {
        summits.Add((y,x));
        points++;
    }

    if(map[y-1][x] == map[y][x] + 1)
        pathfind(y-1,x, ref summits);

    if(map[y+1][x] == map[y][x] + 1)
        pathfind(y+1,x, ref summits);

    if(map[y][x-1] == map[y][x] + 1)
        pathfind(y,x-1, ref summits);

    if(map[y][x+1] == map[y][x] + 1)
        pathfind(y,x+1, ref summits);
}

/*
void DisplayMap(int y, int x)
{
    for(int i = 1; i < height + 1; i++)
    {
        for(int j = 1; j < width + 1; j++)
        {
            if(y == i && x == j)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.Write(map[i][j]);
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}*/