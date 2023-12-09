using System.Text.RegularExpressions;

public partial class MatrixParser
{    
    public long ParseInputAndSumParts(string fileName)
    {
        IEnumerable<long> PartNumbers = new List<long>();

        int lineNo = 0;
        var symbolRegex = SymbolNoNumbersNoDots();
        var lineBuffer = File.ReadAllLines(fileName);

        try
        {
            foreach (var line in lineBuffer)
            {
                var symbolResults = symbolRegex.Match(line);
                while(symbolResults.Success)
                {
                    //We have line and index of each symbol any number in 1 radius in all directions including diagonal must be captured as a whole
                    //Adacent coords are same/+1/-1 line number, same/+1/-1 index
                    //if we are at the first line or the last line we cannot read behind or ahead
                    //If the symbol is the first or last column, we can't check ahead or behind
                    var prevLine = lineNo == 0 ? string.Empty : lineBuffer[lineNo - 1];
                    var nextLine = lineNo == lineBuffer.Length - 1 ? string.Empty : lineBuffer[lineNo + 1];

                    var parts = FindPartNumbers(new string[] { prevLine, line, nextLine}, symbolResults.Index);
                    PartNumbers = PartNumbers.Union(parts);

                    symbolResults = symbolResults.NextMatch();
                }

                lineNo++;
            }
        } 
        catch(Exception ex)
        {
            Console.WriteLine($"Error parsing line {lineNo}");
            throw ex;
        }
     
        return PartNumbers.Sum();
    }

    public IEnumerable<long> FindPartNumbers(string[] linesToSearch, int symbolIndex, int radius = 1)
    {
        //TODO: use the radius other than as a magic number
        var parts = new List<long>();

        var startMatchIndex = symbolIndex == 0 ? symbolIndex : symbolIndex - radius;
        var searchLength = radius * 3 + (symbolIndex == linesToSearch.First().Length - 1 ? - 1 : 0);
        
        foreach(var line in linesToSearch)
        {
            var match = NumericsForwards().Match(line, startMatchIndex, searchLength);
            if(match.Success)
            {
                var partNumber = ExtractAdjacentPartNumber(line, match.Index, match.Value);
                parts.Add(long.Parse(partNumber));
            }
        }

        return parts;
    }

    public string ExtractAdjacentPartNumber(string partNumberLine, int positionMatch, string initialAdjacentCharacter)
    {
        //OK we have a match now we have to read ahead and behind of the match position to get the whole string
        //This is likely horribly inefficient
        int startPos = positionMatch;
        var numberString = initialAdjacentCharacter;

        //read until we hit a non numeric
        for(int i = startPos; i > 0; i--)
        {
            var substr = partNumberLine.Substring(i - 1, 1);
            if(!NumericsForwards().Match(substr).Success)
            {
                break;
            }
            numberString = substr + numberString;
        }

        for(int i = startPos; i < partNumberLine.Length - 1; i++)
        {
            var substr = partNumberLine.Substring(i + 1, 1);
            if(!NumericsForwards().Match(substr).Success)
            {
                break;
            }
            numberString += substr;
        }

        return numberString;
    }

    [GeneratedRegex("([^0-9\\.])")]
    private static partial Regex SymbolNoNumbersNoDots();

    [GeneratedRegex("([0-9])")]
    private static partial Regex NumericsForwards();
}

