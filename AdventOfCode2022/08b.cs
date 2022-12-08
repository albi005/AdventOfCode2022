byte[][] rows = File.ReadAllLines("08.txt")
    .Select(row => row.Select(x => byte.Parse(x.ToString())).ToArray())
    .ToArray();

rows.Select((row, y) =>
        row.Select((_, x) =>
                row.Take(x + 1).Reverse().DistanceFromFirst()
                * row.Skip(x).DistanceFromFirst()
                * rows.Take(y + 1).Reverse().Select(r => r[x]).DistanceFromFirst()
                * rows.Skip(y).Select(r => r[x]).DistanceFromFirst())
            .Max())
    .Max()
    .Print();

public static class Minecraft
{
    public static int DistanceFromFirst(this IEnumerable<byte> source)
    {
        byte first = source.FirstOrDefault();
        int index = 0;
        foreach (byte val in source.Skip(1))
        {
            index++;
            if (val >= first) break;
        }

        return index;
    }
}