// https://adventofcode.com/2024/day/6

string[] puzzleinput = File.ReadAllLines(@"..\input.txt");

int height = puzzleinput.Length;
int width = puzzleinput[0].Length;

(int y, int x) guardpos = (0,0);
char direction = 'N';
int count = 0;

// Build grid 1 space bigger on each side
char[][] grid = new char[height + 2][];
for(int i = 0; i < height + 2; i++)
{
    grid[i] = new char[width + 2];
}

// Populate grid
for(int i = 0; i < height + 2; i++)
{
    Array.Fill<char>(grid[i], '*');
}
for(int i = 0; i < width; i++)
{
    for(int j = 0; j < height; j++)
    {
        grid[i+1][j+1] = puzzleinput[i][j];
        if(puzzleinput[i][j] == '^')
            guardpos = (i+1,j+1);
    }
}

while(true)
{
    grid[guardpos.y][guardpos.x] = 'X';

    if(direction == 'N')
    {
        if(grid[guardpos.y-1][guardpos.x] == '.' || grid[guardpos.y-1][guardpos.x] == 'X')
            guardpos.y -= 1;
        else if(grid[guardpos.y-1][guardpos.x] == '#')
            direction = 'E';
        else if(grid[guardpos.y-1][guardpos.x] == '*')
            break;
    }
    else if(direction == 'E')
    {
        if(grid[guardpos.y][guardpos.x+1] == '.' || grid[guardpos.y][guardpos.x+1] == 'X')
            guardpos.x += 1;
        else if(grid[guardpos.y][guardpos.x+1] == '#')
            direction = 'S';
        else if(grid[guardpos.y][guardpos.x+1] == '*')
            break;
    }
    else if(direction == 'S')
    {
        if(grid[guardpos.y+1][guardpos.x] == '.' || grid[guardpos.y+1][guardpos.x] == 'X')
            guardpos.y += 1;
        else if(grid[guardpos.y+1][guardpos.x] == '#')
            direction = 'W';
        else if(grid[guardpos.y+1][guardpos.x] == '*')
            break;
    }
    else if(direction == 'W')
    {
        if(grid[guardpos.y][guardpos.x-1] == '.' || grid[guardpos.y][guardpos.x-1] == 'X')
            guardpos.x -= 1;
        else if(grid[guardpos.y][guardpos.x-1] == '#')
            direction = 'N';
        else if(grid[guardpos.y][guardpos.x-1] == '*')
            break;
    }
}

for(int i = 0; i < height + 2; i++)
{
    for(int j = 0; j < height + 2; j++)
    {
        //Console.Write(grid[i][j]);
        if(grid[i][j] == 'X')
            count++;
    }
    //Console.WriteLine();
}

Console.WriteLine(count);