byte[][] rows = File.ReadAllLines("08.txt")
    .Select(row => row.Select(x => byte.Parse(x.ToString())).ToArray())
    .ToArray();

rows.Select((row, y) =>
        row.Where((val, x) =>
            {
                bool isVisible =
                    row.Take(x).All(b => b < val)
                    || row.Skip(x + 1).All(b => b < val)
                    || rows.Take(y).All(r => r[x] < val)
                    || rows.Skip(y + 1).All(r => r[x] < val);
                return isVisible;
            })
            .Count())
    .Sum()
    .Print();