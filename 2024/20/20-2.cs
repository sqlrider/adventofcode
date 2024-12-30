// https://adventofcode.com/2024/day/20#part2

string[] puzzleinput = File.ReadAllLines(@"..\input.txt");

int height = puzzleinput.Length;
int width = puzzleinput[0].Length;

char[][] grid = new char[height + 2][];

int start_y = 0;
int start_x = 0;

for(int i = 0; i < height + 2; i++)
{
    grid[i] = new char[width + 2];
    Array.Fill(grid[i], '#');
}

for(int i = 1; i < height + 1; i++)
{
    for(int j = 1; j < width + 1; j++)
    {
        grid[i][j] = puzzleinput[i-1][j-1];
        if(grid[i][j] == 'S')
        {
            start_y = i;
            start_x = j;
        }
    }
}

int picoseconds = 0;

// Perfom an initial traverse to get the default finish time and populate a dictionary of positions and how far into the race they are
Dictionary<(int y,int x),int> map = new Dictionary<(int,int),int>();
InitialTraverse(start_y, start_x, ref picoseconds);

List<string> cheats = new List<string>();
int cheats_over_100 = 0;

foreach(var m in map)
{
    // Try south east 
    for(int i = 0; i < 21; i++)
    {
        for(int j = 0; j + i < 21; j++)
        {
            if(m.Key.y + i <= height && m.Key.y + i > 0 && m.Key.x + j <= width && m.Key.x + j > 0)
            {
                if((grid[m.Key.y + i][m.Key.x + j] == '.' || grid[m.Key.y + i][m.Key.x + j] == 'E') && map[(m.Key.y + i,m.Key.x + j)] > m.Value)
                {
                    if(map[(m.Key.y + i,m.Key.x + j)] - map[(m.Key.y,m.Key.x)] - (j + i) >= 100)
                        cheats.Add($"{m.Key.y},{m.Key.x} - {m.Key.y+i},{m.Key.x+j}");
                }
            }
        }
    }

    // Try north east 
    for(int i = 0; i < 21; i++)
    {
        for(int j = 0; j + i < 21; j++)
        {
            if(m.Key.y - i <= height && m.Key.y - i > 0 && m.Key.x + j <= width && m.Key.x + j > 0)
            {
                if((grid[m.Key.y - i][m.Key.x + j] == '.' || grid[m.Key.y - i][m.Key.x + j] == 'E') && map[(m.Key.y - i,m.Key.x + j)] > m.Value)
                {
                    if(map[(m.Key.y - i,m.Key.x + j)] - map[(m.Key.y,m.Key.x)] - (j + i) >= 100)
                        cheats.Add($"{m.Key.y},{m.Key.x} - {m.Key.y-i},{m.Key.x+j}");
                }
            }
        }
    }


    // Try north west
    for(int i = 0; i < 21; i++)
    {
        for(int j = 0; j + i < 21; j++)
        {
            if(m.Key.y - i <= height && m.Key.y - i > 0 && m.Key.x - j <= width && m.Key.x - j > 0)
            {
                if((grid[m.Key.y - i][m.Key.x - j] == '.' || grid[m.Key.y - i][m.Key.x - j] == 'E') && map[(m.Key.y - i,m.Key.x - j)] > m.Value)
                {
                    if(map[(m.Key.y - i,m.Key.x - j)] - map[(m.Key.y,m.Key.x)] - (j + i) >= 100)
                        cheats.Add($"{m.Key.y},{m.Key.x} - {m.Key.y-i},{m.Key.x-j}");
                }
            }
        }
    }

    // Try south west
    for(int i = 0; i < 21; i++)
    {
        for(int j = 0; j + i < 21; j++)
        {
            if(m.Key.y + i <= height && m.Key.y + i > 0 && m.Key.x - j <= width && m.Key.x - j> 0)
            {
                if((grid[m.Key.y + i][m.Key.x - j] == '.' || grid[m.Key.y + i][m.Key.x - j] == 'E') && map[(m.Key.y + i,m.Key.x - j)] > m.Value)
                {
                    if(map[(m.Key.y + i,m.Key.x - j)] - map[(m.Key.y,m.Key.x)] - (j + i) >= 100)
                        cheats.Add($"{m.Key.y},{m.Key.x} - {m.Key.y+i},{m.Key.x-j}");
                }
           }
        }
    }
}

cheats_over_100 = cheats.Distinct().Count();

Console.WriteLine($"Total cheats over 100 = {cheats_over_100}");


void DrawGrid(int cur_y, int cur_x)
{
    for(int i = 1; i < height + 1; i++)
    {
        for(int j = 1; j < width + 1; j++)
        {
            if(i == cur_y && j == cur_x)
                Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(grid[i][j]);
            Console.ForegroundColor = ConsoleColor.White;
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}

void InitialTraverse(int y, int x, ref int picoseconds)
{
    char lastdir = 'X';

    while(true)
    {
        map[(y,x)] = picoseconds;

        if(grid[y][x] == 'E')
            break;

         // Try N
        if((grid[y-1][x] == '.' || grid[y-1][x] == 'E') && lastdir != 'S')
        {
            y -= 1;
            lastdir = 'N';
        }

        // Try S
        else if((grid[y+1][x] == '.' || grid[y+1][x] == 'E') && lastdir != 'N')
        {
            y+=1;
            lastdir = 'S';
        }

        // Try W
        else if((grid[y][x-1] == '.' || grid[y][x-1] == 'E') && lastdir != 'E')
         {
            x-=1;
            lastdir = 'W';
         }

        // Try E
        else if((grid[y][x+1] == '.' || grid[y][x+1] == 'E') && lastdir != 'W')
        {
            x+=1;
            lastdir = 'E';
        }

        picoseconds++;
    }

    return;
}
