using System.Text.RegularExpressions;

string[] puzzleinput = File.ReadAllLines(@"C:\Study\adventofcode\2023\06\input.txt");

string[] tmp = puzzleinput[0].Split(':');
string[] tmp2 = puzzleinput[1].Split(':');
string[] times = Regex.Split(tmp[1].Trim(), @"\D+");
string[] distances = Regex.Split(tmp2[1].Trim(), @"\D+");

int total = 1;

for(int i = 0; i < times.Length; i++)
{
    int time = Convert.ToInt32(times[i]);
    int distance = Convert.ToInt32(distances[i]);

    Console.WriteLine($"Time: {time}, Distance: {distance}");
    
    int waystowin = 0;

    for(int j = 0; j < time; j++)
    {
        int score = j * (time - j);
        Console.WriteLine($"Travelled at {j} ms/s for {time - j} seconds, score: {score}");
        if(score > distance)
        {
            waystowin++;
        }
    }

    total *= waystowin;
}

Console.WriteLine($"Total: {total}");