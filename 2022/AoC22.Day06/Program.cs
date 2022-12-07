using System.Diagnostics;
using System.Runtime.CompilerServices;

static string GetSourceFilePathName([CallerFilePath] string? callerFilePath = null) => callerFilePath ?? string.Empty;
string input = File.ReadAllText(Path.Combine(Path.GetDirectoryName(GetSourceFilePathName()) ?? string.Empty, "input.txt"));
string[] test_input = @"mjqjpqmgbljsphdztnvjfqwrcgsmlb
bvwbjplbgvbhsrlpgdmjqwftvncz
nppdvjthqldpwncqszvftbrmjlhg
nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg
zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw".Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);


static int FindMarker(string line, int length)
{
    HashSet<char> prev = new();
    int n;
    for (n = 0; n < line.Length;)
    {
        char c = line[n];
        if (prev.Contains(c))
        {
            n -= (prev.Count - 1);
            prev.Clear();
        }
        else
        {
            prev.Add(c);
            if (prev.Count == length)
                break;
            n++;
        }
    }
    return n+1;
}


static void Test1(string[] lines)
{
    int[] expect = new int[] { 7, 5, 6, 10, 11 };
    Debug.Assert(expect.Length == lines.Length);
    for (int idx = 0; idx < lines.Length; ++idx)
    {
        var line = lines[idx];
        int marker = FindMarker(line, 4);
        Console.WriteLine($"Got: {marker}, wanted: {expect[idx]}");
    }
}

static void Test2(string[] lines)
{
    int[] expect = new int[] { 19, 23, 23, 29, 26 };
    Debug.Assert(expect.Length == lines.Length);
    for (int idx = 0; idx < lines.Length; ++idx)
    {
        var line = lines[idx];
        int marker = FindMarker(line, 14);
        Console.WriteLine($"Got: {marker}, wanted: {expect[idx]}");
    }
}

Test1(test_input);
Test2(test_input);

Console.WriteLine($"Result: {FindMarker(input, 4)}");
Console.WriteLine($"Result: {FindMarker(input, 14)}");

