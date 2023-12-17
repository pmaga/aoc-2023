var data = new List<char[]>();

foreach (var line in await File.ReadAllLinesAsync("./input.txt"))
{
    data.Add(line.ToCharArray());
}

List<string> history = new();

var n = 1000000000;
for (var i = 0; i < n; i++)
{
   OneCycle();

   var state = string.Join('\n', data.Select(x => new string(x)));

   var index = history.IndexOf(state);
   if (index >= 0)
   {
       var match = (n - index) % (history.Count - index) + index;
       data = history[match - 1].Split('\n').Select(x => x.ToCharArray()).ToList();
       break;
   }

   history.Add(state);
}

//Draw();

Console.WriteLine($"Points: {GetPoints()}");

void OneCycle()
{
    //Draw(); Console.WriteLine();
    
    RollNorth();
    //Draw(); Console.WriteLine();
    
    RollLeft();
    //Draw(); Console.WriteLine();
    
    RollSouth();
    //Draw(); Console.WriteLine();

    RollRight();
    //Draw(); Console.WriteLine();
}

bool RollNorth()
{
    var movedAny = false;
    for (var currentCharIndex = 0; currentCharIndex < data[0].Length; currentCharIndex++)
    {
        for (var currentRowIndex = 1; currentRowIndex < data.Count; currentRowIndex++)
        {
            for (var parentRowIndex = currentRowIndex - 1; parentRowIndex >= 0; parentRowIndex--)
            {
                var currentRow = data[currentRowIndex];
                var parentRow = data[parentRowIndex];

                if (parentRow[currentCharIndex] == '.' && currentRow[currentCharIndex] == 'O')
                {
                    parentRow[currentCharIndex] = 'O';
                    currentRow[currentCharIndex] = '.';
                    movedAny = true;
                    currentRowIndex--;
                }
                else
                {
                    parentRowIndex = 0; //end
                }
            }
        }
    }
    return movedAny;
}

bool RollSouth()
{
    var movedAny = false;
    for (var currentCharIndex = 0; currentCharIndex < data![0].Length; currentCharIndex++)
    {
        for (var currentRowIndex = data.Count - 2; currentRowIndex >= 0; currentRowIndex--)
        {
            for (var parentRowIndex = currentRowIndex + 1; parentRowIndex < data.Count; parentRowIndex++)
            {
                var currentRow = data[currentRowIndex];
                var parentRow = data[parentRowIndex];

                if (parentRow[currentCharIndex] == '.' && currentRow[currentCharIndex] == 'O')
                {
                    parentRow[currentCharIndex] = 'O';
                    currentRow[currentCharIndex] = '.';
                    movedAny = true;
                    currentRowIndex++;
                }
                else
                {
                    parentRowIndex = data.Count; //end
                }
            }
        }
    }
    return movedAny;
}

bool RollRight()
{
    var movedAny = false;
    for (var currentRowIndex = 0; currentRowIndex < data.Count; currentRowIndex++)
    {
        for (var currentCharIndex = 0; currentCharIndex < data[0].Length; currentCharIndex++)
        {
            for (var leftCharIndex = currentCharIndex - 1; leftCharIndex >= 0; leftCharIndex--)
            {
                var currentRow = data[currentRowIndex];

                if (currentRow[currentCharIndex] == '.' && currentRow[leftCharIndex] == 'O')
                {
                    currentRow[currentCharIndex] = 'O';
                    currentRow[leftCharIndex] = '.';
                    movedAny = true;
                    currentCharIndex--;
                }
                else
                {
                    leftCharIndex = 0; //end
                }
            }
        }
    }
    return movedAny;
}

bool RollLeft()
{
    var movedAny = false;
    for (var currentRowIndex = 0; currentRowIndex < data.Count; currentRowIndex++)
    {
        for (var currentCharIndex = 0; currentCharIndex < data[0].Length - 1; currentCharIndex++)
        {
            var currentRow = data[currentRowIndex];

            if (currentRow[currentCharIndex] == '.' && currentRow[currentCharIndex + 1] == 'O')
            {
                currentRow[currentCharIndex] = 'O';
                currentRow[currentCharIndex + 1] = '.';
                movedAny = true;
                
                currentCharIndex = -1;
            }
        }
    }

    return movedAny;
}

void Draw()
{
    foreach (var d in data)
    {
        Console.WriteLine(string.Join(string.Empty, d));
    }
}

int GetPoints()
{
    var points = 0;
    for (var i = 0; i < data.Count; i++)
    {
        for (var j = 0; j < data[i].Length; j++)
        {
            if (data[i][j] == 'O')
            {
                points += data.Count - i;
            }
        }
    }

    return points;
}