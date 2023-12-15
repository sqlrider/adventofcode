
using System.Text;

string[] puzzleinput = File.ReadAllLines(@"C:\Study\adventofcode\2023\15\input.txt");
string[] temp = puzzleinput[0].Split(',');

int bytecount;
int current_value;

List<(string label, int focal)>[] arr = new List<(string,int)>[256];

for(int i = 0; i < arr.Length; i++)
{
    arr[i] = new List<(string,int)>();
}

foreach(string t in temp)
{
    string label;
    int focal = 0;

    if(t.Contains('='))
    {
        label = t.Substring(0,t.IndexOf('='));
        focal = Convert.ToInt32(t.Substring(t.IndexOf('=') + 1));
    }
    else
    {
        label = t.Substring(0,t.IndexOf('-'));
    }

    bytecount = Encoding.ASCII.GetByteCount(label);
    byte[] ascii_bytes = new byte[bytecount];
    ascii_bytes = Encoding.ASCII.GetBytes(label);

    current_value = 0;

    foreach(byte b in ascii_bytes)
    {
        current_value += (int)b;
        current_value *= 17;
        current_value %= 256;
    }
    
    if(t.Contains('='))
    {
        bool labelexists = false;
        for(int i = 0; i < arr[current_value].Count; i++)
        {
            if(arr[current_value][i].label == label)
            {
                arr[current_value][i] = (label,focal);
                labelexists = true;
            }
        }

        if(!labelexists)
        {
            arr[current_value].Add((label, focal));
        }
    }
    else
    {
        for(int i = 0; i < arr[current_value].Count; i++)
        {
            if(arr[current_value][i].label == label)
                arr[current_value].RemoveAt(i);
        }
    }
}

int totalpower = 0;
int focuspower;

for(int i = 0; i < arr.Length; i++)
{
    if(arr[i].Count > 0)
    {
        for(int j = 0; j < arr[i].Count; j++)
        {
            focuspower = 1 + i;
            focuspower *= ((j + 1) * arr[i][j].focal);
            totalpower += focuspower;
        }
    }
}

Console.WriteLine($"Total focus power: {totalpower}");