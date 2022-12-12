using System.Diagnostics;
using System.Drawing;

Stopwatch stopwatch = Stopwatch.StartNew();

string[] heights = File.ReadAllLines("12.txt");

Point start = new();
Point end = new();

uint[][] distances = new uint[heights.Length][];
for (int i = 0; i < heights.Length; i++)
{
    distances[i] = new uint[heights[0].Length];
    for (int j = 0; j < heights[0].Length; j++)
    {
        distances[i][j] = uint.MaxValue;
        char height = heights[i][j];
        if (height == 'S')
            start = new(j, i);
        if (height == 'E')
            end = new(j, i);
    }
}

distances[start.Y][start.X] = 0;
Queue<Point> queue = new();
queue.Enqueue(start);

Point[] directions =
{
    new(0, -1),
    new(1, 0),
    new(0, 1),
    new(-1, 0)
};

while (queue.TryDequeue(out Point point))
{
    uint dist = distances[point.Y][point.X];
    uint distPlusOne = dist + 1;
    
    foreach (Point direction in directions)
    {
        Point next = point + (Size)direction;
        if (next.X < 0 || next.X > distances[0].Length - 1 || next.Y < 0 || next.Y > distances.Length - 1)
            continue;
        
        if (distances[next.Y][next.X] <= distPlusOne)
            continue;

        char nextHeight = heights[next.Y][next.X];
        char currHeight = heights[point.Y][point.X];
        if (nextHeight == 'E' && currHeight < 'x')
            continue;
        if (nextHeight > currHeight + 1 && currHeight != 'S')
            continue;

        distances[next.Y][next.X] = distPlusOne;
        queue.Enqueue(next);
    }
}

File.WriteAllLines("out.txt", distances.Select(row => string.Join('\t', row)));

Console.WriteLine(distances[end.Y][end.X]);

Console.WriteLine($"{stopwatch.ElapsedMilliseconds} ms");