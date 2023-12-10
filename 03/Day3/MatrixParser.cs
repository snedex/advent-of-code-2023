using System.Security.Cryptography.X509Certificates;
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

            var linesToSearch = GetLinesToSearch(lineBuffer, lineNo, searchSize);
            var parts = ExtractPartNumbersForSymbols(linesToSearch, symbolIndices, searchSize);

            partNumbers.AddRange(parts);

            lineNo++;
        }

        return partNumbers.Sum();
    }

    public long FindGearsExtractRatio(string fileName, char gearChar, int searchSize = 1, int numIntersections = 2)
    {
        var lineBuffer = File.ReadAllLines(fileName);

        var ratios = new List<long>();
        var gearRegex = new Regex(Regex.Escape(gearChar.ToString()));

        var lineNo = 0;
        foreach (var line in lineBuffer)
        {
            var gearResults = gearRegex.Matches(line);
            if (!gearResults.Any())
            {
                lineNo++;
                continue;
            }

            var linesToSearch = GetLinesToSearch(lineBuffer, lineNo, searchSize);

            foreach (var gearMatch in gearResults.Cast<Match>())
            {
                var adjacentParts = ExtractPartNumbersForSymbol(linesToSearch, gearMatch.Index, searchSize);

                if (adjacentParts.Count() == numIntersections)
                {
                    var ratio = adjacentParts.Aggregate(1L, (start, next) => start * next);
                    ratios.Add(ratio);
                }
            }

            lineNo++;
        }

        return ratios.Sum();
    }

    public IEnumerable<string> GetLinesToSearch(string[] linesToSearch, int lineNo, int searchSize)
    {
        var lines = new List<string>();
        var start = lineNo - searchSize;
        var end = lineNo + searchSize;
        for(int lineSearch = start; lineSearch < end + searchSize; lineSearch++)
        {
            if(lineSearch < 0 || lineSearch > linesToSearch.Length - 1)
            {
                continue;
            }
            lines.Add(linesToSearch[lineSearch]);
        }
        return lines;
    }

    public IEnumerable<long> ExtractPartNumbersForSymbols(IEnumerable<string> linesToSearch, IEnumerable<int> symbolIndices, int searchSize)
    {
        var adjacentParts = new List<long>();
        foreach(var index in symbolIndices)
        {
            adjacentParts.AddRange(ExtractPartNumbersForSymbol(linesToSearch, index, searchSize));
        }
        return adjacentParts;
    }

    public IEnumerable<long> ExtractPartNumbersForSymbol(IEnumerable<string> linesToSearch, int symbolIndex, int searchSize)
    {
        var adjacentParts = new List<long>();
        foreach(var line in linesToSearch)
        {
            adjacentParts.AddRange(ExtractIntersectingPartNumbers(line, symbolIndex, searchSize));
        }
        return adjacentParts;
    }

    public IEnumerable<long> ExtractIntersectingPartNumbers(string line, int symbolIndex, int searchSize)
    {
        return NumbersGreedy()
            .Matches(line, 0)
            .Where(m => Intersects(m.Index, m.Length, symbolIndex, searchSize))
            .Select(m => long.Parse(m.Value));
    }

    private bool Intersects(int index, int length, int symbolIndex, int searchWidthAbsolute)
    {
        return symbolIndex - searchWidthAbsolute <= index + length - 1 && symbolIndex + searchWidthAbsolute >= index;
    }

    [GeneratedRegex("([^0-9\\.])")]
    public static partial Regex SymbolNoNumbersNoDots();

    [GeneratedRegex("\\d+")]
    private static partial Regex NumbersGreedy();

    [GeneratedRegex("\\d+", RegexOptions.RightToLeft)]
    private static partial Regex NumbersGreedyReverse();
}