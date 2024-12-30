// https://adventofcode.com/2024/day/6#part2

string[] puzzleinput = File.ReadAllLines(@"..\input.txt");

int height = puzzleinput.Length;
int width = puzzleinput[0].Length;

(int y, int x) originalguardpos = (0,0);
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
        if(puzzleinput[i][j] == '^')
        {
            originalguardpos = (i+1,j+1);
            grid[i+1][j+1] = '.';
        }
        else
            grid[i+1][j+1] = puzzleinput[i][j];
    }
}

(int y, int x) lastobstaclepos = (0,0);

// Outer loop for adding obstacles
for(int i = 1; i < height + 1; i++)
{
    for(int j = 1; j < width + 1; j++)
    {
        if(lastobstaclepos.y != 0 && lastobstaclepos.x != 0)
            grid[lastobstaclepos.y][lastobstaclepos.x] = '.';

        if(grid[i][j] == '.')
        {
            lastobstaclepos.y = i;
            lastobstaclepos.x = j;
            grid[i][j] = '#';
        }
        
        guardpos = originalguardpos;
        direction = 'N';
        int iterations = 0;

        while(true)
        {
            if(iterations > 100000)
            {
                count++;
                break;
            }

            if(direction == 'N')
            {
                if(grid[guardpos.y-1][guardpos.x] == '.')
                    guardpos.y -= 1;
                else if(grid[guardpos.y-1][guardpos.x] == '#')
                    direction = 'E';
                else if(grid[guardpos.y-1][guardpos.x] == '*')
                    break;
            }
            else if(direction == 'E')
            {
                if(grid[guardpos.y][guardpos.x+1] == '.')
                    guardpos.x += 1;
                else if(grid[guardpos.y][guardpos.x+1] == '#')
                    direction = 'S';
                else if(grid[guardpos.y][guardpos.x+1] == '*')
                    break;
            }
            else if(direction == 'S')
            {
                if(grid[guardpos.y+1][guardpos.x] == '.')
                    guardpos.y += 1;
                else if(grid[guardpos.y+1][guardpos.x] == '#')
                    direction = 'W';
                else if(grid[guardpos.y+1][guardpos.x] == '*')
                    break;
            }
            else if(direction == 'W')
            {
                if(grid[guardpos.y][guardpos.x-1] == '.')
                    guardpos.x -= 1;
                else if(grid[guardpos.y][guardpos.x-1] == '#')
                    direction = 'N';
                else if(grid[guardpos.y][guardpos.x-1] == '*')
                    break;
            }
            
            iterations++;
        }
    }
}

Console.WriteLine(count);