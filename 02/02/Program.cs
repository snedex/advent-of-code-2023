// See https://aka.ms/new-console-template for more information
using System.Data;

Console.WriteLine("Day 02");

int part1Resultant = 0;
long part2Resultant = 0L;
var gameValidator = new GameValidator(12, 13, 14);

foreach (var line in File.ReadLines(@"gameinput.txt"))
{
    var rounds = new GameParser().ParseGameLine(line);
    if(gameValidator.IsGameValid(rounds))
    {
        part1Resultant += rounds.First().GameId;
    }

    //The min cubes needed to make the game valid
    (int red, int green, int blue) = gameValidator.MinCubesToPlay(rounds);
    part2Resultant += red * green * blue;
}

Console.WriteLine($"Part 1 Resultant: {part1Resultant}");
Console.WriteLine($"Part 2 Resultant: {part2Resultant}");