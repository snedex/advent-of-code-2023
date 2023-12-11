namespace Day5.Tests;

public class UnitTest1
{
    [Theory]
    [InlineData(@"testinput.txt")]
    public void ParseInFileShouldPass(string filename)
    {
        var exception = Record.Exception(() => new Mapper(filename));

        Assert.Null(exception);
    }

    [Theory]
    [InlineData(@"testinput.txt", 4)]
    public void ParseInFileCorrectNoSeedsShouldPass(string filename, int expectedSeedCount)
    {
        var map = new Mapper(filename);
        Assert.Equal(expectedSeedCount, map.Seeds.Count);
    }

    [Theory]
    [InlineData(@"testinput.txt", 2)]
    public void ParseInFileCorrectNoSoilShouldPass(string filename, int expectedSoilCount)
    {
        var map = new Mapper(filename);
        Assert.Equal(expectedSoilCount, map.SeedToSoil.Count);
    }

    [Theory]
    [InlineData(@"testinput.txt", 3)]
    public void ParseInFileCorrectNoFertilizerShouldPass(string filename, int expectedFertilizerCount)
    {
        var map = new Mapper(filename);
        Assert.Equal(expectedFertilizerCount, map.SoilToFertilizer.Count);
    }

    [Theory]
    [InlineData(@"testinput.txt", 4)]
    public void ParseInFileCorrectNoWaterShouldPass(string filename, int expectedWaterCount)
    {
        var map = new Mapper(filename);
        Assert.Equal(expectedWaterCount, map.FertilizerToWater.Count);
    }

    [Theory]
    [InlineData(@"testinput.txt", 2)]
    public void ParseInFileCorrectNoLightShouldPass(string filename, int expectedLightCount)
    {
        var map = new Mapper(filename);
        Assert.Equal(expectedLightCount, map.WaterToLight.Count);
    }

    [Theory]
    [InlineData(@"testinput.txt", 3)]
    public void ParseInFileCorrectNoTemperatureShouldPass(string filename, int expectedTemperatureCount)
    {
        var map = new Mapper(filename);
        Assert.Equal(expectedTemperatureCount, map.LightToTemperature.Count);
    }

    [Theory]
    [InlineData(@"testinput.txt", 2)]
    public void ParseInFileCorrectNoHumidityShouldPass(string filename, int expectedHumidityCount)
    {
        var map = new Mapper(filename);
        Assert.Equal(expectedHumidityCount, map.TemperatureToHumidity.Count);
    }

    [Theory]
    [InlineData(@"testinput.txt", 2)]
    public void ParseInFileCorrectNoLocationShouldPass(string filename, int expectedLocationCount)
    {
        var map = new Mapper(filename);
        Assert.Equal(expectedLocationCount, map.HumitidyToLocation.Count);
    }

    [Theory]
    [InlineData(@"testinput.txt", 2)]
    public void ParseInFileCorrectNoSeedRangesShouldPass(string filename, int expectedSeedRanges)
    {
        var map = new Mapper(filename);
        Assert.Equal(expectedSeedRanges, map.SeedRanges.Count);
    }

    [Theory]
    [InlineData(@"testinput.txt", 79, 82)]
    [InlineData(@"testinput.txt", 14, 43)]
    [InlineData(@"testinput.txt", 55, 86)]
    [InlineData(@"testinput.txt", 13, 35)]
    public void SeedToLocationValueShouldPass(string filename, int seed, int expectedLocation)
    {
        var map = new Mapper(filename);
        var location = map.Convert(seed, Segment.Seeds, Segment.Location);
        Assert.Equal(expectedLocation, location);
    }

    [Theory]
    [InlineData(@"testinput.txt", 82, 46)]
    public void SeedToLocationSeedRanges(string filename, int expectedSeed, int expectedLocation)
    {
        var mapper = new Mapper(filename);
        (var location, var seed) = mapper.FindLowestLocationWithSeedRanges();

        Assert.Equal(expectedSeed, seed);
        Assert.Equal(expectedLocation, location);
    }
}