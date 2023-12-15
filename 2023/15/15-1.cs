
using System.Text;

string[] puzzleinput = File.ReadAllLines(@"C:\Study\adventofcode\2023\15\input.txt");
string[] temp = puzzleinput[0].Split(',');

int bytecount;
int current_value;
int total_value = 0;

foreach(string t in temp)
{
    bytecount = Encoding.ASCII.GetByteCount(t);
    byte[] ascii_bytes = new byte[bytecount];
    ascii_bytes = Encoding.ASCII.GetBytes(t);

    current_value = 0;

    foreach(byte b in ascii_bytes)
    {
        current_value += (int)b;
        current_value *= 17;
        current_value %= 256;
    }

    total_value += current_value;
}

Console.WriteLine($"Total value: {total_value}");