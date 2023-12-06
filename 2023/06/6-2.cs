using System.Text.RegularExpressions;

string[] puzzleinput = File.ReadAllLines(@"C:\Study\adventofcode\2023\06\input.txt");

string[] tmp = puzzleinput[0].Split(':');
string tmp2 = tmp[1];
string[] tmp3 = puzzleinput[1].Split(':');
string tmp4 = tmp3[1];

string time_str = "";
string distance_str = "";

for(int i = 0; i < tmp2.Length; i++)
{
    if(Int32.TryParse(tmp2[i].ToString(),out _))
        time_str += tmp2[i].ToString();
}

for(int i = 0; i < tmp4.Length; i++)
{
    if(Int32.TryParse(tmp4[i].ToString(),out _))
        distance_str += tmp4[i].ToString();
}

Console.WriteLine(time_str);
Console.WriteLine(distance_str);

long time = Convert.ToInt64(time_str);
long distance = Convert.ToInt64(distance_str);

Console.WriteLine($"Time: {time}, Distance: {distance}");
    
int waystowin = 0;

for(long j = 0; j < time; j++)
{
    long score = j * (time - j);
    //Console.WriteLine($"Travelled at {j} ms/s for {time - j} seconds, score: {score}");
    if(score > distance)
    {
        waystowin++;
    }
}

Console.WriteLine($"Total: {waystowin}");
