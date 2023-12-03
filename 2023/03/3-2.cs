
string[] puzzleinput = File.ReadAllLines(@"C:\Study\adventofcode\2023\03\input.txt");

int width = puzzleinput[0].Length;
int height = puzzleinput.Length;

int total = 0;

// Dictionary to hold key-value pairs of gears plus any part numbers near them
Dictionary<Tuple<int,int>, List<int>> gear_dict = new Dictionary<Tuple<int,int>, List<int>>();

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
    bool is_next_to_gear = false;
    int gear_i = 0;
    int gear_j = 0;

    for(int j = 1; j < grid[0].Length - 1; j++)
    {
        // Check if current position is a number, if so, start building string representation and also check for surrounding gear
        if(int.TryParse(grid[i][j].ToString(), out int temp))
        {
            currentnumber += grid[i][j];
            
            // Check if surrounding character is a * - if we don't already know part of the number is next to one
            // and store the gear coords
            if(!is_next_to_gear)
            {
                if(grid[i-1][j-1] == '*')
                {
                    is_next_to_gear = true;
                    gear_i = i-1;
                    gear_j = j-1;
                }
                else if(grid[i-1][j] == '*')
                {
                    is_next_to_gear = true;
                    gear_i = i-1;
                    gear_j = j;
                }
                else if(grid[i-1][j+1] == '*')
                {
                    is_next_to_gear = true;
                    gear_i = i-1;
                    gear_j = j+1;
                }
                else if(grid[i][j-1] == '*')
                {
                    is_next_to_gear = true;
                    gear_i = i;
                    gear_j = j-1;
                }
                else if(grid[i][j+1] == '*')
                {
                    is_next_to_gear = true;
                    gear_i = i;
                    gear_j = j+1;
                }
                else if(grid[i+1][j-1] == '*')
                {
                    is_next_to_gear = true;
                    gear_i = i+1;
                    gear_j = j-1;
                }
                else if(grid[i+1][j] == '*')
                {
                    is_next_to_gear = true;
                    gear_i = i+1;
                    gear_j = j;
                }
                else if(grid[i+1][j+1] == '*')
                {
                    is_next_to_gear = true;
                    gear_i = i+1;
                    gear_j = j+1;
                }
            }

            // Current number may be on edge of grid so need to perform check here too
            if((j == grid[0].Length - 2) && is_next_to_gear)
            {
                // Create a tuple for gear co-ords, then add it to dictionary (create list if not exists)
                Tuple<int,int> gear_tuple = new Tuple<int,int>(gear_i, gear_j);
                List<int> part_list;
                if(!gear_dict.TryGetValue(gear_tuple, out part_list))
                {
                    part_list = new List<int>();
                    gear_dict.Add(gear_tuple,part_list);
                }

                part_list.Add(Convert.ToInt32(currentnumber));
            }
        }
        else // if not a number, then check if we've just finished reading one, if so, add it if valid
        {
            if(currentnumber != "" && is_next_to_gear)
            {
                // Create a tuple for gear co-ords, then add it to dictionary (create list if not exists)
                Tuple<int,int> gear_tuple = new Tuple<int,int>(gear_i, gear_j);
                List<int> part_list;
                if(!gear_dict.TryGetValue(gear_tuple, out part_list))
                {
                    part_list = new List<int>();
                    gear_dict.Add(gear_tuple,part_list);
                }

                part_list.Add(Convert.ToInt32(currentnumber));
            }

            currentnumber = "";
            is_next_to_gear = false;
        }
    }
}

// Check dictionary for any gears which have > 1 part numbers added
foreach(KeyValuePair<Tuple<int,int>,List<int>> entry in gear_dict)
{
    if(entry.Value.Count > 1)
    {
        int subtotal = 1;
        foreach(int part_num in entry.Value)
        {
            subtotal *= part_num;
        }

        total += subtotal;
    }
}

Console.WriteLine($"Sum of gear ratios: {total}");