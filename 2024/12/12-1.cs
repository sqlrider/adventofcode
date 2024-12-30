// https://adventofcode.com/2024/day/12

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

int fences;
int area;
string plant_type;
int total_price = 0;

for(int i = 1; i < height + 1; i++)
{
    for(int j = 1; j < height + 1; j++)
    {
        if(!treaded[i][j])
        {
            plant_type = grid[i][j];
            fences = 0;
            area = 0;
            PathFind(i,j);

            total_price += area * fences;
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
        fences++;
    else if(grid[y-1][x] == plant_type && !treaded[y-1][x])
        PathFind(y-1,x);
    
    // Check south
    if(grid[y+1][x] == "*" || grid[y+1][x] != plant_type)
        fences++;
    else if(grid[y+1][x] == plant_type && !treaded[y+1][x])
        PathFind(y+1,x);

    // Check west
    if(grid[y][x-1] == "*" || grid[y][x-1] != plant_type)
        fences++;
    else if(grid[y][x-1] == plant_type && !treaded[y][x-1])
        PathFind(y,x-1);

    // Check east
    if(grid[y][x+1] == "*" || grid[y][x+1] != plant_type)
        fences++;
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