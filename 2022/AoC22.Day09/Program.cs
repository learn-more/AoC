using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;

static string GetSourceFilePathName([CallerFilePath] string? callerFilePath = null) => callerFilePath ?? string.Empty;
string[] input = System.IO.File.ReadAllLines(Path.Combine(Path.GetDirectoryName(GetSourceFilePathName()) ?? string.Empty, "input.txt"));
string[] test_input = @"R 4
U 4
L 3
D 1
R 4
D 1
L 5
R 2".Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

static string GetResult(string[] input)
{
    HashSet<string> visited = new();
    Point Head = new Point(0, 0);
    Point Tail = new Point(0, 0);

    visited.Add($"{Tail.X},{Tail.Y}");

    foreach(string line in input)
    {
        char dir = line[0];
        int xAdd = 0, yAdd = 0;
        int dist = int.Parse(line[2..]);
        switch(dir)
        {
            case 'U': yAdd = 1; break;
            case 'D': yAdd = -1; break;
            case 'L': xAdd = -1; break;
            case 'R': xAdd= 1; break;
            default: Debug.Assert(false); break;
        }

        while (dist-- > 0)
        {
            Head.X += xAdd;
            Head.Y += yAdd;

            Point diff = new Point(Math.Abs(Head.X - Tail.X), Math.Abs(Head.Y - Tail.Y));
            if (diff.X > 1 || diff.Y > 1)
            {
                Tail.X += ((Head.X > Tail.X) ? 1 : -1) * Math.Clamp(diff.X, 0, 1);
                Tail.Y += ((Head.Y > Tail.Y) ? 1 : -1) * Math.Clamp(diff.Y, 0, 1);
            }
            diff = new Point(Math.Abs(Head.X - Tail.X), Math.Abs(Head.Y - Tail.Y));
            Debug.Assert(diff.X <= 1 && diff.Y <= 1);
            visited.Add($"{Tail.X},{Tail.Y}");
        }
    }

    return $"{visited.Count}";

}

Console.WriteLine($"Test result(13): {GetResult(test_input)}");
Console.WriteLine($"Result: {GetResult(input)}");
