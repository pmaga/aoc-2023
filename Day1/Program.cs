var sum = 0;
var textDigits = new[]
{
    "zero",
    "one",
    "two",
    "three",
    "four",
    "five",
    "six",
    "seven",
    "eight",
    "nine"
};

foreach (var line in await File.ReadAllLinesAsync("./input.txt"))
{
    var (first, last) = ((int?)null, (int?)null);
    for (var lineIndex = 0; lineIndex < line.Length; lineIndex++)
    {
        var c = line[lineIndex];
        if (char.IsDigit(c))
        {
            var v = (int) char.GetNumericValue(c);
            (first, last) = (first ?? v, v);
        }
        else
        {
            for (var i = 0; i < textDigits.Length; i++)
            {
                var textDigit = textDigits[i];
                if (lineIndex + textDigit.Length > line.Length)
                {
                    continue;
                }

                if (string.Equals(textDigit, line.Substring(lineIndex, textDigit.Length)))
                {
                    (first, last) = (first ?? i, i);
                }
            }
        }
    }

    sum += int.Parse($"{first}{last}");
}

Console.WriteLine(sum.ToString());