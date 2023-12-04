var points = 0;

// Part 1
foreach (var line in await File.ReadAllLinesAsync("./input.txt"))
{
    var s1 = line.Split(':');
    var results = s1[1].Split('|');

    var winningNumbers = results[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
    var myNumbers = results[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);

    var matches = myNumbers.Count(winningNumbers.Contains);
    if (matches == 1)
    {
        points += 1;
    }
    else if (matches > 1)
    {
        points += (int)Math.Pow(2, matches - 1);
    }
}
Console.WriteLine(points.ToString());

// Part 2
var allLines = await File.ReadAllLinesAsync("./input.txt");
var data = allLines.Select(x =>
{
    var s1 = x.Split(':');
    var results = s1[1].Split('|');

    var winningNumbers = results[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
    var myNumbers = results[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
    return (winningNumbers, myNumbers);
}).ToArray();

var total = 0;
for (var i = 0; i < data.Length; i++)
{
    AnalyzeScratchcard(i);
}

Console.WriteLine(total.ToString());
return;

void AnalyzeScratchcard(int currentIndex)
{
    total++;
    var currentScratchCard = data[currentIndex];
    var matches = currentScratchCard.myNumbers.Count(currentScratchCard.winningNumbers.Contains);
    for (var i = 1; i <= matches; i++)
    {
        AnalyzeScratchcard(currentIndex + i);
    }
}