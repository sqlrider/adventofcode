
string[] puzzleinput = File.ReadAllLines(@"C:\Study\adventofcode\2023\02\input.txt");

// The Elf would first like to know which games would have been possible if the bag contained only 12 red cubes, 13 green cubes, and 14 blue cubes?
int maxred = 12;
int maxgreen = 13;
int maxblue = 14;

bool game_possible = true;
int possible_games_sum = 0;

for(int i = 0; i < puzzleinput.Length; i++)
{
    game_possible = true;

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
                    game_possible = false;
            }
            else if(cubecount[1] == "green")
            {
                if(num_cubes > maxgreen)
                    game_possible = false;
            }
            else if(cubecount[1] == "blue")
            {
                if(num_cubes > maxblue)
                    game_possible = false;
            }
        }
    }

    if(game_possible == true)
    {
        possible_games_sum += (i + 1);
    }
}

Console.WriteLine($"Sum of possible games: {possible_games_sum}");