
string[] puzzleinput = File.ReadAllLines(@"C:\Study\adventofcode\2023\1\input.txt");

int total = 0;

foreach(string line in puzzleinput)
{
    int first_digit = 0;
    int second_digit = 0;

    foreach(char chr in line)
    {
        if(first_digit == 0)
        {
            if(Int32.TryParse(chr.ToString(), out int a))
                first_digit = a;
        }
        else
        {
            if(Int32.TryParse(chr.ToString(), out int a))
                second_digit = a;
        }
    }

    if (second_digit == 0)
        second_digit = first_digit;

    int line_total = Int32.Parse((first_digit.ToString() + second_digit.ToString()));

    total += line_total;
}

Console.WriteLine(total);
