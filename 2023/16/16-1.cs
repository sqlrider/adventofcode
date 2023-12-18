// https://adventofcode.com/2023/day/16 - Part 1

string[] puzzleinput = File.ReadAllLines(@"C:\Study\adventofcode\2023\16\input.txt");

Point[][] grid = new Point[puzzleinput.Length + 2][];

for(int i = 0; i < grid.Length; i++)
{
    grid[i] = new Point[puzzleinput[0].Length + 2];
}

// Fill edges of grid
for(int i = 0; i < grid.Length; i++)
{
    for(int k = 0; k < grid[0].Length; k++)
        grid[i][k].content = '*';
}

// Set up grid
for(int i = 1; i < puzzleinput.Length + 1; i++)
{
    for(int k = 1; k < puzzleinput[0].Length + 1; k++)
    {
        grid[i][k].content = puzzleinput[i-1][k-1];
        grid[i][k].energized = false;
    }
}

// Set up list of beams and initial beam
List<Beam> beams = new List<Beam>();
beams.Add(new Beam(1, 1, 'E'));
grid[1][1].energized = true;

bool beams_in_flight = true;
List<Beam> beams_to_add = new List<Beam>();
int rounds_nothing_energized = 0;
bool energized_this_round = false;

PrintGrid();
Console.WriteLine();

// Loop while active beams in flight (upto 10 'turns' with no newly-energized locations)
while(beams_in_flight)
{
    energized_this_round = false;

    // Iterate over beams
    for(int a = 0; a < beams.Count; a++)
    {

        // Energize current location of beam
        if(grid[beams[a].y][beams[a].x].content != '*' && !grid[beams[a].y][beams[a].x].energized)
        {
            grid[beams[a].y][beams[a].x].energized = true;
            energized_this_round = true;
            rounds_nothing_energized = 0;
        }

        // If beam is on a *, remove beam from list
        if(grid[beams[a].y][beams[a].x].content == '*')
        {
            beams.RemoveAt(a);
            a--;
        }

        // Move beam to next location
        // North
        else if(beams[a].Direction == 'N')
        {
            // If beam is on a -, split beam
            if(grid[beams[a].y][beams[a].x].content == '-')
            {
                // West side - move beam west
                beams[a].x -= 1;
                beams[a].Direction = 'W';
                
                // East side - create new beam
                beams_to_add.Add(new Beam(beams[a].y, beams[a].x + 1, 'E'));
            }
            // If beam on a mirror, energize the mirror and move in one go
            else if(grid[beams[a].y][beams[a].x].content == '\\')
            {
                grid[beams[a].y][beams[a].x].energized = true;
                beams[a].x -= 1;
                beams[a].Direction = 'W';
            }
            else if(grid[beams[a].y][beams[a].x].content == '/')
            {
                grid[beams[a].y][beams[a].x].energized = true;
                beams[a].x += 1;
                beams[a].Direction = 'E';
            }
            // Pass through . or |
            else if(grid[beams[a].y][beams[a].x].content == '.' || grid[beams[a].y][beams[a].x].content == '|')
                beams[a].y -= 1;
        }
        else if(beams[a].Direction == 'S')
        {
            // If beam is on a -, split beam
            if(grid[beams[a].y][beams[a].x].content == '-')
            {
                // West side - move beam west
                beams[a].x -= 1;
                beams[a].Direction = 'W';
                
                // East side - create new beam
                beams_to_add.Add(new Beam(beams[a].y, beams[a].x + 1, 'E'));
            }
            // If beam on a mirror, energize the mirror and move in one go
            else if(grid[beams[a].y][beams[a].x].content == '\\')
            {
                grid[beams[a].y][beams[a].x].energized = true;
                beams[a].x += 1;
                beams[a].Direction = 'E';
            }
            else if(grid[beams[a].y][beams[a].x].content == '/')
            {
                grid[beams[a].y][beams[a].x].energized = true;
                beams[a].x -= 1;
                beams[a].Direction = 'W';
            }
            // Pass through . or |
            else if(grid[beams[a].y][beams[a].x].content == '.' || grid[beams[a].y][beams[a].x].content == '|')
                beams[a].y += 1;
            
        }
        else if(beams[a].Direction == 'W')
        {
            // If beam is on a |, split beam
            if(grid[beams[a].y][beams[a].x].content == '|')
            {
                // North side - move beam north
                beams[a].y -= 1;
                beams[a].Direction = 'N';
                
                // South side - create new beam
                beams_to_add.Add(new Beam(beams[a].y + 1, beams[a].x, 'S'));
            }
            // If beam on a mirror, energize the mirror and move in one go
            else if(grid[beams[a].y][beams[a].x].content == '\\')
            {
                grid[beams[a].y][beams[a].x].energized = true;
                beams[a].y -= 1;
                beams[a].Direction = 'N';
            }
            else if(grid[beams[a].y][beams[a].x].content == '/')
            {
                grid[beams[a].y][beams[a].x].energized = true;
                beams[a].y += 1;
                beams[a].Direction = 'S';
            }
            // Pass through . or - 
            else if(grid[beams[a].y][beams[a].x].content == '.' || grid[beams[a].y][beams[a].x].content == '-')
                beams[a].x -= 1;
        }
        else if(beams[a].Direction == 'E')
        {
            // If beam is on a |, split beam
            if(grid[beams[a].y][beams[a].x].content == '|')
            {
                // North side - move beam north
                beams[a].y -= 1;
                beams[a].Direction = 'N';
                
                // South side - create new beam
                beams_to_add.Add(new Beam(beams[a].y + 1, beams[a].x, 'S'));
            }
            // If beam on a mirror, energize the mirror and move in one go
            else if(grid[beams[a].y][beams[a].x].content == '\\')
            {
                grid[beams[a].y][beams[a].x].energized = true;
                beams[a].y += 1;
                beams[a].Direction = 'S';
            }
            else if(grid[beams[a].y][beams[a].x].content == '/')
            {
                grid[beams[a].y][beams[a].x].energized = true;
                beams[a].y -= 1;
                beams[a].Direction = 'N';
            }
            // Pass through . or - 
            else if(grid[beams[a].y][beams[a].x].content == '.' || grid[beams[a].y][beams[a].x].content == '-')
                beams[a].x += 1;
        }
    }

    // Add any new beams to main list
    foreach(Beam b in beams_to_add)
        beams.Add(b);
    beams_to_add.Clear();

    if(!energized_this_round)
        rounds_nothing_energized++;

    if(rounds_nothing_energized > 10)
        beams_in_flight = false;
}

// Count energized locations
int energized_count = 0;
for(int i = 0; i < grid.Length; i++)
{
    for(int k = 0; k < grid[0].Length; k++)
    {
        if(grid[i][k].energized)
            energized_count++;
    }
}

PrintGrid();

Console.WriteLine($"Energized: {energized_count}");

// Print
void PrintGrid()
{
for(int i = 0; i < grid.Length; i++)
    {
        for(int k = 0; k < grid[0].Length; k++)
        {
            if(grid[i][k].energized)
                Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(grid[i][k].content);
            Console.ResetColor();
        }
        Console.WriteLine();
    }
}

public struct Point
{
    public bool energized;
    public char content;
}

public class Beam
{
    public Beam(int y, int x, char Direction)
    {
        this.y = y;
        this.x = x;
        this.Direction = Direction;
    }
    public int y {get; set;}
    public int x {get; set;}
    public char Direction {get; set;}
}