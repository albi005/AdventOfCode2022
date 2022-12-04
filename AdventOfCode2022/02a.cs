int GetPoints(string text)
{
    int points = 0;
    points += text[2] switch
    {
        'X' => 1,
        'Y' => 2,
        'Z' => 3,
        _ => throw new ArgumentOutOfRangeException()
    };
    points += text switch
    {
        "A X" => 3,
        "A Y" => 6,
        "A Z" => 0,
        "B X" => 0,
        "B Y" => 3,
        "B Z" => 6,
        "C X" => 6,
        "C Y" => 0,
        "C Z" => 3,
        _ => throw new ArgumentOutOfRangeException(nameof(text), text, null)
    };
    return points;
}

var sum = File.ReadAllLines("02.txt")
    .Select(GetPoints)
    .Sum();
Console.WriteLine(sum);