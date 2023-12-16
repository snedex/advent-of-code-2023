
var sum = new BoatRaceProcessor("input.txt").SumScenarios();

Console.WriteLine($"Part 1 Resultant: {sum}");

var pt2Wins = new BoatRaceProcessorPt2("input.txt").GetNumWinningScenarios(0);

Console.WriteLine($"Part 2 Resultant: {pt2Wins}");