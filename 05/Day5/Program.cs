// See https://aka.ms/new-console-template for more information
Console.WriteLine("Day 5!");

var mapper = new Mapper(@"input.txt");
var lowestLocation = mapper.FindLowestLocationSeeds();
Console.WriteLine(@$"Part 1 result: {lowestLocation}");

var lowestLocationPt2 = mapper.FindLowestLocationWithSeedRanges();
Console.WriteLine(@$"Part 2 result: {lowestLocationPt2}");