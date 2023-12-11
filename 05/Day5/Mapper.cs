public enum Segment
{
    None,
    Seeds,
    Soil,
    Fertilizer,
    Water,
    Light,
    Temperature,
    Humidity,
    Location
}

public class MapRange
{
    public long Destination { get; set; }
    public long Source { get; set; }
    public long Range { get; set; }

    public MapRange(long destination, long source, long range)
    {
        Destination = destination;
        Source = source;
        Range = range;
    }

    public bool IntersectsSource(long item)
    {
        return item >= Source && item <= Source + Range;
    }

    public long ToDestination(long sourceIndex)
    {
        return Destination + (sourceIndex - Source);
    }
}

public class Mapper
{
    public HashSet<long> Seeds { get; init; } = new HashSet<long>();
    public IList<MapRange> SeedToSoil { get; private set; } = new List<MapRange>();
    public IList<MapRange> SoilToFertilizer { get; private set; } = new List<MapRange>();
    public IList<MapRange> FertilizerToWater { get; private set; } = new List<MapRange>();
    public IList<MapRange> WaterToLight { get; private set; } = new List<MapRange>();
    public IList<MapRange> LightToTemperature { get; private set; } = new List<MapRange>();
    public IList<MapRange> TemperatureToHumidity { get; private set; } = new List<MapRange>();
    public IList<MapRange> HumitidyToLocation { get; private set; } = new List<MapRange>();

    private const string HeaderMarker = ":";

    public Mapper(string filename)
    {
        ParseAlmanac(filename);
    }

    protected void ParseAlmanac(string filename)
    {
        var segment = Segment.None;
        foreach (var fileLine in File.ReadLines(filename))
        {
            var line = fileLine;
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            //Where are we? ... I don't know anymore
            if (line.Contains(HeaderMarker))
            {
                segment = GetNextSegment(segment);

                //Seeds are the special case as it's inline with the header
                if (segment != Segment.Seeds)
                {
                    continue;
                }
                line = line.Split(HeaderMarker)[1];
            }

            ProcessLine(segment, line);
        }
    }

    private void ProcessLine(Segment segment, string line)
    {
        if (segment == Segment.None)
        {
            return;
        }

        //Seeds are a special case
        var columns = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        if (segment == Segment.Seeds)
        {
            foreach (var col in columns)
            {
                Seeds.Add(long.Parse(col));
            }
            return;
        }

        //generate the map: dest, source, range
        var destination = long.Parse(columns[0]);
        var source = long.Parse(columns[1]);
        var range = long.Parse(columns[2]);
        var item = new MapRange(destination, source, range);

        GetSegmentCollection(segment).Add(item);
    }

    private static Segment GetNextSegment(Segment segment) => segment switch
    {
        Segment.None => Segment.Seeds,
        Segment.Seeds => Segment.Soil,
        Segment.Soil => Segment.Fertilizer,
        Segment.Fertilizer => Segment.Water,
        Segment.Water => Segment.Light,
        Segment.Light => Segment.Temperature,
        Segment.Temperature => Segment.Humidity,
        Segment.Humidity => Segment.Location,
        _ => Segment.None
    };

    private IList<MapRange> GetSegmentCollection(Segment segment)
    {
        switch (segment)
        {
            case Segment.Soil:
                return SeedToSoil;
            case Segment.Fertilizer:
                return SoilToFertilizer;
            case Segment.Water:
                return FertilizerToWater;
            case Segment.Light:
                return WaterToLight;
            case Segment.Temperature:
                return LightToTemperature;
            case Segment.Humidity:
                return TemperatureToHumidity;
            case Segment.Location:
                return HumitidyToLocation;
            default:
                return new List<MapRange>();
        }
    }

    public long Convert(long value, Segment source, Segment destination)
    {
        var currentSegment = source;
        var segmentValue = value;
        while(currentSegment != destination)
        {
            currentSegment = GetNextSegment(currentSegment);

            if(currentSegment == Segment.None)
            {
                break;
            }

            var col = GetSegmentCollection(currentSegment);
            segmentValue = FindIntersectionOrDefault(col, segmentValue); 
        }

        return segmentValue;
    }

    private long FindIntersectionOrDefault(IList<MapRange> mapRanges, long sourceValue)
    {
         foreach(var map in mapRanges)
        {
            if(map.IntersectsSource(sourceValue))
            {
                return map.ToDestination(sourceValue);
            }
        }

        return sourceValue;
    }
}