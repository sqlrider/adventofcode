// https://adventofcode.com/2024/day/2#part2

string[] puzzleinput = File.ReadAllLines(@"..\input.txt");

bool firstequal = false;
bool increasing = false;
bool is_safe;
int total = 0;

foreach(string s in puzzleinput)
{
    var splitted = s.Split(' ');
    List<int> levels = new List<int>();

    for(int i = 0; i < splitted.Length; i++)
    {
        levels.Add(Convert.ToInt32(splitted[i]));
    }

    if(levels[1] > levels[0])
        increasing = true;
    else if(levels[1] < levels[0])
        increasing = false;
    else
        firstequal = true;

    is_safe = true;
    
    for(int i = 0; i < levels.Count- 1; i++)
    {
        if(firstequal)
        {
            is_safe = false;
            break;
        }
        else if(levels[i] == levels[i+1])
        {
            is_safe = false;
            break;
        }
        else if(increasing)
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
    else
    {
        for(int i = 0; i < levels.Count; i++)
        {
            var amended_levels = new List<int>(levels);
            amended_levels.RemoveAt(i);

            if(amended_levels[1] > amended_levels[0])
                increasing = true;
            else if(amended_levels[1] < amended_levels[0])
                increasing = false;
            else
                continue;

            is_safe = true;
            
            for(int j = 0; j < amended_levels.Count- 1; j++)
            {

                if(amended_levels[j] == amended_levels[j+1])
                {
                    is_safe = false;
                    break;
                }
                if(increasing)
                {
                    int diff = amended_levels[j+1] - amended_levels[j];
                    if(diff < 1 || diff > 3)
                    {
                        is_safe = false;
                        break;
                    }
                }

                if(!increasing)
                {
                    int diff = amended_levels[j] - amended_levels[j+1];
                    if(diff < 1 || diff > 3)
                    {
                        is_safe = false;
                        break;
                    }
                }
            }

            if(is_safe)
            {
                total++;
                break;
            }
        }
    }
}

Console.WriteLine(total);

