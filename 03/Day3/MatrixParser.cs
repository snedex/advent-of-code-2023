using System.Text.RegularExpressions;

public partial class MatrixParser
{
    public IDictionary<int, IList<int>> SymbolCoords { get; private set; }

    public IList<int> PartNumbers { get; private set; }

    public MatrixParser()
    { 
        ResetCollections();
    }

    private void ResetCollections()
    {
        SymbolCoords = new Dictionary<int, IList<int>>();
        PartNumbers = new List<int>();
    }

    public void ParseInput(string fileName)
    {
        ResetCollections();

        SymbolCoords = new Dictionary<int, IList<int>>();

        int lineNo = 0;
        var symbolRegex = SymbolNoNumbersNoDots();
        var numericRegex = Numerics();
        var lineBuffer = File.ReadAllLines(fileName);

        foreach (var line in lineBuffer)
        {
            var symbolResults = symbolRegex.Match(line);
            while(symbolResults.Success)
            {
                if(!SymbolCoords.ContainsKey(lineNo))
                {
                    SymbolCoords.Add(lineNo, new List<int>());
                }

                SymbolCoords[lineNo].Add(symbolResults.Index);

                //Adacent coords are same/+1/-1 line number, same/+1/-1 index
                //if we are at the first line or the last line we cannot read behind or ahead
                //If the symbol is the first or last column, we can't check ahead or behind
                var prevLine = lineNo == 0 ? string.Empty : lineBuffer[lineNo - 1];
                var nextLine = lineNo == lineBuffer.Length - 1 ? string.Empty : lineBuffer[lineNo + 1];

                var startMatchIndex = symbolResults.Index == 0 ? symbolResults.Index : symbolResults.Index - 1;
                var searchLength = symbolResults.Index == line.Length - 1 ? 2 : 3;

                //Search in the grid for any number within the radius
                var readBehindMatches = numericRegex.Match(prevLine, startMatchIndex, searchLength);
                var readCurrentMatches = numericRegex.Match(line, startMatchIndex, searchLength);
                var readAheadMatches = numericRegex.Match(nextLine, startMatchIndex, searchLength);
                
                if(readBehindMatches.Success)
                {
                    //OK we have a match now we have to read ahead and behind of the match position to get the whole string
                    //This is likely horribly inefficient
                    
                }
                
                if(readCurrentMatches.Success)
                {

                }
                
                if(readAheadMatches.Success)
                {

                }


                symbolResults = symbolResults.NextMatch();
            }

            lineNo++;
        }

        //We have line and index of each symbol any number in 1 radius in all directions including diagonal must be captured as a whole

    }

    [GeneratedRegex("([^0-9\\.])")]
    private static partial Regex SymbolNoNumbersNoDots();

    [GeneratedRegex("([0-9])")]
    private static partial Regex Numerics();
}

