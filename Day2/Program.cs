var gamesResults = new Dictionary<int, List<(int Green, int Red, int Blue)>>();

foreach (var line in await File.ReadAllLinesAsync("./input.txt"))
{
    var s1 = line.Split(':');
    var gameId = int.Parse(s1[0].Substring(5, s1[0].Length - 5));
    
    var gameResults = new List<(int Green, int Red, int Blue)>();
    gamesResults.Add(gameId, gameResults);

    var cubesLine = s1[1];
    var sets = cubesLine.Split(';', StringSplitOptions.RemoveEmptyEntries);
    foreach (var set in sets)
    {
        var gameResult = (Green: 0, Red: 0, Blue: 0);
        var cubes = set.Split(',', StringSplitOptions.RemoveEmptyEntries);
        foreach (var cubeData in cubes)
        {
            if (cubeData.EndsWith("green"))
            {
                gameResult.Green = int.Parse(cubeData.Replace(" green", string.Empty));
            }
            else if (cubeData.EndsWith("red"))
            {
                gameResult.Red = int.Parse(cubeData.Replace(" red", string.Empty));
            }
            else if (cubeData.EndsWith("blue"))
            {
                gameResult.Blue = int.Parse(cubeData.Replace(" blue", string.Empty));
            }
        }
        
        gameResults.Add(gameResult);
    }
}

//var gameIds = 0;
//  only 12 red cubes, 13 green cubes, and 14 blue cubes
// foreach (var gamesResult in gamesResults)
// {
//     var isPossible = gamesResult.Value.All(x => x.Red <= 12)
//         && gamesResult.Value.All(x => x.Green <= 13)
//         && gamesResult.Value.All(x => x.Blue <= 14);
//     if (isPossible)
//     {
//         gameIds += gamesResult.Key;
//     }
// }

var sum = 0;
foreach (var gamesResult in gamesResults)
{   
    var minRed = gamesResult.Value.Max(x => x.Red);
    var minBlue = gamesResult.Value.Max(x => x.Blue);
    var minGreen = gamesResult.Value.Max(x => x.Green);
    sum += minRed * minBlue * minGreen;
}

Console.WriteLine(sum.ToString());