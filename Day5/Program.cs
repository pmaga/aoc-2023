using System.Numerics;

var points = 0;

// Part 1
var lines = await File.ReadAllLinesAsync("./input.txt");

var seedToSoilMap = new List<RangeMap>();
var soilToFertilizerMap = new List<RangeMap>();
var fertilizerToWaterMap = new List<RangeMap>();
var waterToLightMap = new List<RangeMap>();
var lightToTemperatureMap = new List<RangeMap>();
var temperatureToHumidityMap = new List<RangeMap>();
var humidityToLocationMap = new List<RangeMap>();

var seeds = lines[0][7..].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();

for (var index = 1; index < lines.Length; index++)
{
    var line = lines[index];
    switch (line)
    {
        case "seed-to-soil map:":
            seedToSoilMap = GetAllValues();
            continue;
        case "soil-to-fertilizer map:":
            soilToFertilizerMap = GetAllValues();
            continue;
        case "fertilizer-to-water map:":
            fertilizerToWaterMap = GetAllValues();
            continue;
        case "water-to-light map:":
            waterToLightMap = GetAllValues();
            continue;
        case "light-to-temperature map:":
            lightToTemperatureMap = GetAllValues();
            continue;
        case "temperature-to-humidity map:":
            temperatureToHumidityMap = GetAllValues();
            continue;
        case "humidity-to-location map:":
            humidityToLocationMap = GetAllValues();
            continue;
    }

    continue;

    List<RangeMap> GetAllValues()
    {
        index++;

        var arr = new List<RangeMap>();
        while (!string.IsNullOrWhiteSpace(line) && index < lines.Length)
        {
            line = lines[index];
            index++;
            
            var s = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (s.Length == 3)
            {
                var range = new RangeMap(long.Parse(s[0]), long.Parse(s[1]), long.Parse(s[2]));
                arr.Add(range);
            }
            else
            {
                index--;
            }
        }

        return arr;
    }
}

// part 1
// var locations = new List<long>();
// for (var i = 0; i < seeds.Count; i += 2)
// {
//     var seed = seeds[i];
//     
//     var soilMatch = seedToSoilMap.SingleOrDefault(x => seed >= x.SourceStart && seed <= x.SourceStart + x.Range - 1);
//     var soil = soilMatch is null ? seed : seed + Diff(soilMatch.DestinationStart, soilMatch.SourceStart);
//
//     var fertilizerMatch = soilToFertilizerMap.SingleOrDefault(x => soil >= x.SourceStart && soil <= x.SourceStart + x.Range - 1);
//     var fertilizer = fertilizerMatch is null ? soil : soil + Diff(fertilizerMatch.DestinationStart, fertilizerMatch.SourceStart);
//
//     var waterMatch = fertilizerToWaterMap.SingleOrDefault(x => fertilizer >= x.SourceStart && fertilizer <= x.SourceStart + x.Range - 1);
//     var water = waterMatch is null ? fertilizer : fertilizer + Diff(waterMatch.DestinationStart, waterMatch.SourceStart);
//
//     var lightMatch = waterToLightMap.SingleOrDefault(x => water >= x.SourceStart && water <= x.SourceStart + x.Range - 1);
//     var light = lightMatch is null ? water : water + Diff(lightMatch.DestinationStart, lightMatch.SourceStart);
//
//     var temperatureMatch = lightToTemperatureMap.SingleOrDefault(x => light >= x.SourceStart && light <= x.SourceStart + x.Range - 1);
//     var temperature = temperatureMatch is null ? light : light + Diff(temperatureMatch.DestinationStart, temperatureMatch.SourceStart);
//
//     var humidityMatch = temperatureToHumidityMap.SingleOrDefault(x => temperature >= x.SourceStart && temperature <= x.SourceStart + x.Range - 1);
//     var humidity = humidityMatch is null ? temperature : temperature + Diff(humidityMatch.DestinationStart, humidityMatch.SourceStart);
//
//     var locationMatch = humidityToLocationMap.SingleOrDefault(x => humidity >= x.SourceStart && humidity <= x.SourceStart + x.Range - 1);
//     var location = locationMatch is null ? humidity : humidity + Diff(locationMatch.DestinationStart, locationMatch.SourceStart);
//     locations.Add(location);
//     
//     long Diff(long x, long y) => x - y;
// }

// part2
var locations = new List<long>();
for (var i = 0; i < seeds.Count; i += 2)
{
    for (var j = 0; j < seeds[i + 1]; j++)
    {
        var seed = seeds[i];
        
        var soilMatch = seedToSoilMap.SingleOrDefault(x => seed >= x.SourceStart && seed <= x.SourceStart + x.Range - 1);
        var soil = soilMatch is null ? seed : seed + Diff(soilMatch.DestinationStart, soilMatch.SourceStart);

        var fertilizerMatch = soilToFertilizerMap.SingleOrDefault(x => soil >= x.SourceStart && soil <= x.SourceStart + x.Range - 1);
        var fertilizer = fertilizerMatch is null ? soil : soil + Diff(fertilizerMatch.DestinationStart, fertilizerMatch.SourceStart);
    
        var waterMatch = fertilizerToWaterMap.SingleOrDefault(x => fertilizer >= x.SourceStart && fertilizer <= x.SourceStart + x.Range - 1);
        var water = waterMatch is null ? fertilizer : fertilizer + Diff(waterMatch.DestinationStart, waterMatch.SourceStart);
    
        var lightMatch = waterToLightMap.SingleOrDefault(x => water >= x.SourceStart && water <= x.SourceStart + x.Range - 1);
        var light = lightMatch is null ? water : water + Diff(lightMatch.DestinationStart, lightMatch.SourceStart);
    
        var temperatureMatch = lightToTemperatureMap.SingleOrDefault(x => light >= x.SourceStart && light <= x.SourceStart + x.Range - 1);
        var temperature = temperatureMatch is null ? light : light + Diff(temperatureMatch.DestinationStart, temperatureMatch.SourceStart);
    
        var humidityMatch = temperatureToHumidityMap.SingleOrDefault(x => temperature >= x.SourceStart && temperature <= x.SourceStart + x.Range - 1);
        var humidity = humidityMatch is null ? temperature : temperature + Diff(humidityMatch.DestinationStart, humidityMatch.SourceStart);
    
        var locationMatch = humidityToLocationMap.SingleOrDefault(x => humidity >= x.SourceStart && humidity <= x.SourceStart + x.Range - 1);
        var location = locationMatch is null ? humidity : humidity + Diff(locationMatch.DestinationStart, locationMatch.SourceStart);
        locations.Add(location);
    }
    
    long Diff(long x, long y) => x - y;
}


Console.WriteLine($"Min: {locations.Min()}");

public sealed record RangeMap(long DestinationStart, long SourceStart, long Range);