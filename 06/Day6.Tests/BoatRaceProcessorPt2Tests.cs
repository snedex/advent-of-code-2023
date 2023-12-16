namespace Day6.Tests;

public class BoatRaceProcessorPt2Tests
{
    [Theory]
    [InlineData("testinput.txt", 1)]
    [InlineData("input.txt", 1)]
    public void ProcessInput_FileInput_CorrectNumberOfEntries(string filename, int expectedEntries)
    {
        var processor = new BoatRaceProcessorPt2(filename);
        Assert.Equal(expectedEntries, processor.RaceRecords.Count);
    }

    [Theory]
    [InlineData("testinput.txt", 71530, 940200)]
    public void ProcessInput_TestInput_CorrectDataInRace1(string filename, int race1Time, int race1Distance)
    {
        var processor = new BoatRaceProcessorPt2(filename);
        Assert.Equal(race1Time, processor.RaceRecords.Keys.First());
        Assert.Equal(race1Distance, processor.RaceRecords.Values.First());
    }

    [Theory]
    [InlineData("testinput.txt", 0, 71503)]
    public void ProcessRace_TestRaceWinningScenariosCount_MatchesExpected(string fileName, int raceIndex, int expectedScenarios)
    {
        var processor = new BoatRaceProcessorPt2(fileName);
        var scenarios = processor.GetNumWinningScenarios(raceIndex);

        Assert.Equal(expectedScenarios, scenarios);
    }
}