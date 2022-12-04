int GetPoints(string text)
{
    int points = 0;
    points += text[2] switch
    {
        'X' => 0,
        'Y' => 3,
        'Z' => 6,
    };
    char mine = text switch
    {
        "A X" => 'C',
        "A Y" => 'A',
        "A Z" => 'B',
        "B X" => 'A',
        "B Y" => 'B',
        "B Z" => 'C',
        "C X" => 'B',
        "C Y" => 'C',
        "C Z" => 'A',
    };
    points += mine switch
    {
        'A' => 1,
        'B' => 2,
        'C' => 3
    };
    return points;
}

var sum = File.ReadAllLines("02.txt")
    .Select(GetPoints)
    .Sum();
Console.WriteLine(sum);