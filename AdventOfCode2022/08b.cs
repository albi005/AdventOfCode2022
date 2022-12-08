byte[][] rows = File.ReadAllLines("08.txt")
    .Select(row => row.Select(x => byte.Parse(x.ToString())).ToArray())
    .ToArray();

int CalculateDistance(IEnumerable<byte> source, int height)
{
    int index = 0;
    foreach (byte val in source)
    {
        index++;
        if (val >= height) break;
    }

    return index;
}

rows.Select((row, y) =>
        row.Select((height, x)
                => CalculateDistance(row.Take(x).Reverse(), height)
                   * CalculateDistance(row.Skip(x + 1), height)
                   * CalculateDistance(rows.Take(y).Reverse().Select(r => r[x]), height)
                   * CalculateDistance(rows.Skip(y + 1).Select(r => r[x]), height))
            .Max())
    .Max()
    .Print();