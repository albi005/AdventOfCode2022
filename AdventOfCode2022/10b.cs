string[] lines = File.ReadAllLines("10.txt");

int x = 1;
int cycle = 0;

void Increment()
{
    int position = cycle % 40; 
    Console.Write(Math.Abs(position - x) < 2 ? '#' : '.');
    if (position == 39)
        Console.WriteLine();
    cycle++;
}

foreach (string line in lines)
{
    if (line == "noop")
        Increment();
    else
    {
        Increment();
        Increment();
        x += int.Parse(line.Split(' ')[1]);
    }
}