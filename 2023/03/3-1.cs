
using System.Buffers;

string[] puzzleinput = File.ReadAllLines(@"C:\Study\adventofcode\2023\03\input.txt");

int width = puzzleinput[0].Length;
int height = puzzleinput.Length;

// New in .NET 8!
// char[] not_part_list = {'.','0','1','2','3','4','5','6','7','8','9'};
SearchValues<char> not_part_list = SearchValues.Create(".0123456789");

int total = 0;

// Set up grid arrays, +1 size in each dimension to save bounds checking
char[][] grid = new char[height+2][];
for (int i = 0; i < grid.Length; i++)
{
    grid[i] = new char[width + 2];
}

// Populate grid with . as default
for(int i = 0; i < grid.Length; i++)
{
    Array.Fill(grid[i], '.');
}

// Populate only puzzle area
for(int i = 0; i < height; i++)
{
    for(int j = 0; j < width; j++)
    {
        grid[i+1][j+1] = puzzleinput[i][j];
    }
}

for(int i = 1; i < grid.Length - 1; i++)
{
    string currentnumber = "";
    bool is_engine_part = false;

    for(int j = 1; j < grid[0].Length - 1; j++)
    {
        // Check if current position is a number, if so, start building string representation and also check for surrounding symbol
        if(int.TryParse(grid[i][j].ToString(), out int temp))
        {
            currentnumber += grid[i][j];
            
            // Check if surrounding character is not either . or a number - if we don't already know part of the number is next to a symbol
            if(!is_engine_part)
            {
                if(grid[i-1][j-1].ToString().AsSpan().IndexOfAny(not_part_list) == -1
                || grid[i-1][j].ToString().AsSpan().IndexOfAny(not_part_list) == -1
                || grid[i-1][j+1].ToString().AsSpan().IndexOfAny(not_part_list) == -1
                || grid[i][j-1].ToString().AsSpan().IndexOfAny(not_part_list) == -1
                || grid[i][j+1].ToString().AsSpan().IndexOfAny(not_part_list) == -1
                || grid[i+1][j-1].ToString().AsSpan().IndexOfAny(not_part_list) == -1
                || grid[i+1][j].ToString().AsSpan().IndexOfAny(not_part_list) == -1
                || grid[i+1][j+1].ToString().AsSpan().IndexOfAny(not_part_list) == -1)
                {
                    is_engine_part = true;
                }
            }

            // Current number may be on edge of grid
            if((j == grid[0].Length - 2) && is_engine_part)
            {
                //Console.WriteLine($"Found engine part {currentnumber}");
                total += Convert.ToInt32(currentnumber);
            }
        }
        else // if not a number, then check if we've just finished reading one, if so, add it if valid
        {
            if(currentnumber != "" && is_engine_part)
            {
                //Console.WriteLine($"Found engine part {currentnumber}");
                total += Convert.ToInt32(currentnumber);
            }

            currentnumber = "";
            is_engine_part = false;
        }
    }
}

Console.WriteLine($"Total: {total}");