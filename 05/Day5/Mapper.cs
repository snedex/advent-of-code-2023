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
    public HashSet<long> Seeds { get; init; } = new HashSet<long>();
    public IDictionary<long, long> SeedToSoil { get; private set; } = new Dictionary<long, long>();
    public IDictionary<long, long> SoilToFertilizer { get; private set; } = new Dictionary<long, long>();
    public IDictionary<long, long> FertilizerToWater { get; private set; } = new Dictionary<long, long>();
    public IDictionary<long, long> WaterToLight { get; private set; } = new Dictionary<long, long>();
    public IDictionary<long, long> LightToTemperature { get; private set; } = new Dictionary<long, long>();
    public IDictionary<long, long> TemperatureToHumidity { get; private set; } = new Dictionary<long, long>();
    public IDictionary<long, long> HumitidyToLocation { get; private set; } = new Dictionary<long, long>();

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

        //Seeds are a special case
        var columns = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        if(segment == Segment.Seeds)
        {
            foreach(var col in columns)
            {
                Seeds.Add(long.Parse(col));
            }
            return;
        }

        //generate the map: dest, source, range
        var destination = long.Parse(columns[0]);
        var source = long.Parse(columns[1]);
        var range = long.Parse(columns[2]);

        for(int i = 0; i < range; i++)
        {
            var key = source + i; 
            var value = destination + i;
            
            switch(segment)
            {
                case Segment.Soil:
                    SeedToSoil.Add(key, value);
                    break;
                case Segment.Fertilizer:
                    SoilToFertilizer.Add(key, value);
                    break;
                case Segment.Water:
                    FertilizerToWater.Add(key, value);
                    break;
                case Segment.Light:
                    WaterToLight.Add(key, value);
                    break;
                case Segment.Temperature:
                    LightToTemperature.Add(key, value);
                    break;
                case Segment.Humidity:
                    TemperatureToHumidity.Add(key, value);
                    break;
                 case Segment.Location:
                    HumitidyToLocation.Add(key, value);
                    break;
                default:
                    break;
            }
        }
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

    public long ToSoil(long seed)
    {
        if(!SeedToSoil.TryGetValue(seed, out var soil))
        {
            soil = seed;
        }
        return soil;
    }

    public long ToFertilizer(long soil)
    {
        if(!SoilToFertilizer.TryGetValue(soil, out var fertilizer))
        {
            fertilizer = soil;
        }
        return fertilizer;
    }

    public long ToWater(long fertilizer)
    {
        if(!FertilizerToWater.TryGetValue(fertilizer, out var water))
        {
            water = fertilizer;
        }
        return water;
    }

    public long ToLight(long water)
    {
        if(!WaterToLight.TryGetValue(water, out var light))
        {
            light = water;
        }
        return light;
    }

    public long ToTemperature(long light)
    {
        if(!LightToTemperature.TryGetValue(light, out var temp))
        {
            temp = light;
        }
        return temp;
    }

    public long ToHumidity(long temperature)
    {
        if(!TemperatureToHumidity.TryGetValue(temperature, out var humidity))
        {
            humidity = temperature;
        }
        return humidity;
    }

    public long ToLocation(long humidity)
    {
        if(!HumitidyToLocation.TryGetValue(humidity, out var location))
        {
            location = humidity;
        }
        return location;
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
}