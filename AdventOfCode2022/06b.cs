string input = File.ReadAllText("06.txt");

Dictionary<char, int> counter = new();

foreach (char c in input.Take(13))
{
    if (counter.ContainsKey(c))
        counter[c]++;
    else
        counter[c] = 1;
}

for (int i = 13; i < input.Length; i++)
{
    char added = input[i];
    if (counter.ContainsKey(added))
        counter[added]++;
    else
        counter[added] = 1;

    if (counter.Count == 14)
    {
        Console.WriteLine(i + 1);
        return;
    }
    
    char removed = input[i - 13];
    counter[removed]--;
    if (counter[removed] == 0)
        counter.Remove(removed);
}