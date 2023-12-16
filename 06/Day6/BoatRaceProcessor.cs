
using System.Reflection;

public class BoatRaceProcessor
{

    protected const string TimeHeader = "Time:";
    protected const string DistanceHeader = "Distance:";

    public IDictionary<int, int> RaceRecords = new Dictionary<int, int>();

    public BoatRaceProcessor(string fileName)
    {
        ReadInput(fileName);
    }

    public int Race(int raceIndex, int windupMilliseconds)
    {
        var (raceTime, _) = RaceRecords.ElementAt(raceIndex);
        return RaceInternal(raceTime, windupMilliseconds);
    }

    private int RaceInternal(int raceTime, int windupMilliseconds)
    {
        if(windupMilliseconds == 0 || raceTime == windupMilliseconds)
        {
            return 0;
        }

        return windupMilliseconds * (raceTime - windupMilliseconds);
    }

    public int GetNumWinningScenarios(int raceIndex)
    {
        var (raceTime, distanceToBeat) = RaceRecords.ElementAt(raceIndex);

        for(int i = 1; i <= raceTime; i++)
        {
            int distance = RaceInternal(raceTime, i);
            if (distance > distanceToBeat)
            {
                return raceTime - (i * 2) + 1;
            }
        }

        return 0;
    }

    public int SumScenarios()
    {
        int result = 1;

        for(int i = 0; i < RaceRecords.Count; i++)
        {
            result *= GetNumWinningScenarios(i);
        }

        return result;
    }

    private void ReadInput(string fileName)
    {
        fileName = Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().Location).Directory!.FullName, fileName);
        var raceLines = File.ReadAllLines(fileName);

        ParseLines(raceLines);
    }

    protected virtual void ParseLines(string[] raceLines)
    {
        var times = ParseLineToEntries(raceLines[0], TimeHeader);
        var distances = ParseLineToEntries(raceLines[1], DistanceHeader);

        for(int i = 0; i < times.Length; i++)
        {
            RaceRecords.Add(times[i], distances[i]);
        }
    }

    private int[] ParseLineToEntries(string raceLine, string header)
    {
        return raceLine.Replace(header, string.Empty)
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();
    }
}