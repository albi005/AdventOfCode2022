using System.Diagnostics;
using System.Drawing;

string[] lines = File.ReadAllLines("09.txt");

HashSet<Point> visited = new();
Point head = new();
Point tail = new();
foreach (string line in lines)
{
    string[] parts = line.Split(' ');

    for (int i = 0; i < int.Parse(parts[1]); i++)
    {
        Point offset = parts[0] switch
        {
            "U" => new(0, -1),
            "D" => new(0, 1),
            "L" => new(-1, 0),
            "R" => new(1, 0),
            _ => throw new UnreachableException()
        };
        head.Offset(offset);
        Point diff = head - (Size)tail;
        tail = diff switch
        {
            { Y: < -1 } => head with { Y = head.Y + 1 },
            { Y: > 1 } => head with { Y = head.Y - 1 },
            { X: < -1 } => head with { X = head.X + 1 },
            { X: > 1 } => head with { X = head.X - 1 },
            _ => tail
        };
        visited.Add(tail);
    }
}

visited.Count.Print();