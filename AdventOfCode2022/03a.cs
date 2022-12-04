int sum = File.ReadAllText("03.txt")
    .Split("\n")
    .Select<string, (string First, string Second)>(line => (line[..(line.Length / 2)], line[(line.Length / 2)..]))
    .Select(halves =>
    {
        HashSet<char> seen = halves.First.ToHashSet();
        return halves.Second.First(x => seen.Contains(x));
    })
    .Select(x => x <= 90 ? x - 'A' + 27 : x - 'a' + 1)
    .Sum();
    
Console.WriteLine(sum);