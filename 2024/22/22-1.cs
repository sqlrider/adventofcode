// https://adventofcode.com/2024/day/22

string[] puzzleinput = File.ReadAllLines(@"..\input.txt");

long[] secretnums = new long[puzzleinput.Length];
for(int i = 0; i < puzzleinput.Length; i++)
    secretnums[i] = Convert.ToInt64(puzzleinput[i]);

long tmp;

for(int n = 0; n < 2000; n++)
{
    for(int i = 0; i < secretnums.Length; i++)
    {
        // Calculate the result of multiplying the secret number by 64. Then, mix this result into the secret number. Finally, prune the secret number.
        tmp = secretnums[i] * 64;
        secretnums[i] = secretnums[i] ^ tmp;
        secretnums[i] = secretnums[i] % 16777216;

        // Calculate the result of dividing the secret number by 32. Round the result down to the nearest integer. Then, mix this result into the secret number. Finally, prune the secret number.
        tmp = secretnums[i] / 32;
        secretnums[i] = secretnums[i] ^ tmp;
        secretnums[i] = secretnums[i] % 16777216;

        // Calculate the result of multiplying the secret number by 2048. Then, mix this result into the secret number. Finally, prune the secret number.
        tmp = secretnums[i] * 2048;
        secretnums[i] = secretnums[i] ^ tmp;
        secretnums[i] = secretnums[i] % 16777216;
    }
}

long sum = 0;
for(int i = 0; i < secretnums.Length; i++)
    sum += secretnums[i];

Console.WriteLine($"Sum = {sum}");
