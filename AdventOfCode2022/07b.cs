string[] lines = System.IO.File.ReadAllLines("07.txt");

Directory cd = new("", null);
Directory root = cd;

foreach (string line in lines.Skip(1))
{
    if (line == "$ ls" || line.StartsWith("dir ")) continue;
    if (line == "$ cd ..")
    {
        cd = cd.Parent ?? cd;
        continue;
    }
    if (line.StartsWith("$ cd"))
    {
        string dirName = line[5..];
        Directory directory = new(dirName, cd);
        cd.Directories.Add(directory);
        cd = directory;
        continue;
    }

    string[] fileSizeAndName = line.Split(' ');
    if (cd.Files.Any(f => f.Name == fileSizeAndName[1])) continue;
    
    File file = new(fileSizeAndName[1], int.Parse(fileSizeAndName[0]));
    cd.Files.Add(file);
    Directory temp = cd;
    temp.Size += file.Size;
    while (temp.Parent != null)
    {
        temp = temp.Parent;
        temp.Size += file.Size;
    }
}

long used = root.Size;
const long total = 70_000_000;
long unused = total - used;
const long minUnused = 30_000_000;
long minToClean = minUnused - unused;

Directory best = root;
void Visit(Directory dir)
{
    if (dir.Size < minToClean) return;
    if (dir.Size < best.Size)
        best = dir;
    foreach (Directory subDir in dir.Directories)
    {
        Visit(subDir);
    }
}

Visit(root);
best.Size.Print();

public record Directory(string Name, Directory? Parent)
{
    public long Size { get; set; }
    public List<Directory> Directories { get; } = new();
    public List<File> Files { get; } = new();
}

public record File(string Name, int Size);