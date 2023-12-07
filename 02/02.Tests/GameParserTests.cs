

public class GameParserTests
{

    [Theory]
    [InlineData("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", 1)]
    [InlineData("Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue", 2)]
    [InlineData("Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red", 3)]
    [InlineData("Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red", 4)]
    [InlineData("Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green", 5)]
    public void CorrectGameIdShouldPass(string gameLine, int expectedGameId)
    {
        var parser = new GameParser();
        var rounds = parser.ParseGameLine(gameLine);

        Assert.NotNull(rounds);

        foreach(var round in rounds)
        {
            Assert.Equal(expectedGameId, round.GameId);
        }
    }

    [Theory]
    [InlineData("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", 3)]
    [InlineData("Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue", 3)]
    [InlineData("Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red", 3)]
    [InlineData("Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red", 3)]
    [InlineData("Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green", 2)]
    public void CorrectNumberOfRoundsShouldPass(string gameLine, int expectedRounds)
    {
        var parser = new GameParser();
        var rounds = parser.ParseGameLine(gameLine);

        Assert.NotNull(rounds);
        Assert.Equal(expectedRounds, rounds.Count());
    }

    [Theory]
    [InlineData("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", 1, 4, 0, 3, 1, 2, 6, 0, 2, 0)]
    [InlineData("Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue", 2, 0, 2, 1, 1, 3, 4, 0, 1, 1)]
    [InlineData("Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red", 3, 20, 8, 6, 4, 13, 5, 1, 5, 0)]
    [InlineData("Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red", 4, 3, 1, 6, 6, 3, 0, 14, 3, 15)]
    public void ValidCubesParsed3RoundsShouldPass(string gameLine, int gameId, int round1Red, int round1Green, int round1Blue, int round2Red, int round2Green, int round2Blue, int round3Red, int round3Green, int round3Blue)
    {
        var parser = new GameParser();
        var rounds = parser.ParseGameLine(gameLine);

        Assert.Collection(rounds, e => {
            Assert.Equal(new GameRecord(gameId, round1Red, round1Green, round1Blue), e);
        }, e => {
            Assert.Equal(new GameRecord(gameId, round2Red, round2Green, round2Blue), e);
        }, e => {
            Assert.Equal(new GameRecord(gameId, round3Red, round3Green, round3Blue), e);    
        });
    }

    [Theory]
    [InlineData("Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green", 5, 6, 3, 1, 1, 2, 2)]
    public void ValidCubesParsed2RoundsShouldPass(string gameLine, int gameId, int round1Red, int round1Green, int round1Blue, int round2Red, int round2Green, int round2Blue)
    {
        var parser = new GameParser();
        var rounds = parser.ParseGameLine(gameLine);

        Assert.Collection(rounds, e => {
            Assert.Equal(new GameRecord(gameId, round1Red, round1Green, round1Blue), e);
        }, e => {
            Assert.Equal(new GameRecord(gameId, round2Red, round2Green, round2Blue), e);
        });
    }
}