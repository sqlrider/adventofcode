// https://adventofcode.com/2024/day/7#part2

using System.Text;

string[] puzzleinput = File.ReadAllLines(@"..\input.txt");

long result = 0;

foreach(string s in puzzleinput)
{
    string[] s1 = s.Split(": ");

    long answer = Convert.ToInt64(s1[0]);
    
    string[] s2 = s1[1].Split(' ');

    long[] components = new long[s2.Length];
    
    for(int i = 0; i < s2.Length; i++)
    {
        components[i] = Convert.ToInt64(s2[i]);
    }
    
    int number_count = components.Length;
    var operators = new List<string>();

    // Prepare ternary array for combinations of operators
    for(int i = 0; i < Math.Pow(3,number_count - 1); i++)
    {
        string ternary = ToTernary(i);
        operators.Add("00000000000".Substring(0,number_count - ternary.Length - 1) + ternary);
    }

    // Main loop
    for(int i = 0; i < operators.Count; i++)
    {
        long current = components[0];
        long total = 0;

        for(int j = 0; j < number_count - 1; j++)
        {
            long next = components[j+1];

            if(operators[i][j] == '0')
                total = current + next;
            else if(operators[i][j] == '1')
                total = current * next;
            else if(operators[i][j] == '2')
                total = Convert.ToInt64(current.ToString() + next.ToString());

            current = total;
        }

        if(total == answer)
        {
            result += answer;
            break;
        }
    }
}

Console.WriteLine(result);

string ToTernary(int value)
{
    if(value == 0)
        return "";

    StringBuilder sb = new StringBuilder();

    while (value > 0)
    {
        sb.Insert(0, value % 3);
        value /= 3;
    }

    return sb.ToString();
}