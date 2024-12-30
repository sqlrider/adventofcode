// https://adventofcode.com/2024/day/15

string[] puzzleinput = File.ReadAllLines(@"..\input.txt");

int moves_index = 0;

// Get index of split in input
for(int i = 0; i < puzzleinput.Length; i++)
{
    if(String.IsNullOrEmpty(puzzleinput[i]))
    {
        moves_index = i + 1;
        break;
    }
}

int height = moves_index - 1;
int width = puzzleinput[0].Length;

char[][] grid = new char[height][];
List<char> moves = new List<char>();

(int y, int x) robotpos = (0,0);

// Fill grid
for(int i = 0; i < puzzleinput.Length; i++)
{
    if(String.IsNullOrEmpty(puzzleinput[i]))
        break;

    grid[i] = new char[width];

    for(int j = 0; j < width; j++)
    {
        grid[i][j] = puzzleinput[i][j];

        if(grid[i][j] == '@')
            robotpos = (i,j);
    }
}

// Extract moves
for(int i = moves_index; i < puzzleinput.Length; i++)
{
    for(int j = 0; j < puzzleinput[i].Length; j++)
    {
        moves.Add(puzzleinput[i][j]);
    }
}

// Main loop
foreach(char m in moves)
{
    (int y, int x) move_vector = (0,0);

    switch(m)
    {
        case '^': move_vector = (-1,0); break;
        case '>': move_vector = (0,1); break;
        case 'v': move_vector = (1,0); break;
        case '<': move_vector = (0,-1); break;
        default: break;
    }

    if(grid[robotpos.y + move_vector.y][robotpos.x + move_vector.x] == '#')
    {
        // Hit wall, do nothing
    }
    else if(grid[robotpos.y + move_vector.y][robotpos.x + move_vector.x] == '.')
    {
        grid[robotpos.y][robotpos.x] = '.';
        grid[robotpos.y + move_vector.y][robotpos.x + move_vector.x] = '@';
        robotpos.y += move_vector.y;
        robotpos.x += move_vector.x;
    }
    else if(grid[robotpos.y + move_vector.y][robotpos.x + move_vector.x] == 'O')
    {

        int pushes = 1;
        bool pushed = false;

        for(int i = 2; i < 99; i++)
        {
            if(grid[robotpos.y + (i* move_vector.y)][robotpos.x + (i*move_vector.x)] == '#')
            {
                break;
            }
            if(grid[robotpos.y + (i* move_vector.y)][robotpos.x + (i*move_vector.x)] == '.')
            {
                pushed = true;
                break;
            }
            if(grid[robotpos.y + (i* move_vector.y)][robotpos.x + (i*move_vector.x)] == 'O')
            {
                pushes++;
            }
        }

        if(pushed)
        {
            grid[robotpos.y + ((1 + pushes)*move_vector.y)][robotpos.x + ((1 + pushes)*move_vector.x)] = 'O';
            grid[robotpos.y + move_vector.y][robotpos.x + move_vector.x] = '@';
            grid[robotpos.y][robotpos.x] = '.'; 
            robotpos.y += move_vector.y;
            robotpos.x += move_vector.x;

        }   
    }
}

int sum = 0;

for(int i = 0; i < grid.Length; i++)
{
    for(int j = 0; j < grid[i].Length; j++)
    {
        if(grid[i][j] == 'O')
            sum += (100 * i) + j;
    }
}

DrawGrid();

Console.WriteLine($"Sum = {sum}");

void DrawGrid()
{
    for(int i = 0; i < grid.Length; i++)
    {
        for(int j = 0; j < grid[i].Length; j++)
        {
            Console.Write(grid[i][j]);
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}