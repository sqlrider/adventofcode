// https://adventofcode.com/2024/day/2

string[] puzzleinput = File.ReadAllLines(@"..\input.txt");

bool increasing;
bool is_safe;
int total = 0;

foreach(string s in puzzleinput)
{
    var splitted = s.Split(' ');
    int[] levels = new int[splitted.Length];

    for(int i = 0; i < splitted.Length; i++)
    {
        levels[i] = Convert.ToInt32(splitted[i]);
    }

    if(levels[1] > levels[0])
        increasing = true;
    else if(levels[1] < levels[0])
        increasing = false;
    else
        continue;

    is_safe = true;
    
    for(int i = 0; i < levels.Length - 1; i++)
    {
        if(levels[i] == levels[i+1])
        {
            is_safe = false;
            break;
        }
        if(increasing)
        {
            int diff = levels[i+1] - levels[i];
            if(diff < 1 || diff > 3)
            {
                is_safe = false;
                break;
            }
        }

        if(!increasing)
        {
            int diff = levels[i] - levels[i+1];
            if(diff < 1 || diff > 3)
            {
                is_safe = false;
                break;
            }
        }
    }

    if(is_safe)
        total++;
}

Console.WriteLine(total);

