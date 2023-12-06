

string[] puzzleinput = File.ReadAllLines(@"C:\Study\adventofcode\2023\1\input.txt");

int total = 0;

for(int i = 0; i < puzzleinput.Length; i++)
{
    Console.WriteLine(puzzleinput[i]);

    // Just sanitise the input to digits instead of trying to find first/last substring, so logic from 1 can be re-used.
    // Can't replace 'one' with '1one' or 'one1' because some letters are shared for contiguous numbers i.e. 'oneeight' which would be broken by this unless
    // using more complicated all-at-once updating mechanism.
    // So just put number in middle of word instead.
    string sanitised_input = puzzleinput[i].Replace("one", "o1e").Replace("two", "t2o").Replace("three", "th3ee").Replace("four", "f4ur").Replace("five", "f5ve").Replace("six", "s6x").Replace("seven", "se7en").Replace("eight","ei8ht").Replace("nine","n9ne");
    
    int first_digit = 0;
    int second_digit = 0;

    foreach(char chr in sanitised_input)
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
