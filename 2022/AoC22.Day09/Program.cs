using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;

static string GetSourceFilePathName([CallerFilePath] string? callerFilePath = null) => callerFilePath ?? string.Empty;
string[] input = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(GetSourceFilePathName()) ?? string.Empty, "input.txt"));
string[] test_input = @"R 4
U 4
L 3
D 1
R 4
D 1
L 5
R 2".Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

string[] test2_input2 = @"R 5
U 8
L 8
D 3
R 17
D 10
L 25
U 20".Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);


static void StepKnots(ref Point Head, ref Point Tail)
{
    Point diff = new Point(Math.Abs(Head.X - Tail.X), Math.Abs(Head.Y - Tail.Y));
    if (diff.X > 1 || diff.Y > 1)
    {
        Tail.X += ((Head.X > Tail.X) ? 1 : -1) * Math.Clamp(diff.X, 0, 1);
        Tail.Y += ((Head.Y > Tail.Y) ? 1 : -1) * Math.Clamp(diff.Y, 0, 1);
    }
    diff = new Point(Math.Abs(Head.X - Tail.X), Math.Abs(Head.Y - Tail.Y));
    Debug.Assert(diff.X <= 1 && diff.Y <= 1);
}


static string GetResult(string[] input, int knots)
{
    HashSet<string> visited = new();

    Point[] rope = new Point[knots];
    for (int n = 0; n < knots; n++)
        rope[n] = new Point(0, 0);

    visited.Add($"{rope[^1].X},{rope[^1].Y}");

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
            rope[0].X += xAdd;
            rope[0].Y += yAdd;

            for (int n = 0; n < rope.Length-1; n++)
            {
                StepKnots(ref rope[n], ref rope[n + 1]);
            }

            visited.Add($"{rope[^1].X},{rope[^1].Y}");
        }
    }

    return $"{visited.Count}";
}

Console.WriteLine($"Test1 result(13): {GetResult(test_input, 2)}");
Console.WriteLine($"Result1: {GetResult(input, 2)}");

Console.WriteLine($"Test2 result(1): {GetResult(test_input, 10)}");
Console.WriteLine($"Test2 result(36): {GetResult(test2_input2, 10)}");
Console.WriteLine($"Result2: {GetResult(input, 10)}");
