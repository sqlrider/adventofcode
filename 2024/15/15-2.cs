// https://adventofcode.com/2024/day/15#part2

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
List<Box> boxes = new List<Box>();

(int y, int x) robotpos = (0,0);

// Fill grid
for(int i = 0; i < puzzleinput.Length; i++)
{
    if(String.IsNullOrEmpty(puzzleinput[i]))
        break;

    grid[i] = new char[width * 2];
    Array.Fill(grid[i], '.');

    for(int j = 0; j < width; j++)
    {
        if(puzzleinput[i][j] == '@')
        {
            grid[i][2*j] = '@';
            robotpos = (i,2*j);
        }
        else if(puzzleinput[i][j] == 'O')
        {
            grid[i][2*j] = '[';
            grid[i][2*j+1] = ']';
            boxes.Add(new Box(i, 2*j, 2*j+1));
        }
        else
        {
            grid[i][2*j] = puzzleinput[i][j];
            grid[i][2*j+1] = puzzleinput[i][j];
        }
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
    //Console.WriteLine(m);
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
        // Hit wall, don't move
    }
    else if(grid[robotpos.y + move_vector.y][robotpos.x + move_vector.x] == '.')
    {
        grid[robotpos.y][robotpos.x] = '.';
        grid[robotpos.y + move_vector.y][robotpos.x + move_vector.x] = '@';
        robotpos.y += move_vector.y;
        robotpos.x += move_vector.x;
    }
    else if(grid[robotpos.y + move_vector.y][robotpos.x + move_vector.x] == '[' || grid[robotpos.y + move_vector.y][robotpos.x + move_vector.x] == ']')
    {
        for(int b = 0; b < boxes.Count; b++)
        {
            if(boxes[b].y == robotpos.y + move_vector.y && (boxes[b].x1 == robotpos.x + move_vector.x || boxes[b].x2 == robotpos.x + move_vector.x))
            {
                // Create a list of boxes and add any that are in the chain of potential pushes, select distinct only to prevent double moves
                // can_move sets to false if any of them cannot move
                // Then move them all at once, reversing so grid re-writing is correct
                List<Box> boxes_to_move = new List<Box>();
                bool can_move = true;
                TryMove(boxes[b], move_vector.y, move_vector.x, ref boxes_to_move, ref can_move);
                if(can_move == true)
                {
                    boxes_to_move.Reverse();
                    foreach(Box bo in boxes_to_move.Distinct())
                    {
                        grid[bo.y][bo.x1] = '.';
                        grid[bo.y][bo.x2] = '.';
                        bo.y += move_vector.y;
                        bo.x1 += move_vector.x;
                        bo.x2 += move_vector.x;
                        grid[bo.y][bo.x1] = '[';
                        grid[bo.y][bo.x2] = ']';
                    }
                    grid[robotpos.y][robotpos.x] = '.';
                    robotpos.y += move_vector.y;
                    robotpos.x += move_vector.x;
                    grid[robotpos.y][robotpos.x] = '@';
                }
            }
        }
    }
}


int sum = 0;

for(int i = 0; i < grid.Length; i++)
{
    for(int j = 0; j < grid[i].Length; j++)
    {
        if(grid[i][j] == '[')
            sum += (100 * i) + j;
    }
}

DrawGrid();

Console.WriteLine($"Sum = {sum}");

// recursive try moving function to check if box(es) can move
void TryMove(Box box, int my, int mx, ref List<Box> boxes_to_move, ref bool can_move)
{
    boxes_to_move.Add(box);

    // If moving vertically
    if(my != 0)
    {
        if(grid[box.y+my][box.x1] == '#' || grid[box.y+my][box.x2] == '#')
            can_move = false;
        else if(grid[box.y+my][box.x1] == '[') // whole box directly above/below
        {
            foreach(Box b in boxes)
            {
                if(b.y == box.y+my && b.x1 == box.x1)
                    TryMove(b,my,mx, ref boxes_to_move, ref can_move);
            }
        }
        
        if(grid[box.y+my][box.x1] == ']') // box to NW/SW
        {
            foreach(Box b in boxes)
            {
                if(b.y == box.y+my && b.x1 == box.x1 - 1)
                    TryMove(b,my,mx,ref boxes_to_move, ref can_move); 
            }
        }
        if(grid[box.y+my][box.x2] == '[') // box to NE/SE
        {
            foreach(Box b in boxes)
            {
                if(b.y == box.y+my && b.x1 == box.x1 + 1)
                    TryMove(b,my,mx, ref boxes_to_move, ref can_move);
            }
        }
    }
    else if(mx == 1) // If moving right
    {
        if(grid[box.y][box.x2 + mx] == '#')
            can_move = false;
        else if(grid[box.y][box.x2 + mx] == '[') // Whole box to right
        {
            foreach(Box b in boxes)
            {
                if(b.y == box.y && b.x1 == box.x2 + mx)
                {
                    TryMove(b,my,mx, ref boxes_to_move, ref can_move);
                }
            }
        }
    }
    else if(mx == -1) // if moving left
    {
        if(grid[box.y][box.x1 + mx] == '#')
            can_move = false;
        else if(grid[box.y][box.x1 + mx] == ']') // Whole box to left
        {
            foreach(Box b in boxes)
            {
                if(b.y == box.y && b.x2 == box.x1 + mx)
                {
                    TryMove(b,my,mx, ref boxes_to_move, ref can_move);
                }
            }
        }
    }
}

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

class Box
{
    public Box(int y, int x1, int x2)
    {
        this.y = y;
        this.x1 = x1;
        this.x2 = x2;
    }
    public int y;
    public int x1;
    public int x2;
}