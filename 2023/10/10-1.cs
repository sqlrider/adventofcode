
global using Point = (int y, int x);

string[] puzzleinput = File.ReadAllLines(@"C:\Study\adventofcode\2023\10\input.txt");

int height = puzzleinput.Length + 2;
int width = puzzleinput[0].Length + 2;

char[][] grid = new char[height][];

List<Point> path_points = new List<Point>();

for(int i = 0; i < height; i++)
{
    grid[i] = new char[width];
    Array.Fill(grid[i], '.');
}

for(int i = 0; i < height - 2; i++)
{
    for(int j = 0; j < width - 2; j++)
        grid[i+1][j+1] = puzzleinput[i][j];
}

Point start_point = (y: 0, x: 0);

for(int i = 0; i < height; i++)
{
    for(int j = 0; j < width; j++)
    {
        if(grid[i][j] == 'S')
            start_point = (i,j);
    }
}

Point current_point = start_point;
char next_dir = 'z';
int steps = 0;
bool found_end = false;

// Somewhat overcomplicated traversal scheme due to expecting part 2 to need checking other non-loop paths or something
while(!found_end)
{
    path_points.Add(current_point);
    next_dir = Traverse(grid[current_point.y][current_point.x], current_point.y, current_point.x, next_dir);
    steps++;

    if(next_dir == 'U')
        current_point.y -= 1;

    if(next_dir == 'D')
        current_point.y += 1;

    if(next_dir == 'L')
        current_point.x -= 1;
    
    if(next_dir == 'R')
        current_point.x += 1;

    if(next_dir == 'X')
    {  
        Console.WriteLine("Something's gone wrong.");
        found_end = true;
    }

    if(grid[current_point.y][current_point.x] == 'S')
    {
        Console.WriteLine($"Found end in {steps} steps. Farthest point: {steps / 2}");
        found_end = true;
    }
}

// Display grid
for(int i = 0; i < height; i++)
{
    for(int j = 0; j < width; j++)
    {
        if(path_points.Contains((i,j)))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(grid[i][j]);
        }
        else
        {
            Console.ResetColor();
            Console.Write(grid[i][j]);
        }
    }
    Console.WriteLine();
}



char Traverse(char curr_pipe, int curr_y, int curr_x, char came_from = 'z')
{  
    switch(curr_pipe)
    {
        case '|':
            if(came_from == 'D')
                return 'D';
            else
                return 'U';
        case '-':
            if(came_from == 'L')
                return 'L';
            else   
                return 'R';
        case 'L':
            if(came_from == 'D')
                return 'R';
            else
                return 'U';
        case 'J':
            if(came_from == 'D')
                return 'L';
            else
                return 'U';
        case '7':
            if(came_from == 'U')
                return 'L';
            else
                return 'D';
        case 'F':
            if(came_from == 'U')
                return 'R';
            else
                return 'D';
        case 'S':
            //special start point case, pick first of two valid directions to go in (doesn't matter which is found first)
            if(grid[curr_y - 1][curr_x] == '|' || grid[curr_y - 1][curr_x] == 'F' || grid[curr_y - 1][curr_x] == '7')
                return 'U';
            if(grid[curr_y + 1][curr_x] == '|' || grid[curr_y + 1][curr_x] == 'J' || grid[curr_y + 1][curr_x] == 'L')
                return 'D';
            if(grid[curr_y][curr_x + 1] == '-' || grid[curr_y][curr_x + 1] == 'J' || grid[curr_y][curr_x + 1] == '7')
                return 'R';
            if(grid[curr_y][curr_x - 1] == '-' || grid[curr_y][curr_x - 1] == 'L' || grid[curr_y][curr_x - 1] == 'F')
                return 'L';
            return 'X';
        default:
            return 'X';
    }
}
