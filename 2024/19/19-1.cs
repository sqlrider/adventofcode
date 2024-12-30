// https://adventofcode.com/2024/day/19

string[] puzzleinput = File.ReadAllLines(@"..\input.txt");

string[] tmp =  puzzleinput[0].Split(',');
string[] towels = new string[tmp.Length];
for(int i = 0; i < tmp.Length; i++)
    towels[i] = tmp[i].Trim();

List<string> designs = new List<string>();
for(int i = 2; i< puzzleinput.Length; i++)
{
    designs.Add(puzzleinput[i]);
}

int total = 0;

foreach(string design in designs)
{
    bool matched = false;
    Recurse(design, design, ref matched);
    if(matched)
        total++;
}

Console.WriteLine($"Designs possible = {total}");

void Recurse(string target, string original_target, ref bool matched)
{
    if(matched)
        return;
    if(target == "")
    {
        matched = true;
        return;
    }

    foreach(string t in towels)
    {
        if(matched)
            return;

        if(target.StartsWith(t))
        {
            target = target.Substring(t.Length, target.Length - t.Length);
            Recurse(target, target, ref matched);
        }

        target = original_target;
    }
    return;
}