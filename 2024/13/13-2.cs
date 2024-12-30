// https://adventofcode.com/2024/day/13#part2

using System.Numerics;

string[] puzzleinput = File.ReadAllLines(@"..\input.txt");

BigInteger totalcost = 0;
BigInteger a_x, a_y, b_x, b_y;
BigInteger prize_x, prize_y;
BigInteger original_a_x, original_a_y, original_b_x, original_b_y;
BigInteger original_prize_y, original_prize_x;
BigInteger coeff_a1, coeff_a2;
BigInteger subtracted_b, subtracted_ans;
BigInteger a,b;

for(int i = 0; i < puzzleinput.Length - 1; i += 4)
{
    a_x = Convert.ToInt64(puzzleinput[i].Substring(12,2));
    a_y = Convert.ToInt64(puzzleinput[i].Substring(18,2));
    b_x = Convert.ToInt64(puzzleinput[i+1].Substring(12,2));
    b_y = Convert.ToInt64(puzzleinput[i+1].Substring(18,2));
    prize_x = Convert.ToInt64(puzzleinput[i+2].Substring(puzzleinput[i+2].IndexOf("X=") + 2, puzzleinput[i+2].IndexOf("Y=") -  puzzleinput[i+2].IndexOf("X=") - 4));
    prize_y = Convert.ToInt64(puzzleinput[i+2].Substring(puzzleinput[i+2].IndexOf("Y=") + 2, puzzleinput[i+2].Length -  puzzleinput[i+2].IndexOf("Y=") - 2));

    prize_x += 10000000000000;
    prize_y += 10000000000000;

    original_a_x = a_x;
    original_a_y = a_y;
    original_b_x = b_x;
    original_b_y = b_y;
    original_prize_x = prize_x;
    original_prize_y = prize_y;

    // Multiple each equation by opposite coefficients 
    coeff_a1 = a_x;
    coeff_a2 = a_y;
    b_x *= coeff_a2;
    prize_x *= coeff_a2;
    b_y *= coeff_a1;
    prize_y *= coeff_a1;

    // Subtract second equation from first
    subtracted_b = b_y - b_x;
    subtracted_ans = prize_y - prize_x;

    // Check if b calculates as a fraction, abort if it does as no solution exists
    if(subtracted_ans % subtracted_b > 0)
        continue;
    
    b = subtracted_ans / subtracted_b;

    // Reset variables for re-calculating for a
    a_x = Convert.ToInt64(puzzleinput[i].Substring(12,2));
    b_x = Convert.ToInt64(puzzleinput[i+1].Substring(12,2));
    prize_x = Convert.ToInt64(puzzleinput[i+2].Substring(puzzleinput[i+2].IndexOf("X=") + 2, puzzleinput[i+2].IndexOf("Y=") -  puzzleinput[i+2].IndexOf("X=") - 4));
    prize_x += 10000000000000;

    // Substitute b
    prize_x -= b_x * b;
    
    // Check if a calculates as a fraction, abort if it does as no solution exists
    if(prize_x % a_x > 0)
        continue;

    a = prize_x / a_x;

    // Check if a and b values work for original equation
    if(original_a_x * a + original_b_x * b == original_prize_x && original_a_y * a + original_b_y * b == original_prize_y)
        totalcost += a*3 + b;
}

Console.WriteLine($"Total cost = {totalcost}");

