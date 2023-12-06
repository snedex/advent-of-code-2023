// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

Console.WriteLine("Day 1");
long part1Resultant = 0;

foreach (var line in File.ReadLines(@"calibrationvalues.txt"))
{
    var first = ForwardMatch().Match(line);
    var last = ReverseMatch().Match(line);

    part1Resultant += int.Parse(string.Concat(first.Value, last.Value)); 
}

Console.WriteLine($"Resultant is: {part1Resultant}");

partial class Program
{
    [GeneratedRegex("[0-9]")]
    private static partial Regex ForwardMatch();

    [GeneratedRegex("[0-9]", RegexOptions.RightToLeft)]
    private static partial Regex ReverseMatch();
}