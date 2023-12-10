
using System.Text.RegularExpressions;

public partial class ScratchCardProcessor
{
    private string InputFileName = string.Empty;

    public IList<ScratchCard> ScratchCards { get; init; } = new List<ScratchCard>();

    private const string GameSeparator = "|";
    private const string CardNumberMarker = "Card";
    private const string TicketSeparator = ":";

    public ScratchCardProcessor(string fileName)
    {
        InputFileName = fileName;
    }

    public ScratchCardProcessor()
    {

    }

    public int ScratchCardMulitplierResult()
    {
        BuildCards();
        for(int i = 0; i < ScratchCards.Count; i++)
        {
            var matches = ScratchCards[i].Matches;
            if(matches == 0)
            {
                continue;
            }

            for(int winIdx = ScratchCards[i].CardNo; winIdx < ScratchCards[i].CardNo + matches; winIdx++)
            {
                ScratchCards[winIdx].Copies += 1 * ScratchCards[i].Copies;
            }
        }

        return ScratchCards.Sum(c => c.Copies);
    }

    public int GetTotalScore()
    {
        BuildCards();
        return ScratchCards.Sum(c => c.Score);
    }

    public void BuildCards()
    {
        ScratchCards.Clear();

        foreach (var line in File.ReadLines(InputFileName))
        {
            if(string.IsNullOrEmpty(line) || !line.StartsWith(CardNumberMarker))
            {
                continue;
            }

            ScratchCards.Add(BuildCard(line));
        }
    }

    public ScratchCard BuildCard(string line)
    {
        line = line.Replace(CardNumberMarker, string.Empty);

        var segments = line.Split(new string[] { TicketSeparator, GameSeparator }, StringSplitOptions.TrimEntries);
        var card = new ScratchCard();

        for(int segmentIndex = 0; segmentIndex < segments.Length; segmentIndex++)
        {
            var segment = segments[segmentIndex];
            foreach(var part in segment.Split(" ", StringSplitOptions.RemoveEmptyEntries))
            {
                var value = int.Parse(part);
                switch(segmentIndex % 3)
                {
                    case 0: 
                        card.CardNo = value;
                        break;
                    case 1:
                        card.WinningNumbers.Add(value);
                        break;
                    case 2: 
                        card.GameNumbers.Add(value);
                        break;
                }
            }
        }

        return card;
    }
}

public class ScratchCard
{
    public int CardNo { get; set; }

    public IList<int> WinningNumbers { get; init; } = new List<int>();
    public IList<int> GameNumbers { get; init; } = new List<int>();

    public int Copies { get; set; } = 1;

    public ScratchCard(int cardNo)
    {
        CardNo = cardNo;
    }

    public ScratchCard()
    {
    }

    public int Matches
    {
        get 
        {
            return GameNumbers.Intersect(WinningNumbers).Count();
        }
    }

    public int Score 
    {
        get 
        {
            var intersections = Matches;
            if(intersections == 0)
            {
                return 0;
            }

            return (int)Math.Pow(2, intersections - 1);
        }
    }
}