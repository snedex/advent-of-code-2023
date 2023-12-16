
public class BoatRaceProcessorPt2 : BoatRaceProcessor
{
    public new IDictionary<long, long> RaceRecords = new Dictionary<long, long>();

    public BoatRaceProcessorPt2(string fileName) : base(fileName)
    {
    }

    public new long GetNumWinningScenarios(int raceIndex)
    {
        var (raceTime, distanceToBeat) = RaceRecords.ElementAt(raceIndex);

        for(long i = 1; i <= raceTime; i++)
        {
            long distance = RaceInternal(raceTime, i);
            if (distance > distanceToBeat)
            {
                return raceTime - (i * 2L) + 1L;
            }
        }

        return 0;
    }

    private long RaceInternal(long raceTime, long windupMilliseconds)
    {
        if(windupMilliseconds == 0 || raceTime == windupMilliseconds)
        {
            return 0;
        }

        return windupMilliseconds * (raceTime - windupMilliseconds);
    }

    protected override void ParseLines(string[] raceLines)
    {
       RaceRecords.Add(ParseLineToEntry(raceLines[0], TimeHeader), ParseLineToEntry(raceLines[1], DistanceHeader));
    }

    private long ParseLineToEntry(string raceLine, string header)
    {
        return long.Parse(raceLine.Replace(header, string.Empty)
            .Replace(" ", string.Empty));
    }
}