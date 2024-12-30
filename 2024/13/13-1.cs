// https://adventofcode.com/2024/day/13

string[] puzzleinput = File.ReadAllLines(@"..\input.txt");

int totalcost = 0;
int cost;
int y,x;
int a_x, a_y, b_x, b_y;
int prize_x, prize_y;
int mincost;

// Brute force
for(int i = 0; i < puzzleinput.Length + 1; i += 4)
{
    a_x = Convert.ToInt32(puzzleinput[i].Substring(12,2));
    a_y = Convert.ToInt32(puzzleinput[i].Substring(18,2));

    b_x = Convert.ToInt32(puzzleinput[i+1].Substring(12,2));
    b_y = Convert.ToInt32(puzzleinput[i+1].Substring(18,2));

    prize_x = Convert.ToInt32(puzzleinput[i+2].Substring(puzzleinput[i+2].IndexOf("X=") + 2, puzzleinput[i+2].IndexOf("Y=") -  puzzleinput[i+2].IndexOf("X=") - 4));
    prize_y = Convert.ToInt32(puzzleinput[i+2].Substring(puzzleinput[i+2].IndexOf("Y=") + 2, puzzleinput[i+2].Length -  puzzleinput[i+2].IndexOf("Y=") - 2));

    mincost = Int32.MaxValue;

    for(int j = 0; j < 100; j++)
    {
        cost = 3 * j;
        x = 0;
        y = 0;

        x += a_x * j;
        y += a_y * j;

        for(int k = 1; k < 100; k++)
        {
            cost++;
            x += b_x;
            y += b_y;

            if(x == prize_x && y == prize_y)
                mincost = Math.Min(cost,mincost);
        }
    }

    if(mincost == Int32.MaxValue)
    {
        // Console.WriteLine("No solution");
    }
    else
        totalcost += mincost;
}

Console.WriteLine($"Total cost = {totalcost}");

