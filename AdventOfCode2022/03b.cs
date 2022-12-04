File.ReadAllText("03.txt")
    .Split("\n")
    .Chunk(3)
    .Select(lines =>
    {
        HashSet<char> first = lines[0].ToHashSet();
        HashSet<char> second = lines[1].ToHashSet();
        return lines[2].First(x => first.Contains(x) && second.Contains(x));
    })
    .Select(x => x <= 90 ? x - 'A' + 27 : x - 'a' + 1)
    .Sum()
    .Print();