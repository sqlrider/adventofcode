
string[] puzzleinput = File.ReadAllLines(@"C:\Study\adventofcode\2023\09\input.txt");

long total = 0;

for(int i = 0; i < puzzleinput.Length; i++)
{
    var rows = new List<List<int>>();
    string[] nums = puzzleinput[i].Trim().Split(' ');

    var row = new List<int>();

    for(int j = 0; j < nums.Length; j++)
    {
        row.Add(Convert.ToInt32(nums[j]));
    }

    rows.Add(row);


    bool allzeroes = false;

    while(!allzeroes)
    {
        var new_row = new List<int>();

        int deepest_row = rows.Count - 1;

        for(int j = 0; j < rows[deepest_row].Count - 1; j++)
        {
            new_row.Add(rows[deepest_row][j+1] - rows[deepest_row][j]);
        }

        rows.Add(new_row);

        allzeroes = true;
        foreach(int k in new_row)
        {
            if(k != 0)
                allzeroes = false;
        }
    }
    
    for(int j = rows.Count - 1; j > 0; j--)
    {
        rows[j-1].Add(rows[j-1].Last() + rows[j].Last());
    }

    total += rows[0].Last();
}

Console.WriteLine($"Total: {total}");

