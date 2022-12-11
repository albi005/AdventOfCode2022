Monkey[] monkeys =
{
    new(old => old * 2,
        item => item % 5 == 0,
        98, 89, 52),
    new(old => old * 13,
        item => item % 2 == 0,
        57, 95, 80, 92, 57, 78),
    new(old => old + 5,
        item => item % 19 == 0,
        82, 74, 97, 75, 51, 92, 83),
    new(old => old + 6,
        item => item % 7 == 0,
        97, 88, 51, 68, 76),
    new(old => old + 1,
        item => item % 17 == 0,
        63),
    new(old => old + 4,
        item => item % 13 == 0,
        94, 91, 51, 63),
    new(old => old + 2,
        item => item % 3 == 0,
        61, 54, 94, 71, 74, 68, 98, 83),
    new(old => old * old,
        item => item % 11 == 0,
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

for (int round = 0; round < 20; round++)
    foreach (Monkey monkey in monkeys)
        monkey.Act();

Monkey[] top2 = monkeys.OrderByDescending(m => m.BusinessCount).Take(2).ToArray();
Console.WriteLine(top2[0].BusinessCount * top2[1].BusinessCount);

public class Monkey
{
    public Monkey(Func<int, int> operation, Func<int, bool> test, params int[] items)
    {
        Operation = operation;
        Test = test;
        foreach (int item in items)
            Items.Enqueue(item);
    }
    
    public Queue<int> Items { get; } = new();
    public Func<int, int> Operation { get; set; }
    public Func<int, bool> Test { get; set; }
    public Monkey IfTrue { get; set; } = null!;
    public Monkey IfFalse { get; set; } = null!;
    public int BusinessCount { get; private set; }

    public void Act()
    {
        while (Items.TryDequeue(out int item))
        {
            BusinessCount++;
            item = Operation(item);
            item /= 3;
            if (Test(item))
                IfTrue.Items.Enqueue(item);
            else
                IfFalse.Items.Enqueue(item);
        }
    }
}