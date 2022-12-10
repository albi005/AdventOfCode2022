using System.Diagnostics;
using System.Drawing;

string[] lines = File.ReadAllLines("09.txt");

HashSet<Point> visited = new();
Point[] knots = new Point[10];

void Print()
{
    if (!Debugger.IsAttached) return;

    int minX = knots.Append(new()).Min(x => x.X);
    int minY = knots.Append(new()).Min(x => x.Y);
    int maxX = knots.Append(new()).Max(x => x.X);
    int maxY = knots.Append(new()).Max(x => x.Y);

    char[][] grid = Enumerable
        .Range(0, maxY - minY + 1)
        .Select(_ => Enumerable
            .Range(0, maxX - minX + 1)
            .Select(_ => '.')
            .ToArray())
        .ToArray();
    
    grid[-minY][-minX] = 's';
    for (int index = 1; index < knots.Length - 1; index++)
    {
        Point knot = knots[index];
        grid[knot.Y - minY][knot.X - minX] = index.ToString()[0];
    }
    grid[knots[0].Y - minY][knots[0].X - minX] = 'H';

    Console.WriteLine();
    foreach (char[] row in grid)
    {
        Console.WriteLine(string.Join("", row));
    }
}

foreach (string line in lines)
{
    string[] parts = line.Split(' ');

    Point offset = parts[0] switch
    {
        "U" => new(0, -1),
        "D" => new(0, 1),
        "L" => new(-1, 0),
        "R" => new(1, 0),
        _ => throw new UnreachableException()
    };
    for (int i = 0; i < int.Parse(parts[1]); i++)
    {
        knots[0].Offset(offset);
        for (int j = 1; j < knots.Length; j++)
        {
            Point current = knots[j];
            Point prev = knots[j - 1];
            int xd = prev.X - current.X;
            int yd = prev.Y - current.Y;
            if (Math.Abs(xd) > 1 || Math.Abs(yd) > 1)
                knots[j] = new(prev.X - xd / 2, prev.Y - yd / 2);
            
            // Original solution:
            // current = diff switch
            // {
            //     ({ Y: -2 } or { Y: 2 }) and ({ X: -2 } or { X: 2 }) => new(prev.X - diff.X / 2, prev.Y - diff.Y / 2),
            //     { Y: -2 } => prev with { Y = prev.Y + 1 },
            //     { Y: 2 } => prev with { Y = prev.Y - 1 },
            //     { X: -2 } => prev with { X = prev.X + 1 },
            //     { X: 2 } => prev with { X = prev.X - 1 },
            //     _ => current
            // };
        }

        visited.Add(knots[^1]);
    }

    Print();
}

visited.Count.Print();