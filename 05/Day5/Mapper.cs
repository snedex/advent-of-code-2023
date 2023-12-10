


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

public class Mapper 
{
    public HashSet<int> Seeds { get; init; } = new HashSet<int>();
    private IDictionary<int, int> SeedToSoil { get; init; } = new Dictionary<int, int>();
    private IDictionary<int, int> SoilToFertilizer { get; init; } = new Dictionary<int, int>();
    private IDictionary<int, int> FertilizerToWater { get; init; } = new Dictionary<int, int>();
    private IDictionary<int, int> WaterToLight { get; init; } = new Dictionary<int, int>();
    private IDictionary<int, int> LightToTemperature { get; init; } = new Dictionary<int, int>();
    private IDictionary<int, int> TemperatureToHumidity { get; init; } = new Dictionary<int, int>();
    private IDictionary<int, int> HumitidyToLocation { get; init; } = new Dictionary<int, int>();

    private const string HeaderMarker = ":";

    public Mapper(string filename)
    {
        ParseAlmanac(filename);
    }

    protected void ParseAlmanac(string filename)
    {
        var segment = Segment.None;
        foreach(var fileLine in File.ReadLines(filename))
        {
            var line = fileLine;
            if(string.IsNullOrEmpty(line))
            {
                continue;
            }

            //Where are we? ... I don't know anymore
            if(line.Contains(HeaderMarker))
            {
                segment = GetNextSegment(segment);

                //Seeds are the special case as it's inline with the header
                if(segment != Segment.Seeds)
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
        if(segment == Segment.None)
        {
            return;
        }

        //Break up the numbers and generate the map

        throw new NotImplementedException();
    }

    private static Segment GetNextSegment(Segment segment)=> segment switch
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

    public int ToSoil(int seed)
    {
        if(!SeedToSoil.TryGetValue(seed, out var soil))
        {
            soil = seed;
        }
        return soil;
    }

    public int ToFertilizer(int soil)
    {
        if(!SoilToFertilizer.TryGetValue(soil, out var fertilizer))
        {
            fertilizer = soil;
        }
        return fertilizer;
    }

    public int ToWater(int fertilizer)
    {
        if(!FertilizerToWater.TryGetValue(fertilizer, out var water))
        {
            water = fertilizer;
        }
        return water;
    }

    public int ToLight(int water)
    {
        if(!WaterToLight.TryGetValue(water, out var light))
        {
            light = water;
        }
        return light;
    }

    public int ToTemperature(int light)
    {
        if(!LightToTemperature.TryGetValue(light, out var temp))
        {
            temp = light;
        }
        return temp;
    }

    public int ToHumidity(int temperature)
    {
        if(!TemperatureToHumidity.TryGetValue(temperature, out var humidity))
        {
            humidity = temperature;
        }
        return humidity;
    }

    public int ToLocation(int humidity)
    {
        if(!HumitidyToLocation.TryGetValue(humidity, out var location))
        {
            location = humidity;
        }
        return location;
    }
}