// https://adventofcode.com/2024/day/24

string[] puzzleinput = File.ReadAllLines(@"..\input.txt");

Dictionary<string,bool> variables = new Dictionary<string,bool>();
List<Operation> ops = new List<Operation>();

// Get divider
int div = 0;
for(int i = 0; i < puzzleinput.Length; i++)
{
    if(String.IsNullOrEmpty(puzzleinput[i]))
        div = i;
}

for(int i = 0; i < div; i++)
{
    string[] tmp = puzzleinput[i].Split(':');
    variables[tmp[0]] = Convert.ToBoolean(Convert.ToInt32(tmp[1].Trim()));
}

for(int i = div + 1; i < puzzleinput.Length; i++)
{
    string[] tmp = puzzleinput[i].Split(' ');
    Operation o = new Operation(tmp[0],tmp[1],tmp[2],tmp[4]);
    ops.Add(o);
}

bool progress = true;

// Main loop
while(progress)
{
    progress = false;

    foreach(Operation o in ops)
    {
        if(variables.ContainsKey(o.operand_1) && variables.ContainsKey(o.operand_2) && !variables.ContainsKey(o.result))
        {
            if(o.op == "AND")
                variables[o.result] = variables[o.operand_1] && variables[o.operand_2];

            if(o.op == "OR")
                variables[o.result] = variables[o.operand_1] || variables[o.operand_2];

            if(o.op == "XOR")
                variables[o.result] = variables[o.operand_1] ^ variables[o.operand_2];

            progress = true;
        }
        else if(variables.ContainsKey(o.operand_1) && !variables.ContainsKey(o.operand_2) && variables.ContainsKey(o.result) && (o.op == "AND" || o.op == "XOR"))
        {
            if(o.op == "AND")
            {
                if(variables[o.result] == true)
                {
                    variables[o.operand_2] = true;
                    progress = true;
                }
                else if(variables[o.result] == false && variables[o.operand_1] == true)
                {
                    variables[o.operand_2] = false;
                    progress = true;
                }
            }
            else if(o.op == "XOR")
            {
                if(variables[o.result] == true)
                    variables[o.operand_2] = !variables[o.operand_1];
                if(variables[o.result] == false)
                    variables[o.operand_2] = variables[o.operand_1];

            progress = true;
            }
        }
        else if(variables.ContainsKey(o.operand_2) && !variables.ContainsKey(o.operand_1) && variables.ContainsKey(o.result) && (o.op == "AND" || o.op == "XOR"))
        {
            if(o.op == "AND")
            {
                if(variables[o.result] == true)
                {
                    variables[o.operand_1] = true;
                    progress = true;
                }
                else if(variables[o.result] == false && variables[o.operand_2] == true)
                {
                    variables[o.operand_1] = false;
                    progress = true;
                }
            }

            if(o.op == "XOR")
            {
                if(variables[o.result] == true)
                    variables[o.operand_1] = !variables[o.operand_2];
                if(variables[o.result] == false)
                    variables[o.operand_1] = variables[o.operand_2];

            progress = true;
            }
        }
    }

    if(!progress)
        break;
}

double n = 0;
double result = 0;
foreach(var kvp in variables.OrderBy(k => k.Key))
{
    if(kvp.Key.StartsWith('z'))
    {
        Console.WriteLine($"{kvp.Key}:{kvp.Value}");
        if(kvp.Value == true)
            result += Math.Pow(2, n);

        n++;
    }
}

Console.WriteLine($"Result = {result}");

class Operation
{
    public Operation(string operand_1, string op, string operand_2, string result)
    {
        this.operand_1 = operand_1;
        this.op = op;
        this.operand_2 = operand_2;
        this.result = result;
    }
    public string operand_1;
    public string op;
    public string operand_2;
    public string result;
}