// See https://aka.ms/new-console-template for more information
Console.WriteLine("Day 4");

var processor = new ScratchCardProcessor(@"Day4/input.txt");
var part1Resultant = processor.GetTotalScore();

Console.WriteLine($"Part 1 result: {part1Resultant}");