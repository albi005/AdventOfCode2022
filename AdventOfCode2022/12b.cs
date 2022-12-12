using System.Diagnostics;
using System.Drawing;

Stopwatch stopwatch = Stopwatch.StartNew();

string[] heights = File.ReadAllLines("12.txt");

Point end = new();

uint[][] distances = new uint[heights.Length][];
for (int i = 0; i < heights.Length; i++)
{
    distances[i] = new uint[heights[0].Length];
    for (int j = 0; j < heights[0].Length; j++)
    {
        distances[i][j] = uint.MaxValue;
        char height = heights[i][j];
        if (height == 'E')
            end = new(j, i);
    }
}

distances[end.Y][end.X] = 0;
Queue<Point> queue = new();
queue.Enqueue(end);

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
        if (currHeight == 'E')
            currHeight = 'z';
        if (currHeight == 'S')
            currHeight = 'a';
        if (nextHeight < currHeight - 1)
            continue;

        distances[next.Y][next.X] = distPlusOne;
        queue.Enqueue(next);
    }
}

File.WriteAllLines("out.txt", distances.Select(row => string.Join('\t', row)));

heights
    .Select((row, y) => row
        .Select((val, x) => (Height: val, Distance: distances[y][x])))
    .SelectMany(row => row)
    .Where(tuple => tuple.Height == 'a')
    .Min(tuple => tuple.Distance)
    .Print();

Console.WriteLine($"{stopwatch.ElapsedMilliseconds} ms");