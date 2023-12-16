namespace Day6.Tests;

public class BoatRaceProcessorTests
{
    [Theory]
    [InlineData("testinput.txt", 3)]
    [InlineData("input.txt", 4)]
    public void ProcessInput_FileInput_CorrectNumberOfEntries(string filename, int expectedEntries)
    {
        var processor = new BoatRaceProcessor(filename);
        Assert.Equal(expectedEntries, processor.RaceRecords.Count);
    }

    [Theory]
    [InlineData("testinput.txt", 7, 9)]
    [InlineData("input.txt", 56, 546)]
    public void ProcessInput_TestInput_CorrectDataInRace1(string filename, int race1Time, int race1Distance)
    {
        var processor = new BoatRaceProcessor(filename);
        Assert.Equal(race1Time, processor.RaceRecords.Keys.First());
        Assert.Equal(race1Distance, processor.RaceRecords.Values.First());
    }

    [Theory]
    [InlineData("testinput.txt", 0, 0, 0)]
    [InlineData("testinput.txt", 0, 1, 6)]
    [InlineData("testinput.txt", 0, 2, 10)]
    [InlineData("testinput.txt", 0, 3, 12)]
    [InlineData("testinput.txt", 0, 4, 12)]
    [InlineData("testinput.txt", 0, 5, 10)]
    [InlineData("testinput.txt", 0, 6, 6)]
    [InlineData("testinput.txt", 0, 7, 0)]
    public void ProcessRace_TestRaceDistanceCalculation_MatchesExpected(string filename, int raceIndex, int windupMilliseconds, int expectedDistance)
    {
        var processor = new BoatRaceProcessor(filename);
        var distance = processor.Race(raceIndex, windupMilliseconds);

        Assert.Equal(expectedDistance, distance);
    }

    [Theory]
    [InlineData("testinput.txt", 0, 4)]
    [InlineData("testinput.txt", 1, 8)]
    [InlineData("testinput.txt", 2, 9)]
    public void ProcessRace_TestRaceWinningScenariosCount_MatchesExpected(string fileName, int raceIndex, int expectedScenarios)
    {
        var processor = new BoatRaceProcessor(fileName);
        var scenarios = processor.GetNumWinningScenarios(raceIndex);

        Assert.Equal(expectedScenarios, scenarios);
    }

    [Theory]
    [InlineData("testinput.txt", 288)]
    public void ProcessRace_TestSumScenarios_MatchesExpected(string fileName, int expectedSum)
    {
        var processor = new BoatRaceProcessor(fileName);
        var scenarios = processor.SumScenarios();

        Assert.Equal(expectedSum, scenarios);
    }
}