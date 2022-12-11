Monkey[] monkeys =
{
    new(x => x * 2,
        x => x % 5 == 0,
        98, 89, 52),
    new(x => x * 13,
        x => x % 2 == 0,
        57, 95, 80, 92, 57, 78),
    new(x => x + 5,
        x => x % 19 == 0,
        82, 74, 97, 75, 51, 92, 83),
    new(x => x + 6,
        x => x % 7 == 0,
        97, 88, 51, 68, 76),
    new(x => x + 1,
        x => x % 17 == 0,
        63),
    new(x => x + 4,
        x => x % 13 == 0,
        94, 91, 51, 63),
    new(x => x + 2,
        x => x % 3 == 0,
        61, 54, 94, 71, 74, 68, 98, 83),
    new(x => x * x,
        x => x % 11 == 0,
        90, 56)
};

monkeys[0].IfTrue = monkeys[6];
monkeys[0].IfFalse = monkeys[1];

monkeys[1].IfTrue = monkeys[2];
monkeys[1].IfFalse = monkeys[6];

monkeys[2].IfTrue = monkeys[7];
monkeys[2].IfFalse = monkeys[5];

monkeys[3].IfTrue = monkeys[0];
monkeys[3].IfFalse = monkeys[4];

monkeys[4].IfTrue = monkeys[0];
monkeys[4].IfFalse = monkeys[1];

monkeys[5].IfTrue = monkeys[4];
monkeys[5].IfFalse = monkeys[3];

monkeys[6].IfTrue = monkeys[2];
monkeys[6].IfFalse = monkeys[7];

monkeys[7].IfTrue = monkeys[3];
monkeys[7].IfFalse = monkeys[5];

for (long round = 0; round < 10_000; round++)
    foreach (Monkey monkey in monkeys)
        monkey.Act();

Monkey[] top2 = monkeys.OrderByDescending(m => m.BusinessCount).Take(2).ToArray();
Console.WriteLine(top2[0].BusinessCount * top2[1].BusinessCount);

public class Monkey
{
    public Monkey(Func<long, long> operation, Func<long, bool> test, params long[] items)
    {
        Operation = operation;
        Test = test;
        foreach (long item in items)
            Items.Enqueue(item);
    }

    public Queue<long> Items { get; } = new();
    public Func<long, long> Operation { get; }
    public Func<long, bool> Test { get; }
    public Monkey IfTrue { get; set; } = null!;
    public Monkey IfFalse { get; set; } = null!;
    public long BusinessCount { get; private set; }

    public void Act()
    {
        while (Items.TryDequeue(out long item))
        {
            BusinessCount++;
            item = Operation(item);
            item %= 2 * 3 * 5 * 7 * 11 * 13 * 17 * 19;
            if (Test(item))
                IfTrue.Items.Enqueue(item);
            else
                IfFalse.Items.Enqueue(item);
        }
    }
}