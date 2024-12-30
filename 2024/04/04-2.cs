// https://adventofcode.com/2024/day/4#part2

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
        if(grid[i][j] == 'A')
        {
            if((grid[i-1][j-1] == 'S' && grid[i-1][j+1] == 'S' && grid[i+1][j-1] == 'M' && grid[i+1][j+1] == 'M') 
            | (grid[i-1][j-1] == 'M' && grid[i-1][j+1] == 'M' && grid[i+1][j-1] == 'S' && grid[i+1][j+1] == 'S')
            | (grid[i-1][j-1] == 'M' && grid[i-1][j+1] == 'S' && grid[i+1][j-1] == 'M' && grid[i+1][j+1] == 'S')
            | (grid[i-1][j-1] == 'S' && grid[i-1][j+1] == 'M' && grid[i+1][j-1] == 'S' && grid[i+1][j+1] == 'M'))
            {
                count++;
            }
        }

    }   
}

Console.WriteLine(count);