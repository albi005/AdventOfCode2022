File.ReadAllText("01.txt")
    .Split("\n\n")
    .Select(x => x.Split("\n").Select(y => int.Parse(y)).Sum())
    .OrderDescending()
    .First()
    .Print();