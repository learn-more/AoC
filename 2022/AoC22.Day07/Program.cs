using System.Diagnostics;
using System.Runtime.CompilerServices;

static string GetSourceFilePathName([CallerFilePath] string? callerFilePath = null) => callerFilePath ?? string.Empty;
string[] input = System.IO.File.ReadAllLines(Path.Combine(Path.GetDirectoryName(GetSourceFilePathName()) ?? string.Empty, "input.txt"));
string[] test_input = @"$ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k".Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

static IEnumerable<Directory> FindDirs(Directory root, int maxSize)
{
    foreach(var f in root.Entries)
    {
        if (f is Directory dir)
        {
            if (f.Size() <= maxSize)
                yield return dir;

            foreach (var x in FindDirs(dir, maxSize))
                yield return x;
        }
    }
}

static void FindBiggerThan(Directory root, int minSize, ref int currentSmallest)
{
    foreach(var f in root.Entries)
    {
        if (f is Directory dir)
        {
            int size = f.Size();
            if (size >= minSize && size < currentSmallest)
            {
                currentSmallest = size;
            }

            FindBiggerThan(dir, minSize, ref currentSmallest);
        }
    }
}

string GetResult(string[] input, int requiredFree)
{
    Directory root = new ("/", null);
    Directory? current = null;

    foreach (string line in input.Select(elem => elem.Trim()))
    {
        if (line[0] == '$')
        {
            string[] args = line[1..].Trim().Split(' ');
            switch(args[0])
            {
                case "cd":
                    Debug.Assert(args.Length == 2);
                    if (args[1] == "/")
                    {
                        current = root;
                    }
                    else if (args[1] == "..")
                    {
                        Debug.Assert(current != null);
                        Debug.Assert(current != current.Parent);
                        current = current.Parent;
                    }
                    else
                    {
                        Debug.Assert(current != null);
                        current = current.Find(args[1]);
                    }
                    break;
                case "ls":
                    // ignore
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
        }
        else
        {
            Debug.Assert(current != null);
            string[] args = line.Split(' ');
            Debug.Assert(args.Length == 2);
            if (args[0] == "dir")
            {
                current.Entries.Add(new Directory(args[1], current));
            }
            else
            {
                current.Entries.Add(new File(args[1], int.Parse(args[0])));
            }
        }
    }

    var all = FindDirs(root, maxSize: 100000);
    int summSmall = all.Sum(elem => elem.Size());

    int rootUsed = root.Size();
    int free = 70000000 - rootUsed;
    requiredFree = requiredFree - free;

    int smallestFree = int.MaxValue;
    FindBiggerThan(root, requiredFree, ref smallestFree);

    return $"{summSmall},{smallestFree}";
}

Console.WriteLine($"Test result(95437,24933642): {GetResult(test_input, requiredFree: 30000000)}");
Console.WriteLine($"Result: {GetResult(input, requiredFree: 30000000)}");


class File
{
    internal readonly string Name;
    readonly int mSize;

    internal File(string name, int size)
    {
        Name = name;
        mSize = size;
    }

    internal virtual int Size() => mSize;
}

class Directory: File
{
    internal readonly Directory Parent;
    internal List<File> Entries = new();

    internal Directory(string name, Directory? parent)
        :base(name,0)
    {
        Parent = parent ?? this;
    }

    internal Directory? Find(string name)
    {
        foreach(var f in Entries)
        {
            if (f.Name == name && f is Directory dir)
            {
                return dir;
            }
        }
        Debug.Assert(false);
        return null;
    }

    internal override int Size()
    {
        return Entries.Sum(elem => elem.Size());
    }
}

