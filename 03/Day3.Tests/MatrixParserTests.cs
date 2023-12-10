using System.Text.RegularExpressions;

namespace Day3.Tests;

public class MatrixParserTest
{
    [Theory]
    [InlineData(new string[] { "467..114..", "...*......", "..35..633." }, new int[] { 3 }, new long[] { 467, 35 })]
    [InlineData(new string[] { "..35..633.", "......#...", "617*......" }, new int[] { 6 }, new long[] { 633 })]
    [InlineData(new string[] { "......#...", "617*......", ".....+.58." }, new int[] { 3 }, new long[] { 617 })]
    [InlineData(new string[] { "...184", "..*...", "...571" }, new int[] { 2 }, new long[] { 184, 571 })]
    [InlineData(new string[] {
    "..........*..../.......337..475*......&..391...347...795....*.........................*......722..666...............@.......................",
    "........823.$.....+.../.........716......*........*....*.....247......329...........697......*.....*....%.....168....624.........592........",
    "...98*.......916..915......245............277..&..353...719..........$.....................846.601.37...47......................*......519.."
    }, new int[] { 12 }, new long[] { 916 })]
    public void ExtractAllPartNumbersShouldPass(string[] partLines, int[] symbolIndices, long[] expectedParts)
    {
        var searchRadius = 1;
        var parser = new MatrixParser();

        var parts = parser.ExtractPartNumbersForSymbols(partLines, symbolIndices, searchRadius);

        Assert.Equal(expectedParts.Length, parts.Count());
        Assert.Equal(expectedParts, parts.ToArray());
    }

    [Theory]
    [InlineData("467..114..", 3, 1, new long[] { 467 })]
    [InlineData("...*......", 3, 1, new long[] { })]
    [InlineData("..35..633.", 3, 1, new long[] { 35 })]
    [InlineData("617*......", 3, 1, new long[] { 617 })]
    public void ExtractPartNumbersPerLineShouldPass(string lineData, int searchPos, int searchSize, long[] expectedResult)
    {
        var result = new MatrixParser().ExtractIntersectingPartNumbers(lineData, searchPos, searchSize);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(@"testinput.txt", 4361)]
    public void ProcessEntireFileSumOfPartsMatchShouldPass(string fileName, long expectedSum)
    {
        var parser = new MatrixParser();
        var sumOfParts = parser.ParseInputAndSumParts(fileName);

        Assert.Equal(expectedSum, sumOfParts);
    }

    [Theory]
    [InlineData("", 0)]
    [InlineData("...$.*....", 2)]
    [InlineData(".664.598..", 0)]
    [InlineData(".....+.58.", 1)]
    [InlineData(".........511..853......*..............*...........355.............35-.....-....@..636.....=...595..131...496..........................=687..", 7)]
    [InlineData("..........*..../.......337..475*......&..391...347...795....*.........................*......722..666...............@.......................", 7)]
    [InlineData("........823.$.....+.../.........716......*........*....*.....247......329...........697......*.....*....%.....168....624.........592........", 9)]
    [InlineData("...98*.......916..915......245............277..&..353...719..........$.....................846.601.37...47......................*......519..", 4)]
    public void NumSymbolMatchesPerLineShouldPass(string line, int expectedMatches)
    {
        var matches = MatrixParser.SymbolNoNumbersNoDots().Matches(line);

        Assert.Equal(expectedMatches, matches.Count);
    }

    [Theory]
    [InlineData(@"testinput.txt", 467835)]
    public void FindGearRatiosAndSumShouldPass(string filename, long expectedValue)
    {
        var sumOfRatios = new MatrixParser().FindGearsExtractRatio(filename, '*');
        
        Assert.Equal(expectedValue, sumOfRatios);
    }
}