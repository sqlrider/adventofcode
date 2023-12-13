
string[] puzzleinput = File.ReadAllLines(@"C:\Study\adventofcode\2023\13\input.txt");

var grids = new List<char[][]>();

int grid_line = 0;

for(int i = 0; i < puzzleinput.Length; i++)
{
    if(puzzleinput[i].Length != 0)
        grid_line++;

    if(puzzleinput[i] == "")
    {
        var grid = new char[grid_line][];
        int grid_line_count = 0;
        for(int j = i - grid_line; j < i; j++)
        {
            var line = new char[puzzleinput[j].Length];
            for(int k = 0; k < puzzleinput[j].Length; k++)
            {
                line[k] = puzzleinput[j][k];
            }
            grid[grid_line_count] = line;
            grid_line_count++;
        }

        grids.Add(grid);
        grid_line = 0;
    }
}

// Find mirror lines by checking each vertical split in columns - check columns on either side using loop conditions to not go out of bounds.
// Quit checking current split if a difference is found and move onto next.
// Mirror is found if no differences found until either left side hits 0 or right side hits max length of array
int total_columns = 0;
int total_rows = 0;

foreach(var grid in grids)
{
    // Vertical
    for(int i = 1; i < grid[0].Length; i++)
    {
        bool is_mirror_line = false;

        for(int step = 1; i - step >= 0 && i + step <= grid[0].Length; step++)
        {
            is_mirror_line = true;

            for(int j = 0; j < grid.Length; j++)
            {
                if(grid[j][i - step] != grid[j][step -1 + i])
                {
                    is_mirror_line = false;
                    break;
                }

            }
            if(!is_mirror_line)
                break;
        }
        if(is_mirror_line)
            total_columns += i;
    }

    // Horizontal
    for(int i = 1; i < grid.Length; i++)
    {
        bool is_mirror_line = false;

        for(int step = 1; i - step >= 0 && i + step <= grid.Length; step++)
        {
            is_mirror_line = true;

            for(int j = 0; j < grid[0].Length; j++)
            {
                if(grid[i - step][j] != grid[step -1 + i][j])
                {
                    is_mirror_line = false;
                    break;
                }
            }
            if(!is_mirror_line)
                break;
        }
        if(is_mirror_line)
            total_rows += i;
    }
}

int total = total_columns + (total_rows * 100);

Console.WriteLine($"Cols: {total_columns}, rows: {total_rows}, Total: {total}");