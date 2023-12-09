// See https://aka.ms/new-console-template for more information
Console.WriteLine("Day 3");


var part1Result = new MatrixParser().ParseInputAndSumParts(@"machineinput.txt");
var part2Result = new MatrixParser().FindGearsExtractRatio(@"machineinput.txt", "*", 2);

Console.WriteLine($"Part 1: result is {part1Result}");
Console.WriteLine($"Part 1: result is {part2Result}");