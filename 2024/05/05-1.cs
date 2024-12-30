// https://adventofcode.com/2024/day/5

string[] puzzleinput = File.ReadAllLines(@"..\input.txt");

List<int> answers = new List<int>();

int split = 0;
for(int i = 0; i < puzzleinput.Length; i++)
{
    if(String.IsNullOrWhiteSpace(puzzleinput[i]))
        split = i;
}

var rules = new List<(int x,int y)>();

for(int i = 0; i < split; i++)
{
    string[] nums = puzzleinput[i].Split('|');
    rules.Add((x:Convert.ToInt32(nums[0]),y:Convert.ToInt32(nums[1])));
}

var updates = new List<List<int>>();

for(int i = split + 1; i < puzzleinput.Length; i++)
{
    var update = new List<int>();

    var nums = puzzleinput[i].Split(',');

    foreach(var num in nums)
    {
        update.Add(Convert.ToInt32(num));
    }

    updates.Add(update);
}

// Brute force
foreach(var update in updates)
{
    bool rulepassed = false;
    
    // Check rules
    foreach(var rule in rules)
    {
        rulepassed = false;

        if(update.Contains(rule.x) && update.Contains(rule.y))
        {   
            for(int i = 0; i < update.Count; i++)
            {
                // check if higher number is ahead
                if(update[i] == rule.x)
                {
                    for(int j = i + 1; j < update.Count; j++)
                    {
                        if(update[j] == rule.y)
                            rulepassed = true;
                    }
                }
                if(update[i] == rule.y)
                {
                    for(int j = i - 1; j > 0; j--)
                    {
                        if(update[j] == rule.x)
                            rulepassed = true;
                    }
                }
            }
        }
        else
            rulepassed = true;

        if(!rulepassed)
            break;
    }

    if(rulepassed)
        answers.Add(update[update.Count / 2]);
}

int answer = answers.Sum();
Console.WriteLine(answer);