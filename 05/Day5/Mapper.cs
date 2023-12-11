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

    public MapRange() { }

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

    public bool IntersectsDestination(long item)
    {
        return item >= Destination && item <= Destination + Range;
    }

    public long ToSource(long destinationIndex)
    {
        return Source + (destinationIndex - Destination);
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

        // switch (segment)
        // {
        //     case Segment.Soil:
        //         SeedToSoil.Add(item);
        //         break;
        //     case Segment.Fertilizer:
        //         SoilToFertilizer.Add(item);
        //         break;
        //     case Segment.Water:
        //         FertilizerToWater.Add(item);
        //         break;
        //     case Segment.Light:
        //         WaterToLight.Add(item);
        //         break;
        //     case Segment.Temperature:
        //         LightToTemperature.Add(item);
        //         break;
        //     case Segment.Humidity:
        //         TemperatureToHumidity.Add(item);
        //         break;
        //     case Segment.Location:
        //         HumitidyToLocation.Add(item);
        //         break;
        //     default:
        //         break;
        // }
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
    public long ToSoil(long seed)
    {
        var col = GetSegmentCollection(Segment.Soil);
        return FindIntersectionOrDefault(col, seed);
    }

    public long ToFertilizer(long soil)
    {
        var col = GetSegmentCollection(Segment.Fertilizer);
        return FindIntersectionOrDefault(col, soil);
    }

    public long ToWater(long fertilizer)
    {
        var col = GetSegmentCollection(Segment.Water);
        return FindIntersectionOrDefault(col, fertilizer);
    }

    public long ToLight(long water)
    {
        var col = GetSegmentCollection(Segment.Light);
        return FindIntersectionOrDefault(col, water);
    }

    public long ToTemperature(long light)
    {
        var col = GetSegmentCollection(Segment.Temperature);
        return FindIntersectionOrDefault(col, light);
    }

    public long ToHumidity(long temperature)
    {
        var col = GetSegmentCollection(Segment.Humidity);
        return FindIntersectionOrDefault(col, temperature);
    }

    public long ToLocation(long humidity)
    {
        var col = GetSegmentCollection(Segment.Location);
        return FindIntersectionOrDefault(col, humidity);
    }

    public long SeedToLocation(long seed)
    {
        //Verbose for debugging, should tidy this up with a fancy interface builder inheritence 
        var soil = ToSoil(seed);
        var fertilizer = ToFertilizer(soil);
        var water = ToWater(fertilizer);
        var light = ToLight(water);
        var temperature = ToTemperature(light);
        var humidity = ToHumidity(temperature);
        var location = ToLocation(humidity);
        return location;
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