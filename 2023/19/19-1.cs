// https://adventofcode.com/2023/day/19 - Part 1

string[] puzzleinput = File.ReadAllLines(@"C:\Study\adventofcode\2023\19\input.txt");

var parts = new List<MachinePart>();
var partrules = new Dictionary<string,PartRuleGroup>();

for(int i = 0; i < puzzleinput.Length; i++)
{
    if(puzzleinput[i].StartsWith("{"))
    {
        int x, m, a, s;
        var tmp = puzzleinput[i].Substring(1,puzzleinput[i].Length - 2).Split(',');
        x = Convert.ToInt32(tmp[0].Split('=')[1]);
        m = Convert.ToInt32(tmp[1].Split('=')[1]);
        a = Convert.ToInt32(tmp[2].Split('=')[1]);
        s = Convert.ToInt32(tmp[3].Split('=')[1]);
        parts.Add(new MachinePart(x,m,a,s));
            
    }
    else if(puzzleinput[i] != "")
    {
        var partrulename = puzzleinput[i].Substring(0,puzzleinput[i].IndexOf('{'));
        var tmp = puzzleinput[i].Substring(puzzleinput[i].IndexOf('{') + 1,puzzleinput[i].Length - (puzzleinput[i].IndexOf('{') + 2)).Split(',');

        var prg = new PartRuleGroup();
        foreach(var t in tmp)
        {
            // If contains a :, it's a rule
            if(t.Contains(':'))
            {
                var pr = new PartRule(Convert.ToChar(t.Substring(0,1)), Convert.ToChar(t.Substring(1,1)), Convert.ToInt32(t.Substring(2,t.IndexOf(':') - 2)), t.Substring(t.IndexOf(':') + 1,t.Length - t.IndexOf(':') - 1 ));
                prg.Rules.Add(pr);
            }
            else // If it doesn't, it's just a destination
                prg.DefaultAction = t;
        }
        // Add rulegroup to dictionary
        partrules[partrulename] = prg;
    }
}

// Loop over parts
for(int i = 0; i < parts.Count; i++)
{
    // All parts begin in the workflow named 'in'
    var next_rule = "in";
    var rule_found = false;

    bool dealtwith = false;

    // Loop through rules, exiting when destination is accepted/rejected
    while(!dealtwith)
    {
        rule_found = false;

        foreach(var pr in partrules[next_rule].Rules)
        {
            if(pr.Operator == '>')
            {
                // Use reflection to avoid needing switch statement for x, m, a, s categories
                if((int)parts[i].GetType().GetProperty(pr.Category.ToString()).GetValue(parts[i]) > pr.Value)
                {
                    next_rule = pr.Destination;
                    rule_found = true;
                    break;
                }
            }
            else if(pr.Operator == '<')
            {
                if((int)parts[i].GetType().GetProperty(pr.Category.ToString()).GetValue(parts[i]) < pr.Value)
                {
                    next_rule = pr.Destination;
                    rule_found = true;
                    break;
                }
            }
        }

        if(next_rule == "A")
        {
            parts[i].Accepted = true;
            dealtwith = true;
        }
        else if(next_rule == "R")
            dealtwith = true;
        else if(!rule_found)
        {
            if(partrules[next_rule].DefaultAction == "A")
            {
                parts[i].Accepted = true;
                dealtwith = true;
            }
            else if(partrules[next_rule].DefaultAction == "R")
                dealtwith = true;
            else
                next_rule = partrules[next_rule].DefaultAction;
        }
    }
}

// Sum accepted part values
int total = 0;
foreach(var p in parts)
{
    if(p.Accepted)
    {
        total += p.x + p.m + p.a + p.s;
    }
}

Console.WriteLine($"Total: {total}");


public class MachinePart
{
    public MachinePart(int x, int m, int a, int s)
    {
        this.x = x;
        this.m = m;
        this.a = a;
        this.s = s;
        this.Accepted = false;
    }
    public int x {get; set;}
    public int m {get; set;}
    public int a {get; set;}
    public int s {get; set;}
    public bool Accepted {get; set;}
}

public struct PartRule
{
    public PartRule(char Category, char Operator, int Value, string Destination)
    {
        this.Category = Category;
        this.Operator = Operator;
        this.Value = Value;
        this.Destination = Destination;
    }
    public char Category {get; set;}
    public char Operator {get; set;}
    public int Value {get; set;}
    public string Destination {get; set;}
}

public struct PartRuleGroup
{
    public PartRuleGroup()
    {
        Rules = new List<PartRule>();
        DefaultAction = "X";
    }

    public List<PartRule> Rules;
    public string DefaultAction;
}