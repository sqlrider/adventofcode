
string[] puzzleinput = File.ReadAllLines(@"C:\Study\adventofcode\2023\07\input.txt");

Hand[] hands = new Hand[puzzleinput.Length];

// Create a mapping of card labels to values
Dictionary<char,int> valuemap = new Dictionary<char, int>();
valuemap['2'] = 2;
valuemap['3'] = 3;
valuemap['4'] = 4;
valuemap['5'] = 5;
valuemap['6'] = 6;
valuemap['7'] = 7;
valuemap['8'] = 8;
valuemap['9'] = 9;
valuemap['T'] = 10;
valuemap['J'] = 1;
valuemap['Q'] = 12;
valuemap['K'] = 13;
valuemap['A'] = 14;

// Create array of hands and their cards
for(int i = 0; i < puzzleinput.Length; i++)
{
    string[] tmp = puzzleinput[i].Trim().Split(' ');

    hands[i] = new Hand();    

    for(int j = 0; j < tmp[0].Length; j++)
    {
        Card c = new Card();
        c.label = tmp[0][j];
        c.value = valuemap[c.label];
        hands[i].cards.Add(c);
    }

    hands[i].bid = Convert.ToInt32(tmp[1]);
}

// Set hand types
for(int i = 0; i < hands.Length; i++)
{
    if(IsHighCard(hands[i]))
        hands[i].handtype = HandType.HighCard;
    
    if(IsOnePair(hands[i]))
        hands[i].handtype = HandType.OnePair;

    if(IsTwoPair(hands[i]))
        hands[i].handtype = HandType.TwoPair;

    if(IsThreeOfAKind(hands[i]))
        hands[i].handtype = HandType.ThreeOfAKind;

    if(IsFullHouse(hands[i]))
        hands[i].handtype = HandType.FullHouse;

    if(IsFourOfAKind(hands[i]))
        hands[i].handtype = HandType.FourOfAKind;

    if(IsFiveOfAKind(hands[i]))
        hands[i].handtype = HandType.FiveOfAKind;
}

// Create lists of each type of hand so they can be sub-ranked
List<Hand> high_cards = new List<Hand>();
for(int i = 0; i < hands.Length; i++)
{
    if(hands[i].handtype == HandType.HighCard)
        high_cards.Add(hands[i]);
}

List<Hand> one_pairs = new List<Hand>();
for(int i = 0; i < hands.Length; i++)
{
    if(hands[i].handtype == HandType.OnePair)
        one_pairs.Add(hands[i]);
}

List<Hand> two_pairs = new List<Hand>();
for(int i = 0; i < hands.Length; i++)
{
    if(hands[i].handtype == HandType.TwoPair)
        two_pairs.Add(hands[i]);
}

List<Hand> three_kinds = new List<Hand>();
for(int i = 0; i < hands.Length; i++)
{
    if(hands[i].handtype == HandType.ThreeOfAKind)
        three_kinds.Add(hands[i]);
}

List<Hand> full_houses = new List<Hand>();
for(int i = 0; i < hands.Length; i++)
{
    if(hands[i].handtype == HandType.FullHouse)
        full_houses.Add(hands[i]);
}

List<Hand> four_kinds = new List<Hand>();
for(int i = 0; i < hands.Length; i++)
{
    if(hands[i].handtype == HandType.FourOfAKind)
        four_kinds.Add(hands[i]);
}

List<Hand> five_kinds = new List<Hand>();
for(int i = 0; i < hands.Length; i++)
{
    if(hands[i].handtype == HandType.FiveOfAKind)
        five_kinds.Add(hands[i]);
}

// Rank each type of hand
high_cards.Sort();
one_pairs.Sort();
two_pairs.Sort();
three_kinds.Sort();
full_houses.Sort();
four_kinds.Sort();
five_kinds.Sort();

int running_rank = 1;

for(int i = 0; i < high_cards.Count; i++)
{
    high_cards[i].rank = running_rank;
    running_rank++;
}

for(int i = 0; i < one_pairs.Count; i++)
{
    one_pairs[i].rank = running_rank;
    running_rank++;
}

for(int i = 0; i < two_pairs.Count; i++)
{
    two_pairs[i].rank = running_rank;
    running_rank++;
}

for(int i = 0; i < three_kinds.Count; i++)
{
    three_kinds[i].rank = running_rank;
    running_rank++;
}

for(int i = 0; i < full_houses.Count; i++)
{
    full_houses[i].rank = running_rank;
    running_rank++;
}

for(int i = 0; i < four_kinds.Count; i++)
{
    four_kinds[i].rank = running_rank;
    running_rank++;
}

for(int i = 0; i < five_kinds.Count; i++)
{
    five_kinds[i].rank = running_rank;
    running_rank++;
}


