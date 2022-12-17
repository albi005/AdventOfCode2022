IEnumerable<int> enumerable = File
    .ReadAllText("13.txt")
    .Split("\n\n")
    .Select(pair => pair.Split("\n"))
    .Select((lines, i) => CompareLists(RemoveBrackets(lines[0]), RemoveBrackets(lines[1])) > 0 ? i + 1 : 0);

enumerable.Sum().Print();
Console.WriteLine();

// 1,[]
int CompareLists(ReadOnlySpan<char> left, ReadOnlySpan<char> right)
{
    while (true)
    {
        bool lHasMore = left.Length > 0;
        bool rHasMore = right.Length > 0;
        if (!lHasMore || !rHasMore)
        {
            if (!lHasMore && !rHasMore)
                return 0;
            if (lHasMore) return -1;
            return 1;
        }

        ReadOnlySpan<char> lItem = GetFirstItem(left);
        ReadOnlySpan<char> rItem = GetFirstItem(right);

        int c = CompareItems(lItem, rItem);
        if (c != 0) return c;

        left = left == lItem ? ReadOnlySpan<char>.Empty : left[(lItem.Length + 1)..];
        right = right == rItem ? ReadOnlySpan<char>.Empty : right[(rItem.Length + 1)..];
    }
}

// 1 or []
int CompareItems(ReadOnlySpan<char> left, ReadOnlySpan<char> right)
{
    bool leftIsInt = uint.TryParse(left, out uint leftInt);
    bool rightIsInt = uint.TryParse(right, out uint rightInt);
    if (leftIsInt && rightIsInt)
        return rightInt.CompareTo(leftInt);
    if (!leftIsInt)
        left = RemoveBrackets(left);
    if (!rightIsInt)
        right = RemoveBrackets(right);
    return CompareLists(left, right);
}

// 1,[]
ReadOnlySpan<char> GetFirstItem(ReadOnlySpan<char> list)
{
    int end = 0;
    if (list[0] == '[')
    {
        int bracketCount = 1;
        while (bracketCount != 0)
        {
            end++;
            if (list[end] == ']')
                bracketCount--;
            else if (list[end] == '[')
                bracketCount++;
        }

        return list[..(end + 1)];
    }

    while (end < list.Length && list[end] != ',') end++;
    return list[..end];
}

// []
ReadOnlySpan<char> RemoveBrackets(ReadOnlySpan<char> list) => list[1..^1];
