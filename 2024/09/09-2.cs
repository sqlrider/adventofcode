// https://adventofcode.com/2024/day/9#part2

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

// Start from right end
bool foundgap;
int gapindex;
int gaplength;
for(int i = diskmap.Count - 1; i >= 0; i--)
{    
    foundgap = false;

    if(diskmap[i] == ".")
        continue;
    else
    {
        // if hit a number, find length
        int length = 1;
        int copyindex = 0;
        for(int j = i-1; j > 0; j--)
        {
            if(diskmap[j] == diskmap[i])
                length++;
            else
            {
                copyindex = j + 1;
                break;
            }
        }

        // See if a space exists
        gapindex = 0;
        gaplength = 0;
        for(int j = 0; j < i; j++)
        {
            // If found a space, calculate if enough blocks free
            if(diskmap[j] == ".")
            {
                gaplength++;
                if(gaplength == length)
                {
                    foundgap = true;
                    gapindex = j - gaplength + 1;
                    break;
                }
            }
            else
                gaplength = 0;
        }

        // Swap blocks as gap is big enough
        if(foundgap)
        {
            for(int l = 0; l < length; l++)
            {
                diskmap[gapindex + l] = diskmap[copyindex + l];
                diskmap[copyindex + l] = ".";
            }
        }

        i = i - length + 1;
    }
}


long checksum = 0;

for(int i = 0; i < diskmap.Count; i++)
{
    if(diskmap[i] == ".")
        continue;
    else
        checksum += i * Convert.ToInt64(diskmap[i]);
}

Console.WriteLine(checksum);
