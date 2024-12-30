// https://adventofcode.com/2024/day/11#part2

string[] puzzleinput = File.ReadAllLines(@"..\input.txt");

string[] splitted = puzzleinput[0].Split(" ");

int[] stones = new int[splitted.Length];

for(int i = 0; i < stones.Length; i++)
{
    stones[i] = Convert.ToInt32(splitted[i]);
}

Dictionary<long,long> dict = new Dictionary<long,long>();
dict[0] = 0;
dict[1] = 0;
long count = 0;

for(int i = 0; i < stones.Length; i++)
{
    if(dict.ContainsKey(stones[i]))
        dict[stones[i]]++;
    else
        dict.Add(stones[i], 1);
}

for(int i = 0; i < 75; i++)
{
    count = 0;
    long zero_to_one = 0;
    Dictionary<long,long> dict_to_add = new Dictionary<long,long>();

    foreach(var stone in dict)
    {
        if(stone.Value > 0)
        {
            // 0 replaced by 1
            if(stone.Key == 0)
            {
                zero_to_one = stone.Value;
                continue;
            }
            // Even digit replaced by two stones
            else if(stone.Key.ToString().Length % 2 == 0)
            {
                string a = stone.Key.ToString().Substring(0, stone.Key.ToString().Length / 2);
                string b = stone.Key.ToString().Substring(stone.Key.ToString().Length / 2, stone.Key.ToString().Length / 2);

                long a1 = Convert.ToInt64(a);
                long b1 = Convert.ToInt64(b);

                if(dict_to_add.ContainsKey(a1))
                    dict_to_add[a1] += stone.Value;
                else
                    dict_to_add.Add(a1,stone.Value);
                
                if(dict_to_add.ContainsKey(b1))
                    dict_to_add[b1] += stone.Value;
                else
                    dict_to_add.Add(b1,stone.Value);

                dict[stone.Key] = 0;

                continue;
            }
            // Everything else
            else
            {
                if(dict_to_add.ContainsKey(stone.Key * 2024))
                    dict_to_add[stone.Key * 2024] += stone.Value;
                else
                    dict_to_add.Add(stone.Key * 2024, stone.Value);

                dict[stone.Key] = 0;
            }
        }
    }

    dict[0] -= zero_to_one;
    dict[1] += zero_to_one;

    foreach(var stone in dict_to_add)
    {
        if(dict.ContainsKey(stone.Key))
            dict[stone.Key] += dict_to_add[stone.Key];
        else
            dict.Add(stone.Key, dict_to_add[stone.Key]);
    }

    foreach(var stone in dict)
    {
        count += stone.Value;
    }
}

Console.WriteLine($"Total stones = {count}");