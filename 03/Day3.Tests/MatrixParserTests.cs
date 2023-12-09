namespace Day3.Tests;

public class MatrixParserTest
{
    [Theory]
    [InlineData("467..114..", 2, "7", "467")]
    [InlineData("...*......", -1, null, null)]
    [InlineData("..35..633.", 3, "5", "35")]
    [InlineData("..35..633.", 6, "6", "633")]
    [InlineData("617*......", 2, "7", "617")]
    [InlineData("..592.....", 4, "2", "592")]
    [InlineData("......755.", 6, "7", "755")]
    [InlineData(".664.598..", 3, "4", "664")]
    [InlineData(".664.598..", 5, "5", "598")]
    public void ExtractAdjacentPartNumberShouldPass(string partNumberLine, int position, string initialMatch, string expectedResult)
    {
        var parser = new MatrixParser();
        var partNumber = parser.ExtractAdjacentPartNumber(partNumberLine, position, initialMatch);

        Assert.Equal(expectedResult, partNumber);
    }

    [Theory]
    [InlineData(new string[] { "467..114..", "...*......", "..35..633." }, 3, new long[] { 467, 35 })]
    [InlineData(new string[] { "..35..633.", "......#...", "617*......" }, 6, new long[] { 633 })]
    [InlineData(new string[] { "......#...", "617*......", ".....+.58." }, 3, new long[] { 617 })]
    public void ExtractAllPartNumbersShouldPass(string[] partLines, int symbolMatchIndex, long[] expectedParts)
    {
        var parser = new MatrixParser();
        var parts = parser.FindPartNumbers(partLines, symbolMatchIndex);

        Assert.Equal(expectedParts.Length, parts.Count());
        Assert.Equal(expectedParts, parts.ToArray());
    }

    [Theory]
    [InlineData( @"testinput.txt", 4361)]
    public void ProcessEntireFileSumOfPartsMatchShouldPass(string fileName, long expectedSum)
    {
        var parser = new MatrixParser();
        var sumOfParts = parser.ParseInputAndSumParts(fileName);

        Assert.Equal(expectedSum, sumOfParts);
    }
}