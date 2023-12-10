
using System.Text.RegularExpressions;

public partial class ScratchCardProcessor
{
    private string InputFileName = string.Empty;

    public IList<ScratchCard> ScratchCards { get; init; } = new List<ScratchCard>();

    private const string CardSeparator = "|";

    public ScratchCardProcessor(string fileName)
    {
        InputFileName = fileName;
    }

    public ScratchCardProcessor()
    {

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
            if(string.IsNullOrEmpty(line) || !line.StartsWith("Card"))
            {
                continue;
            }

            ScratchCards.Add(BuildCard(line));
        }
    }

    public ScratchCard BuildCard(string gameLine)
    {
        var cardMatcher = CardMatchRegex().Match(gameLine);
        var cardNo = int.Parse(cardMatcher.Groups[1].Value);

        gameLine = gameLine.Replace(cardMatcher.Groups[1].Value, string.Empty);

        var numberMatcher = GameNumberRegex();
        var splitIndex = gameLine.IndexOf(CardSeparator);

        var card = new ScratchCard(cardNo);

        foreach(var numberMatch in numberMatcher.Matches(gameLine).Cast<Match>())
        {
            int value = int.Parse(numberMatch.Value);
            if (numberMatch.Index < splitIndex)
            {
                card.WinningNumbers.Add(value);
            }
            else
            {
                card.GameNumbers.Add(value);
            }
        }

        return card;
    }

    [GeneratedRegex("Card[\\s]+([0-9]+):")]
    private static partial Regex CardMatchRegex();
    [GeneratedRegex("([0-9]+[^:]?)")]
    private static partial Regex GameNumberRegex();
}

public class ScratchCard
{
    public int CardNo { get; init; }

    public IList<int> WinningNumbers { get; init; } = new List<int>();
    public IList<int> GameNumbers { get; init; } = new List<int>();

    public ScratchCard(int cardNo)
    {
        CardNo = cardNo;
    }

    public int Score 
    {
        get 
        {
            var intersections = WinningNumbers.Intersect(GameNumbers).Count();
            if(intersections == 0)
            {
                return 0;
            }

            return (int)Math.Pow(2, intersections - 1);
        }
    }
}