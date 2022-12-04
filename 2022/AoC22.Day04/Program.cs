using System.Diagnostics;
using System.Runtime.CompilerServices;

static string GetSourceFilePathName([CallerFilePath] string? callerFilePath = null) => callerFilePath ?? string.Empty;
string[] input = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(GetSourceFilePathName()) ?? string.Empty, "input.txt"));
string[] test_input = @"2-4,6-8
2-3,4-5
5-7,7-9
2-8,3-7
6-6,4-6
2-6,4-8".Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);


static int[] Range(string input)
{
    string[] items = input.Split('-');
    Debug.Assert(items.Length == 2);

    int min = int.Parse(items[0]);
    int max = int.Parse(items[1]);

    return Enumerable.Range(min, max-min+1).ToArray();
}

static int CalcCompleteOverlap(string[] inputs)
{
    int contained = 0;
    foreach (string line in inputs)
    {
        string[] pair = line.Split(',');
        Debug.Assert(pair.Length == 2);

        var left = Range(pair[0]);
        var right = Range(pair[1]);

        int len = left.Union(right).Count();
        if (len == left.Length || len == right.Length)
            contained++;
    }
    return contained;
}

static int CalcPartialOverlap(string[] inputs)
{
    int contained = 0;
    foreach (string line in inputs)
    {
        string[] pair = line.Split(',');
        Debug.Assert(pair.Length == 2);

        var left = Range(pair[0]);
        var right = Range(pair[1]);

        int len = left.Union(right).Count();
        if (len < left.Length+right.Length)
            contained++;
    }
    return contained;
}

Console.WriteLine($"Test result(2): {CalcCompleteOverlap(test_input)}");
Console.WriteLine($"Result: {CalcCompleteOverlap(input)}");

Console.WriteLine($"Test result(4): {CalcPartialOverlap(test_input)}");
Console.WriteLine($"Result: {CalcPartialOverlap(input)}");

