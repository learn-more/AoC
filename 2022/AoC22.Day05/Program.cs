using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

static string GetSourceFilePathName([CallerFilePath] string? callerFilePath = null) => callerFilePath ?? string.Empty;
string[] input = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(GetSourceFilePathName()) ?? string.Empty, "input.txt"));
string[] test_input = @"    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2".Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);


static IEnumerable<string> Split(string str, int chunkSize)
{
    int index = 0;
    while (index < str.Length)
    {
        yield return str.Substring(index, Math.Min(chunkSize, str.Length - index));
        index += chunkSize;
    }
}

static Stack<char>[] ParseStacks(string[] lines)
{
    Stack<char>[]? stacks = null;
    foreach (string line in lines)
    {
        if (string.IsNullOrEmpty(line))
        {
            Debug.Assert(false);
        }

        var parts = Split(line, 4).Select(s => s.Trim()).ToArray();
        if (stacks == null)
        {
            stacks = new Stack<char>[parts.Length];
            for (int n = 0; n < parts.Length; n++)
            {
                stacks[n] = new Stack<char>();
            }
        }
        Debug.Assert(stacks.Length == parts.Length);
        if (!line.Replace(" ", "").All(char.IsDigit))
        {
            for (int n = 0; n < parts.Length; n++)
            {
                if (!string.IsNullOrEmpty(parts[n]))
                {
                    stacks[n].Push(parts[n][1]);
                }
            }
        }
        else
        {
            for (int n = 0;n < stacks.Length; n++)
            {
                Stack<char> rev = new();
                while (stacks[n].Count != 0)
                {
                    rev.Push(stacks[n].Pop());
                }
                stacks[n] = rev;
            }
            return stacks;
        }
    }
    return new Stack<char>[0];
}

static Tuple<int,int, int>[] ParseMoves(string[] lines)
{
    List<Tuple<int, int, int>> moves = new();
    foreach (string line in lines)
    {
        var m = Regex.Match(line, @"move (\d+) from (\d) to (\d)");
        if (m.Success)
        {
            moves.Add(new Tuple<int, int, int>(int.Parse(m.Groups[1].Value), int.Parse(m.Groups[2].Value), int.Parse(m.Groups[3].Value)));
        }
    }
    return moves.ToArray();
}


static string GetResult(string[] lines, bool atOnce)
{
    var stacks = ParseStacks(lines);
    var moves = ParseMoves(lines);

    foreach(var move in moves)
    {
        int count = move.Item1;
        int from = move.Item2;
        int to = move.Item3;
        if (atOnce)
        {
            List<char> items = new();
            for (int n = 0; n < count; ++n)
                items.Add(stacks[from - 1].Pop());
            for (int n = 0; n < count; n++)
                stacks[to - 1].Push(items[count - n-1]);
        }
        else
        {
            for (int n = 0; n < count; ++n)
            {
                char c = stacks[from - 1].Pop();
                stacks[to - 1].Push(c);
            }
        }
    }
    return new string(stacks.Select(stack => stack.Peek()).ToArray());
}


Console.WriteLine($"Test result(CMZ): {GetResult(test_input, false)}");
Console.WriteLine($"Result: {GetResult(input, false)}");

Console.WriteLine($"Test result(MCD): {GetResult(test_input, true)}");
Console.WriteLine($"Result: {GetResult(input, true)}");
