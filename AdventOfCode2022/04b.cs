File.ReadAllText("04.txt")
    .Split("\n")
    .Count(line =>
    {
        string[] split = line.Split(',');
        string[] first = split[0].Split('-');
        string[] second = split[1].Split('-');
        int firstStart = int.Parse(first[0]);
        int firstEnd = int.Parse(first[1]);
        int secondStart = int.Parse(second[0]);
        int secondEnd = int.Parse(second[1]);
        return (secondStart <= firstEnd && secondEnd >= firstStart)
               ||
               (firstStart <= secondEnd && firstEnd >= secondStart);

    })
    .Print();