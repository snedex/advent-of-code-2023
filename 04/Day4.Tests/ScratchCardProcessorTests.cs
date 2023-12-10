namespace Day4.Tests;

public class ScratchCardProcessorTests
{
    [Theory]
    [InlineData(@"testinput.txt", 6)]
    [InlineData(@"input.txt", 212)]
    public void BuildCardsFromFileShouldPass(string filename, int expectedCardCount)
    {
        var processor = new ScratchCardProcessor(filename);
        processor.BuildCards();

        Assert.Equal(expectedCardCount, processor.ScratchCards.Count());
    }

    [Theory]
    [InlineData("Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53", 1, 8, 5, 8)]
    [InlineData("Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19", 2, 2, 5, 8)]
    [InlineData("Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1", 3, 2, 5, 8)]
    [InlineData("Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83", 4, 1, 5, 8)]
    [InlineData("Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36", 5, 0, 5, 8)]
    [InlineData("Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11", 6, 0, 5, 8)]
    [InlineData("Card 136: 72 86 53 71 20 73 28 92 67  4 | 24  9 44 93 13 38  3 97 14 78 23 10 48 63  2 52 50 89 26 68 57 33 43 39  5", 136, 0, 10, 25)]
    [InlineData("Card 198:  8 33 36 79  6 94 71 90 61  4 | 76 94 15 50 85  4 79  6 69 89 21 28 26 66 55 10 61 68 45 46 34 36 90  8 33", 198, 256, 10, 25)]
    [InlineData("Card  79:  9 22 30 32 63 56 10 16 57 43 | 22 51 55 42 84 58 70 62 71 48 52 82 36 43 93 18 96 60 21 89 31 56 30 16 37", 79, 16, 10, 25)]
    [InlineData("Card   8: 40 66 64 42 52  5 18 49 67 94 | 23  5 66 53 33 24 95 86  2 46 67 87 68 71 83 21 78 41 29 62 70 69 61 60 93", 8, 4, 10, 25)]
    public void BuildCardFromLineShouldPass(string gameLine, int expectedCardNo, int expectedScore, int expectedWinningCount, int expectedGameCount)
    {
        var processor = new ScratchCardProcessor();
        var card = processor.BuildCard(gameLine);

        Assert.Equal(expectedCardNo, card.CardNo);
        Assert.Equal(expectedScore, card.Score);
        Assert.Equal(expectedWinningCount, card.WinningNumbers.Count);
        Assert.Equal(expectedGameCount, card.GameNumbers.Count);
    }

    [Theory]
    [InlineData(@"testinput.txt", 13)]
    public void BuildCardTotalScoreFromFileShouldPass(string filename, int expectedScore)
    {
        var processor = new ScratchCardProcessor(filename);
        var score = processor.GetTotalScore();

        Assert.Equal(expectedScore, score);
    }
}