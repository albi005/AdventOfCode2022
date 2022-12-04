int result = File.ReadAllText("01.txt")
    .Split("\n\n")
    .Select(x => x.Split("\n").Select(int.Parse).Sum())
    .OrderDescending()
    .Take(3)
    .Sum();
    
Console.WriteLine(result);