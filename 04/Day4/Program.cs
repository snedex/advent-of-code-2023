// See https://aka.ms/new-console-template for more information
Console.WriteLine("Day 4");

var processor = new ScratchCardProcessor(@"input.txt");
var part1Resultant = processor.GetTotalScore();
var part2Resultant = processor.ScratchCardMulitplierResult();

Console.WriteLine($"Part 1 result: {part1Resultant}");
Console.WriteLine($"Part 2 result: {part2Resultant}");