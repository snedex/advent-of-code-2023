namespace _02.Tests;

public class GameValidatorTests
{
    [Theory]
    [InlineData(12, 13, 14, "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green")]
    [InlineData(12, 13, 14, "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue")]
    [InlineData(12, 13, 14, "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green")]
    public void GameIsValidShouldPass(int redCubes, int greenCubes, int blueCubes, string gameRecord)
    {
        var validator = new GameValidator(redCubes, greenCubes, blueCubes);
        var rounds = new GameParser().ParseGameLine(gameRecord);

        var result = validator.IsGameValid(rounds);

        Assert.True(result, "This game should be possible");
    }

    [Theory]
    [InlineData(12, 13, 14, "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red")]
    [InlineData(12, 13, 14, "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red")]
    public void GameIsInvalidShouldPass(int redCubes, int greenCubes, int blueCubes, string gameRecord)
    {
        var validator = new GameValidator(redCubes, greenCubes, blueCubes);
        var rounds = new GameParser().ParseGameLine(gameRecord);

        var result = validator.IsGameValid(rounds);

        Assert.False(result, "This game should be impossible");
    }

    [Theory]
    [InlineData("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", 4, 2, 6)]
    [InlineData("Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue", 1, 3, 4)]
    [InlineData("Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red", 20, 13, 6)]
    [InlineData("Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red", 14, 3, 15)]
    [InlineData("Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green", 6, 3, 2)]
    public void MinCubesRequiredToPlay(string gameRecord, int expectedRed, int expectedGreen, int expectedBlue)
    {
        var validator = new GameValidator(0, 0, 0);
        var rounds = new GameParser().ParseGameLine(gameRecord);

        (int requiredRed, int requiredGreen, int requiredBlue) = validator.MinCubesToPlay(rounds);

        Assert.Equal(expectedRed, requiredRed);
        Assert.Equal(expectedGreen, requiredGreen);
        Assert.Equal(expectedBlue, requiredBlue);
    }
}