// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

Console.WriteLine("Day 1");
long part1Resultant = 0;
long part2Resultant = 0;

foreach (var line in File.ReadLines(@"calibrationvalues.txt"))
{
    part1Resultant += GetLinePart1Value(line);
    part2Resultant += GetPart2LineValue(line);
}

Console.WriteLine($"PART 1 Resultant is: {part1Resultant}");
Console.WriteLine($"PART 2 Resultant is: {part2Resultant}");

partial class Program
{
    [GeneratedRegex("[0-9]")]
    private static partial Regex ForwardNumberMatch();

    [GeneratedRegex("[0-9]", RegexOptions.RightToLeft)]
    private static partial Regex ReverseNumberMatch();

    [GeneratedRegex("[0-9]|one|two|three|four|five|six|seven|eight|nine")]
    private static partial Regex ForwardTextAndNumberMatch();

    [GeneratedRegex("[0-9]|one|two|three|four|five|six|seven|eight|nine", RegexOptions.RightToLeft)]
    private static partial Regex ReverseTextAndNumberMatch();

    private static readonly Dictionary<string,string> numberStrings = new()
    {
        { "one", "1" },
        { "two", "2" },
        { "three", "3" },
        { "four", "4" },
        { "five", "5" },
        { "six", "6" },
        { "seven", "7" },
        { "eight", "8" },
        { "nine", "9" }
    };

    private static int GetLinePart1Value(string line)
    {
        var first = ForwardNumberMatch().Match(line).Value;
        var last = ReverseNumberMatch().Match(line).Value;

        return int.Parse(string.Concat(first, last)); 
    }

    private static int GetPart2LineValue(string line)
    {
        var first = ForwardTextAndNumberMatch().Match(line).Value;
        var last = ReverseTextAndNumberMatch().Match(line).Value;

        if(numberStrings.ContainsKey(first))
        {
            first = numberStrings[first];
        }

        if(numberStrings.ContainsKey(last))
        {
            last = numberStrings[last];
        }

        return int.Parse(string.Concat(first, last)); 
    }
}
