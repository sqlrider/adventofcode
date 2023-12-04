
string[] puzzleinput = File.ReadAllLines(@"C:\Study\adventofcode\2023\04\input.txt");

// Populate tuple array for game lines
List<Tuple<int,List<string>,List<string>>> cards = new List<Tuple<int,List<string>,List<string>>>();

for(int i = 0; i < puzzleinput.Length; i++)
{
    int game_id = Convert.ToInt32(puzzleinput[i].Substring(puzzleinput[i].IndexOf('d') + 1, puzzleinput[i].IndexOf(':') - puzzleinput[i].IndexOf('d') - 1).Trim());
    string[] winning_nums_str = puzzleinput[i].Substring(puzzleinput[i].IndexOf(':') + 2, puzzleinput[i].IndexOf('|') - puzzleinput[i].IndexOf(':') - 2).Split(' ');
    string[] nums_str = puzzleinput[i].Substring(puzzleinput[i].IndexOf('|') + 2, puzzleinput[i].Length - puzzleinput[i].IndexOf('|') - 2).Split(' ');

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

    var card = new Tuple<int,List<string>,List<string>>(game_id, winning_nums, nums);
    cards.Add(card);
}

// Create dictionaries of cards to card points/instances
Dictionary<int,int> card_dict = new Dictionary<int,int>();
Dictionary<int,int> card_score = new Dictionary<int,int>();
int total = 0;

Console.WriteLine(total);

foreach(Tuple<int,List<string>,List<string>> card in cards)
{
    int subtotal = 0;

    foreach(string num in card.Item3)
    {
        foreach(string winning in card.Item2)
        {
            if(num == winning)
            {
                subtotal++;
            }
        }
    }

    card_dict.Add(card.Item1,subtotal);
    card_score.Add(card.Item1,1);
}

for(int i = 1; i <= card_dict.Count; i++)
{
    Console.WriteLine($"Card {i} : {card_dict[i]} points, {card_score[i]} instances");

    for(int j = 1; j <= card_dict[i]; j++)
    {
       card_score[i + j] += card_score[i];
    }

    total += card_score[i];
}

Console.WriteLine($"Total: {total}");
