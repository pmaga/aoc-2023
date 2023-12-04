var data = new List<char[]>();

foreach (var line in await File.ReadAllLinesAsync("./input.txt"))
{
    var d = new char[line.Length];
    for (var index = 0; index < line.Length; index++)
    {
        d[index] = line[index];
    }
    data.Add(d);
}

var allNumbers = new List<(int Number, int LineIndex, int LineStartIndex, int LineEndIndex)>();

for (var lineIndex = 0; lineIndex < data.Count; lineIndex++)
{
    var line = data[lineIndex];
    
    for (var dataIndex = 0; dataIndex < line.Length; dataIndex++)
    {
        var character = line[dataIndex];
        
        if (char.IsDigit(character))
        {
            var number = character.ToString();
            
            var startIndex = dataIndex;
            var endIndex = startIndex;

            if (startIndex < line.Length)
            {
                for (var scopeIndex = startIndex + 1; scopeIndex < line.Length; scopeIndex++)
                {
                    character = line[scopeIndex];
                    endIndex = scopeIndex - 1;
                    if (char.IsDigit(character))
                    {
                        number += character.ToString();
                    }
                    else
                    {
                        break;
                    }
                }   
            }
            
            allNumbers.Add((int.Parse(number), lineIndex, startIndex, endIndex));

            dataIndex = endIndex + 1;
        }
    }
}

var sum = 0;

var starsInfo = new List<(int LineIndex, int SymbolIndex, int Number)>(); // part2

foreach (var numberInfo in allNumbers)
{
    var leftAdjacent = numberInfo.LineStartIndex > 0 && IsSymbol(data[numberInfo.LineIndex][numberInfo.LineStartIndex - 1]);
    var rightAdjacent = numberInfo.LineEndIndex < data[numberInfo.LineIndex].Length - 1 && IsSymbol(data[numberInfo.LineIndex][numberInfo.LineEndIndex + 1]);

    var leftTopDiagonallyAdjacent = numberInfo is {LineStartIndex: > 0, LineIndex: > 0} && IsSymbol(data[numberInfo.LineIndex - 1][numberInfo.LineStartIndex - 1]);
    var rightTopDiagonallyAdjacent = numberInfo.LineEndIndex < data[numberInfo.LineIndex].Length - 1 && numberInfo.LineIndex > 0 &&
                                     IsSymbol(data[numberInfo.LineIndex - 1][numberInfo.LineEndIndex + 1]);

    var topAdjacent = false;
    var topAdjacentSymbolIndex = 0;
    if (numberInfo.LineIndex > 0)
    {
        for (var i = numberInfo.LineStartIndex; i <= numberInfo.LineEndIndex; i++)
        {
            if (IsSymbol(data[numberInfo.LineIndex - 1][i]))
            {
                topAdjacent = true;
                topAdjacentSymbolIndex = i;
            }
        }
    }

    var bottomAdjacent = false;
    var bottomAdjacentSymbolIndex = 0;
    if (numberInfo.LineIndex < data.Count - 1)
    {
        for (var i = numberInfo.LineStartIndex; i <= numberInfo.LineEndIndex; i++)
        {
            if (IsSymbol(data[numberInfo.LineIndex + 1][i]))
            {
                bottomAdjacent = true;
                bottomAdjacentSymbolIndex = i;
            }
        }
    }

    var leftBottomDiagonallyAdjacent = numberInfo.LineStartIndex > 0 && numberInfo.LineIndex < data.Count - 1 &&
                                       IsSymbol(data[numberInfo.LineIndex + 1][numberInfo.LineStartIndex - 1]);
    var rightBottomDiagonallyAdjacent = numberInfo.LineEndIndex < data[numberInfo.LineIndex].Length - 1 && numberInfo.LineIndex < data.Count - 1 &&
                                        IsSymbol(data[numberInfo.LineIndex + 1][numberInfo.LineEndIndex + 1]);

    if (leftAdjacent
        || rightAdjacent
        || topAdjacent
        || bottomAdjacent
        || leftTopDiagonallyAdjacent
        || rightTopDiagonallyAdjacent
        || leftBottomDiagonallyAdjacent
        || rightBottomDiagonallyAdjacent
       )
    {
        sum += numberInfo.Number;
    }


    // part2
    if (leftAdjacent && data[numberInfo.LineIndex][numberInfo.LineStartIndex - 1] == '*')
    {
        starsInfo.Add((numberInfo.LineIndex, numberInfo.LineStartIndex - 1, numberInfo.Number));
    }

    if (rightAdjacent && data[numberInfo.LineIndex][numberInfo.LineEndIndex + 1] == '*')
    {
        starsInfo.Add((numberInfo.LineIndex, numberInfo.LineEndIndex + 1, numberInfo.Number));
    }

    if (topAdjacent && data[numberInfo.LineIndex - 1][topAdjacentSymbolIndex] == '*')
    {
        starsInfo.Add((numberInfo.LineIndex - 1, topAdjacentSymbolIndex, numberInfo.Number));
    }

    if (bottomAdjacent && data[numberInfo.LineIndex + 1][bottomAdjacentSymbolIndex] == '*')
    {
        starsInfo.Add((numberInfo.LineIndex + 1, bottomAdjacentSymbolIndex, numberInfo.Number));
    }

    if (leftTopDiagonallyAdjacent && data[numberInfo.LineIndex - 1][numberInfo.LineStartIndex - 1] == '*')
    {
        starsInfo.Add((numberInfo.LineIndex - 1, numberInfo.LineStartIndex - 1, numberInfo.Number));
    }

    if (rightTopDiagonallyAdjacent && data[numberInfo.LineIndex - 1][numberInfo.LineEndIndex + 1] == '*')
    {
        starsInfo.Add((numberInfo.LineIndex - 1, numberInfo.LineEndIndex + 1, numberInfo.Number));
    }

    if (leftBottomDiagonallyAdjacent && data[numberInfo.LineIndex + 1][numberInfo.LineStartIndex - 1] == '*')
    {
        starsInfo.Add((numberInfo.LineIndex + 1, numberInfo.LineStartIndex - 1, numberInfo.Number));
    }

    if (rightBottomDiagonallyAdjacent && data[numberInfo.LineIndex + 1][numberInfo.LineEndIndex + 1] == '*')
    {
        starsInfo.Add((numberInfo.LineIndex + 1, numberInfo.LineEndIndex + 1, numberInfo.Number));
    }
}

// part2
var gearsSum = starsInfo.GroupBy(x => $"{x.LineIndex}{x.SymbolIndex}", y => y.Number, (x, y) => y)
    .Where(x => x.Count() == 2)
    .Select(x => x.First() * x.Last() )
    .Sum();

static bool IsSymbol(char c) => c != '.' && !char.IsLetterOrDigit(c);


Console.WriteLine(sum);
Console.WriteLine(gearsSum);