Console.WriteLine("High Cards:");
for(int i = 0; i < high_cards.Count; i++)
{
    Console.WriteLine($"Hand: {high_cards[i].cards[0].label}{high_cards[i].cards[1].label}{high_cards[i].cards[2].label}{high_cards[i].cards[3].label}{high_cards[i].cards[4].label}, type: {high_cards[i].handtype}, rank: {high_cards[i].rank}, bid: {high_cards[i].bid}");
}

Console.WriteLine("One Pairs:");
for(int i = 0; i < one_pairs.Count; i++)
{
    Console.WriteLine($"Hand: {one_pairs[i].cards[0].label}{one_pairs[i].cards[1].label}{one_pairs[i].cards[2].label}{one_pairs[i].cards[3].label}{one_pairs[i].cards[4].label}, type: {one_pairs[i].handtype}, rank: {one_pairs[i].rank}, bid: {one_pairs[i].bid}");
}

Console.WriteLine("Two Pairs:");
for(int i = 0; i < two_pairs.Count; i++)
{
    Console.WriteLine($"Hand: {two_pairs[i].cards[0].label}{two_pairs[i].cards[1].label}{two_pairs[i].cards[2].label}{two_pairs[i].cards[3].label}{two_pairs[i].cards[4].label}, type: {two_pairs[i].handtype}, rank: {two_pairs[i].rank}, bid: {two_pairs[i].bid}");
}

Console.WriteLine("Three Kinds:");
for(int i = 0; i < three_kinds.Count; i++)
{
    Console.WriteLine($"Hand: {three_kinds[i].cards[0].label}{three_kinds[i].cards[1].label}{three_kinds[i].cards[2].label}{three_kinds[i].cards[3].label}{three_kinds[i].cards[4].label}, type: {three_kinds[i].handtype}, rank: {three_kinds[i].rank}, bid: {three_kinds[i].bid}");
}

Console.WriteLine("Full Houses:");
for(int i = 0; i < full_houses.Count; i++)
{
    Console.WriteLine($"Hand: {full_houses[i].cards[0].label}{full_houses[i].cards[1].label}{full_houses[i].cards[2].label}{full_houses[i].cards[3].label}{full_houses[i].cards[4].label}, type: {full_houses[i].handtype}, rank: {full_houses[i].rank}, bid: {full_houses[i].bid}");
}

Console.WriteLine("Four Kinds:");
for(int i = 0; i < four_kinds.Count; i++)
{
    Console.WriteLine($"Hand: {four_kinds[i].cards[0].label}{four_kinds[i].cards[1].label}{four_kinds[i].cards[2].label}{four_kinds[i].cards[3].label}{four_kinds[i].cards[4].label}, type: {four_kinds[i].handtype}, rank: {four_kinds[i].rank}, bid: {four_kinds[i].bid}");
}

Console.WriteLine("Five Kinds:");
for(int i = 0; i < five_kinds.Count; i++)
{
    Console.WriteLine($"Hand: {five_kinds[i].cards[0].label}{five_kinds[i].cards[1].label}{five_kinds[i].cards[2].label}{five_kinds[i].cards[3].label}{five_kinds[i].cards[4].label}, type: {five_kinds[i].handtype}, rank: {five_kinds[i].rank}, bid: {five_kinds[i].bid}");
}

int total_winnings = 0;
foreach(Hand h in hands)
{
    total_winnings += h.bid * h.rank;
}

Console.WriteLine($"Total winnings: {total_winnings}");




bool IsFiveOfAKind(Hand h)
{
    Dictionary<char,int> card_count = new Dictionary<char, int>();

    foreach(var k_v in valuemap)
    {
        card_count.Add(k_v.Key, 0);
    }

    int j_count = 0;

    for(int i = 0; i < h.cards.Count; i++)
    {
        if(h.cards[i].label != 'J')
            card_count[h.cards[i].label] += 1;
        else
            j_count++;
    }

    if(card_count.Values.Max() == 5)
        return true;
    else if(card_count.Values.Max() + j_count == 5)
        return true;

    return false;
}

bool IsFourOfAKind(Hand h)
{
    Dictionary<char,int> card_count = new Dictionary<char, int>();

    foreach(var k_v in valuemap)
    {
        card_count.Add(k_v.Key, 0);
    }

    int j_count = 0;

    for(int i = 0; i < h.cards.Count; i++)
    {
        if(h.cards[i].label != 'J')
            card_count[h.cards[i].label] += 1;
        else
            j_count++;
    }

    if(card_count.Values.Max() == 4)
        return true;
    else if(card_count.Values.Max() + j_count == 4)
        return true;

    return false;
}

