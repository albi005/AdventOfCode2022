string[] lines = File.ReadAllLines("10.txt");

int x = 1;
int cycle = 0;
long sum = 0;

void Increment()
{
    cycle++;
    if (cycle is 20 or 60 or 100 or 140 or 180 or 220)
    {
        sum += cycle * x;
    }
}

foreach (string line in lines)
{
    if (line == "noop")
        Increment();
    else
    {
        Increment();
        x += int.Parse(line.Split(' ')[1]);
        Increment();
    }
}

sum.Print();