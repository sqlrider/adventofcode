// https://adventofcode.com/2024/day/4

string[] puzzleinput = File.ReadAllLines(@"..\input.txt");

int height = puzzleinput.Length;
int width = puzzleinput[0].Length;

int count = 0;

// Build grid 3 spaces bigger on each side
char[][] grid = new char[height + 6][];
for(int i = 0; i < height + 6; i++)
{
    grid[i] = new char[width + 6];
}

// Populate grid
for(int i = 0; i < height + 6; i++)
{
    Array.Fill<char>(grid[i], '*');
}
for(int i = 0; i < width; i++)
{
    for(int j = 0; j < height; j++)
    {
        grid[i+3][j+3] = puzzleinput[i][j];
    }
}

for(int i = 3; i < height + 3; i++)
{
    for(int j = 3; j < height + 3; j++)
    {
        if(grid[i][j] == 'X')
        {
            CheckNorth(i,j);
            CheckNorthEast(i,j);
            CheckEast(i,j);
            CheckSouthEast(i,j);
            CheckSouth(i,j);
            CheckSouthWest(i,j);
            CheckWest(i,j);
            CheckNorthWest(i,j);
        }

    }   
}

Console.WriteLine(count);

void CheckNorth(int y, int x)
{
    if(grid[y-1][x] == 'M')
    {
        if(grid[y-2][x] == 'A')
        {
            if(grid[y-3][x] == 'S')
                count++;
        }
    }
}

void CheckNorthEast(int y, int x)
{
    if(grid[y-1][x+1] == 'M')
    {
        if(grid[y-2][x+2] == 'A')
        {
            if(grid[y-3][x+3] == 'S')
                count++;
        }
    }
}

void CheckEast(int y, int x)
{
    if(grid[y][x+1] == 'M')
    {
        if(grid[y][x+2] == 'A')
        {
            if(grid[y][x+3] == 'S')
                count++;
        }
    }
}

void CheckSouthEast(int y, int x)
{
    if(grid[y+1][x+1] == 'M')
    {
        if(grid[y+2][x+2] == 'A')
        {
            if(grid[y+3][x+3] == 'S')
                count++;
        }
    }
}

void CheckSouth(int y, int x)
{
    if(grid[y+1][x] == 'M')
    {
        if(grid[y+2][x] == 'A')
        {
            if(grid[y+3][x] == 'S')
                count++;
        }
    }
}

void CheckSouthWest(int y, int x)
{
    if(grid[y+1][x-1] == 'M')
    {
        if(grid[y+2][x-2] == 'A')
        {
            if(grid[y+3][x-3] == 'S')
                count++;
        }
    }
}

void CheckWest(int y, int x)
{
    if(grid[y][x-1] == 'M')
    {
        if(grid[y][x-2] == 'A')
        {
            if(grid[y][x-3] == 'S')
                count++;
        }
    }
}

void CheckNorthWest(int y, int x)
{
    if(grid[y-1][x-1] == 'M')
    {
        if(grid[y-2][x-2] == 'A')
        {
            if(grid[y-3][x-3] == 'S')
                count++;
        }
    }
}