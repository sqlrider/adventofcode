
string[] puzzleinput = File.ReadAllLines(@"C:\Study\adventofcode\2023\04\input.txt");

int total = 0;

foreach(string line in puzzleinput)
{
    int subtotal = 0;

    Console.WriteLine(line);

    string[] winning_nums_str = line.Substring(line.IndexOf(':') + 2, line.IndexOf('|') - line.IndexOf(':') - 2).Split(' ');
    string[] nums_str = line.Substring(line.IndexOf('|') + 2, line.Length - line.IndexOf('|') - 2).Split(' ');

    List<string> winning_nums = new List<string>();
    List<string> nums = new List<string>();

    foreach(string n in winning_nums_str)
    {
       if(n != "")
            winning_nums.Add(n);
    }
    foreach(string n in nums_str)
    {
        if(n != "")
            nums.Add(n);
    }

    foreach(string n in nums)
    {
        foreach(string winning in winning_nums)
        {
            if(n == winning)
            {
                if(subtotal == 0)
                    subtotal = 1;
                else
                    subtotal *= 2;
            }
        }
    }

    Console.WriteLine($"Subtotal: {subtotal}");

    total += subtotal;
}

Console.WriteLine($"Total: {total}");