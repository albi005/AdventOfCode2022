string[] lines = File.ReadAllText("05.txt")
    .Split("\n");

Stack<char>[] stacks = Enumerable.Range(0, 9).Select(_ => new Stack<char>()).ToArray();

foreach (string line in lines.Take(8).Reverse())
{
    for (int i = 0; i < 9; i++)
    {
        char val = line[i * 4 + 1];
        if (val != ' ')
            stacks[i].Push(val);
    }
}

foreach (string line in lines.Skip(10))
{
    string[] parts = line.Split(' ');
    int count = int.Parse(parts[1]);
    int from = int.Parse(parts[3]) - 1;
    int to = int.Parse(parts[5]) - 1;
    char[] buffer = new char[count];
    for (int i = 0; i < count; i++)
        buffer[i] = stacks[from].Pop();
    foreach (char c in buffer.Reverse())
        stacks[to].Push(c);
}

string.Join("", stacks.Select(s => s.Peek())).Print();