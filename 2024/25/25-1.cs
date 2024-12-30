// https://adventofcode.com/2024/day/25

string[] puzzleinput = File.ReadAllLines(@"..\input.txt");

const int lockheight = 7;
const int lockwidth = 5;
List<Lock> locks = new List<Lock>();
List<Key> keys = new List<Key>();
int[] tmp_layout = new int[lockwidth];

for(int i = 0; i < puzzleinput.Length; i+= lockheight + 1)
{
    Array.Fill(tmp_layout,0);

    if(puzzleinput[i][0] == '#' && puzzleinput[i][1] == '#' && puzzleinput[i][2] == '#' && puzzleinput[i][3] == '#' && puzzleinput[i][4] == '#')
    {
        for(int k = 0; k < 5; k++)
        {
            for(int j = 0; j < 7; j++)
            {
                if(puzzleinput[i+j][k] == '#')
                    tmp_layout[k]++;
            }
            
        }

        locks.Add(new Lock(tmp_layout));
    }
    else
    {
        for(int k = 0; k < 5; k++)
        {
            for(int j = 0; j < 7; j++)
            {
                if(puzzleinput[i+j][k] == '#')
                    tmp_layout[k]++;
            }
        }

        keys.Add(new Key(tmp_layout));
    }
}

int total = 0;
bool fits;
foreach(Lock l in locks)
{
    foreach(Key k in keys)
    {
        fits = true;
        for(int i = 0; i < lockwidth; i++)
        {
            if(l.layout![i] + k.layout![i] > 7)
                fits = false;
        }

        if(fits)
            total++;
    }
}

Console.WriteLine($"Total = {total}");

public class Lock
{
    public Lock(int[] layout)
    {
        this.layout = layout.Clone() as int[];
    }
    public int[]? layout;
}

public class Key
{
    public Key(int[] layout)
    {
        this.layout = layout.Clone() as int[];
    }
    public int[]? layout;
}