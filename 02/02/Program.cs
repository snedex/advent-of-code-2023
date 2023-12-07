// See https://aka.ms/new-console-template for more information
Console.WriteLine("Day 02");

int part1Resultant = 0;
var gameValidator = new GameValidator(12, 13, 14);

foreach (var line in File.ReadLines(@"gameinput.txt"))
{
    var rounds = new GameParser().ParseGameLine(line);
    if(gameValidator.IsGameValid(rounds))
    {
        part1Resultant += rounds.First().GameId;
    }
}

Console.WriteLine($"Part 1 Resultant: {part1Resultant}");