bool IsFullHouse(Hand h)
{
    Dictionary<char,int> card_count = new Dictionary<char, int>();

    foreach(var k_v in valuemap)
    {
        card_count.Add(k_v.Key, 0);
    }

    int j_count = 0;

    for(int i = 0; i < h.cards.Count; i++)
    {
        if(h.cards[i].label != 'J')
            card_count[h.cards[i].label] += 1;
        else
            j_count++;
    }

    bool three = false;
    bool two = false;
    int pairs = 0;

    foreach(var k_v in card_count)
    {
        if(k_v.Value == 3)
            three = true;
        if(k_v.Value == 2)
        {
            two = true;
            pairs++;
        }
    }
    
    // Deal with base case
    if(three && two)
        return true;

    // Now deal with joker
    if(pairs == 2 && j_count == 1)
    {
        return true;
    }

    return false;
}

bool IsThreeOfAKind(Hand h)
{
    Dictionary<char,int> card_count = new Dictionary<char, int>();

    foreach(var k_v in valuemap)
    {
        card_count.Add(k_v.Key, 0);
    }

    int j_count = 0;

    for(int i = 0; i < h.cards.Count; i++)
    {
        if(h.cards[i].label != 'J')
            card_count[h.cards[i].label]++;
        else
            j_count++;
    }

    if(card_count.Values.Max() == 3 && card_count.Values.Min() < 2)
        return true;
    
    if(card_count.Values.Max() + j_count == 3)
        return true;
    
    return false;
}

bool IsTwoPair(Hand h)
{
    Dictionary<char,int> card_count = new Dictionary<char, int>();

    foreach(var k_v in valuemap)
    {
        card_count.Add(k_v.Key, 0);
    }

    for(int i = 0; i < h.cards.Count; i++)
    {
        card_count[h.cards[i].label] += 1;
    }

    int pairs = 0;
    foreach(var k_v in card_count)
    {
        if(k_v.Value == 2)
            pairs++;
    }

    if(pairs == 2)
        return true;
    
    return false;
}

bool IsOnePair(Hand h)
{
    Dictionary<char,int> card_count = new Dictionary<char, int>();

    foreach(var k_v in valuemap)
    {
        card_count.Add(k_v.Key, 0);
    }

    int j_count = 0;
    for(int i = 0; i < h.cards.Count; i++)
    {
        if(h.cards[i].label != 'J')
            card_count[h.cards[i].label] += 1;
        else
            j_count++;
    }

    int pairs = 0;
    foreach(var k_v in card_count)
    {
        if(k_v.Value == 2)
            pairs++;
    }

    if(pairs == 1)
        return true;
    
    if(card_count.Values.Max() == 1 && j_count == 1)
        return true;
    
    return false;
}

bool IsHighCard(Hand h)
{
    Dictionary<char,int> card_count = new Dictionary<char, int>();

    foreach(var k_v in valuemap)
    {
        card_count.Add(k_v.Key, 0);
    }

    for(int i = 0; i < h.cards.Count; i++)
    {
        card_count[h.cards[i].label] += 1;
    }

    if(card_count.Values.Max() == 1)
        return true;
    
    return false;
}

// Hand, card and handtype definitions.
// Cards are structs as they are simple value types.
// Hands are classes so can be passed by reference and implement IComparable for easy sorting
public enum HandType
{
    HighCard,
    OnePair,
    TwoPair,
    ThreeOfAKind,
    FullHouse,
    FourOfAKind,
    FiveOfAKind
}

public struct Card
{
    public char label;
    public int value;
}

public class Hand : IComparable
{
    public Hand()
    {
        cards = new List<Card>();
        rank = 0;   
    }

    public int CompareTo(object? obj)
    {
        if(obj == null)
            return -1;

        Hand other_hand = (Hand)obj;

        if(this.cards[0].value > other_hand.cards[0].value)
        {
            return 1;
        }
        else if(this.cards[0].value == other_hand.cards[0].value)
        {
            if(this.cards[1].value > other_hand.cards[1].value)
            {
                return 1;
            }
            else if(this.cards[1].value == other_hand.cards[1].value)
            {
                if(this.cards[2].value > other_hand.cards[2].value)
                {
                    return 1;
                }
                else if(this.cards[2].value == other_hand.cards[2].value)
                {
                    if(this.cards[3].value > other_hand.cards[3].value)
                    {
                        return 1;
                    }
                    else if(this.cards[3].value == other_hand.cards[3].value)
                    {
                        if(this.cards[4].value > other_hand.cards[4].value)
                        {
                            return 1;
                        }
                    }
                }
            }
        }
        
        return -1;
    }
    
    public List<Card> cards;

    public int bid {get; set;}

    public HandType handtype {get; set;}

    public int rank {get; set;}
}
