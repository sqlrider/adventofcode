// https://adventofcode.com/2024/day/14#part2

string[] puzzleinput = File.ReadAllLines(@"..\input.txt");

int height = 103;
int width = 101;
int heightmid = height / 2;
int widthmid = width / 2;

List<Robot> robots = new List<Robot>();

foreach(string s in puzzleinput)
{
    string stripped = s.Replace("p=","").Replace("v=","").Replace(" ",",");
    string[] splitted = stripped.Split(',');
    robots.Add(new Robot(Convert.ToInt32(splitted[1]),Convert.ToInt32(splitted[0]),Convert.ToInt32(splitted[3]),Convert.ToInt32(splitted[2])));
}

GridSquare[][] grid = new GridSquare[height][];
for(int i = 0; i < grid.Length; i++)
{
    grid[i] = new GridSquare[width];
    for(int j = 0; j < grid[i].Length; j++)
    {
        grid[i][j] = new GridSquare(i,j);
    }
}

foreach(Robot r in robots)
{
    grid[r.y][r.x].RobotCount++;
}

// Save robot positions
List<List<(int y, int x)>> all_positions = new List<List<(int,int)>>();

int n = 0;
int northwest;
int northeast;
int southwest;
int southeast;
int safetyfactor;
int minsafetyfactor = Int32.MaxValue;

// Keep displaying the grid when a minimum safety factor found (more robots clustered in the middle), wait for xmas tree shape to appear
while(true)
{   
    n++;

    northwest = 0;
    northeast = 0;
    southwest = 0;
    southeast = 0;

    foreach(Robot r in robots)
    {
        grid[r.y][r.x].RobotCount--;

        r.y += r.vy;
        if(r.y >= 0)
            r.y %= height;
        else
            r.y += height;

        r.x += r.vx;
        if(r.x >= 0)
            r.x %= width;
        else
            r.x += width;

        grid[r.y][r.x].RobotCount++;
    }


    for(int i = 0; i < grid.Length; i++)
    {
        for(int j = 0; j < grid[i].Length; j++)
        {
            if(i < heightmid && j < widthmid)
                northwest += grid[i][j].RobotCount;
            else if(i < heightmid && j > widthmid)
                northeast += grid[i][j].RobotCount;
            else if(i > heightmid && j < widthmid)
                southwest += grid[i][j].RobotCount;
            else if(i > heightmid && j > widthmid)
                southeast += grid[i][j].RobotCount;
        }
    }

    safetyfactor = northwest * northeast * southwest * southeast;
    if(safetyfactor < minsafetyfactor)
    {
        minsafetyfactor = safetyfactor;

        DrawGrid();
        Console.WriteLine($"Minimum found at n={n}");
    }
}



void DrawGrid()
{
    for(int i = 0; i < grid.Length; i++)
    {
        for(int j = 0; j < grid[i].Length; j++)
        {
            if(grid[i][j].RobotCount == 0)
                Console.Write(".");
            else
                Console.Write(grid[i][j].RobotCount);
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}


class GridSquare
{
    public GridSquare(int y, int x)
    {
        this.y = y;
        this.x = x;
        RobotCount = 0;
    }

    public int RobotCount;
    public int y {get;set;}
    public int x {get;set;}
}

class Robot
{
    public Robot(int y, int x, int vy, int vx)
    {
        this.y = y;
        this.x = x;
        this.vy = vy;
        this.vx = vx;
    }

    public int y {get;set;}
    public int x {get;set;}
    public int vy  {get;set;}
    public int vx {get;set;}
}