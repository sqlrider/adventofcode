// https://adventofcode.com/2024/day/9

string[] puzzleinput = File.ReadAllLines(@"..\input.txt");

List<string> diskmap = new List<string>();

bool file = true;
int filenum = 0;

for(int i = 0; i < puzzleinput[0].Length; i++)
{
    int currentnum = Convert.ToInt32(puzzleinput[0][i].ToString());

    if(file)
    {
        for(int j = 0; j < currentnum; j++)
        {
            diskmap.Add(filenum.ToString());
        }
        file = false;
        filenum++;
    }
    else
    {
        for(int j = 0; j < currentnum; j++)
        {
            diskmap.Add(".");
        }
        file = true;
    }
}

bool any_swapped;
for(int i = 0; i < diskmap.Count; i++)
{
    any_swapped = false;
    
    if(diskmap[i] != ".")
        continue;
    else
    {
        for(int j = diskmap.Count - 1; j > i; j--)
        {
            if(diskmap[j] != ".")
            {
                diskmap[i] = diskmap[j];
                diskmap[j] = ".";
                any_swapped = true;
                break;
            }
        }
    }
    if(!any_swapped)
        break;
}

long checksum = 0;

for(int i = 0; i < diskmap.Count; i++)
{
    if(diskmap[i] == ".")
        break;
    else
        checksum += i * Convert.ToInt64(diskmap[i]);
}

Console.WriteLine(checksum);