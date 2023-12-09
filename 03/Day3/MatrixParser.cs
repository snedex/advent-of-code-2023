using System.Text.RegularExpressions;

public partial class MatrixParser
{
    public long ParseInputAndSumParts(string fileName)
    {
        var partNumbers = new List<long>();

        int lineNo = 0;
        var searchSize = 1;
        var symbolRegex = SymbolNoNumbersNoDots();
        var lineBuffer = File.ReadAllLines(fileName);

        foreach (var line in lineBuffer)
        {
            var symbolIndices = symbolRegex.Matches(line).Select(m => m.Index);
            if (!symbolIndices.Any())
            {
                lineNo++;
                continue;
            }

            var prevLine = lineNo == 0 ? string.Empty : lineBuffer[lineNo - searchSize];
            var nextLine = lineNo + searchSize >= lineBuffer.Length ? string.Empty : lineBuffer[lineNo + searchSize];

            var parts = ExtractIntersectingPartNumbers(new string[] { prevLine, line, nextLine }, symbolIndices, searchSize);
            partNumbers.AddRange(parts);

            lineNo++;
        }

        return partNumbers.Sum();
    }

    public long FindGearsExtractRatio(string fileName, string gearChar, int numIntersections)
    {
        var lineBuffer = File.ReadAllLines(fileName);
        
        var gearRegex = new Regex(Regex.Escape(gearChar));
        var lineNo = 0;
        var ratios = new List<long>();
        var searchSize = 1;

        foreach (var line in lineBuffer)
        {
            var symbolIndices = gearRegex.Matches(line).Select(m => m.Index);
            if (!symbolIndices.Any())
            {
                lineNo++;
                continue;
            }

            var prevLine = lineNo == 0 ? string.Empty : lineBuffer[lineNo - searchSize];
            var nextLine = lineNo + searchSize >= lineBuffer.Length ? string.Empty : lineBuffer[lineNo + searchSize];

            var parts = ExtractIntersectingPartNumbers(new string[] { prevLine, line, nextLine }, symbolIndices, searchSize);
            if(parts.Count() == numIntersections)
            {
                var ratio = parts.Aggregate(1L, (x,y) => x * y);
                ratios.Add(ratio);
            }

            lineNo++;
        }

        return ratios.Sum();
    }

    public IEnumerable<long> ExtractIntersectingPartNumbers(string[] lines, IEnumerable<int> symbolIndices, int searchRadius)
    {
        var partNumbers = new List<long>();

        foreach (var line in lines)
        {
            foreach (var match in NumbersGreedy().Matches(line).Cast<Match>())
            {
                foreach (int symbolIndex in symbolIndices)
                {
                    //start1 <= end2 && end1 >= start2
                    var searchStart = symbolIndex - 1;
                    var SearchEnd = symbolIndex + 1;
                    var numberStart = match.Index;
                    var numberEnd = match.Index + match.Length - 1;

                    if (searchStart <= numberEnd && SearchEnd >= numberStart)
                    {
                        partNumbers.Add(long.Parse(match.Value));
                    }
                }
            }
        }

        return partNumbers;
    }

    [GeneratedRegex("([^0-9\\.])")]
    public static partial Regex SymbolNoNumbersNoDots();

    [GeneratedRegex("\\d+")]
    private static partial Regex NumbersGreedy();
}

