// See https://aka.ms/new-console-template for more information
Console.WriteLine("Day 5!");

var mapper = new Mapper(@"input.txt");

var lowestLocation = -1L;
foreach(var seed in mapper.Seeds)
{
    var seedLocation = mapper.SeedToLocation(seed);
    if(lowestLocation == -1 || seedLocation < lowestLocation)
    {
        lowestLocation = seedLocation;
    }
}

Console.WriteLine(@$"Part 1 result: {lowestLocation}");