// https://adventofcode.com/2024/day/22#part2

string[] puzzleinput = File.ReadAllLines(@"..\input.txt");

long[] secretnums = new long[puzzleinput.Length];
long[][] prices = new long[puzzleinput.Length][];
for(long i = 0; i < prices.Length; i++)
    prices[i] = new long[2000];

long[][] pricediffs = new long[puzzleinput.Length][];
for(long i = 0; i < pricediffs.Length; i++)
    pricediffs[i] = new long[2000];

List<string> all_sequences = new List<string>();
Dictionary<string,int>[] dict = new Dictionary<string,int>[puzzleinput.Length];
for(long i = 0; i < dict.Length; i++)
    dict[i] = new Dictionary<string,int>();

for(long i = 0; i < puzzleinput.Length; i++)
    secretnums[i] = Convert.ToInt64(puzzleinput[i]);

long tmp;

for(long n = 0; n < 2000; n++)
{
    for(long i = 0; i < secretnums.Length; i++)
    {
        // last digit
        prices[i][n] = secretnums[i] % 10;

        // Calculate the result of multiplying the secret number by 64. Then, mix this result longo the secret number. Finally, prune the secret number.
        tmp = secretnums[i] * 64;
        secretnums[i] = secretnums[i] ^ tmp;
        secretnums[i] = secretnums[i] % 16777216;

        // Calculate the result of dividing the secret number by 32. Round the result down to the nearest longeger. Then, mix this result longo the secret number. Finally, prune the secret number.
        tmp = secretnums[i] / 32;
        secretnums[i] = secretnums[i] ^ tmp;
        secretnums[i] = secretnums[i] % 16777216;

        // Calculate the result of multiplying the secret number by 2048. Then, mix this result longo the secret number. Finally, prune the secret number.
        tmp = secretnums[i] * 2048;
        secretnums[i] = secretnums[i] ^ tmp;
        secretnums[i] = secretnums[i] % 16777216;
    }
}

// For every monkey
for(int n = 0; n < secretnums.Length; n++)
{
    for(long i = 1; i < prices[n].Length; i++)
    {
        pricediffs[n][i] = prices[n][i] - prices[n][i-1];
    }

    string diffs_string;
    for(int i = 1; i < pricediffs[n].Length - 3; i++)
    {
        diffs_string = pricediffs[n][i] + "," + pricediffs[n][i+1] + "," + pricediffs[n][i+2] + "," + pricediffs[n][i+3];
        if(!dict[n].ContainsKey(diffs_string))
            dict[n][diffs_string] = Convert.ToInt32(prices[n][i+3]);
        all_sequences.Add(diffs_string);
    }
}

int total;
int max_total = Int32.MinValue;
string best_seq = "";
foreach(string seq in all_sequences)
{
    total = 0;
    foreach(var d in dict)
    {
        if(d.ContainsKey(seq))
            total += d[seq];
    }

    if(total > max_total)
    {
        max_total = total;
        best_seq = seq;
    }
}

Console.WriteLine($"Best total is {max_total} from sequence {best_seq}");