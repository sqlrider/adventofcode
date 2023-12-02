
string[] puzzleinput = File.ReadAllLines(@"C:\Study\adventofcode\2023\02\input.txt");

// in each game you played, what is the fewest number of cubes of each color that could have been in the bag to make the game possible?
int maxred;
int maxgreen;
int maxblue;

int total_powers = 0;

for(int i = 0; i < puzzleinput.Length; i++)
{
    maxred = 0;
    maxgreen = 0;
    maxblue = 0;

    string subs = puzzleinput[i].Substring(puzzleinput[i].IndexOf(':') + 2);

    string[] bags = subs.Split(';');

    // For each bag in a game
    for(int j = 0; j < bags.Length; j++)
    {
        bags[j] = bags[j].Trim();
        
        string[] cubes = bags[j].Split(',');

        for(int k = 0; k < cubes.Length; k++)
        {
            cubes[k] = cubes[k].Trim();

            string[] cubecount = cubes[k].Split(' ');
            int num_cubes = Int32.Parse(cubecount[0]);

            if(cubecount[1] == "red")
            {
                if(num_cubes > maxred)
                    maxred = num_cubes;
            }
            else if(cubecount[1] == "green")
            {
                if(num_cubes > maxgreen)
                    maxgreen = num_cubes;
            }
            else if(cubecount[1] == "blue")
            {
                if(num_cubes > maxblue)
                    maxblue = num_cubes;
            }
        }
    }

    int power = maxred * maxgreen * maxblue;
    total_powers += power;
}

Console.WriteLine($"Total power: {total_powers}");