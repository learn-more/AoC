using System.Runtime.CompilerServices;

static string GetSourceFilePathName([CallerFilePath] string? callerFilePath = null) => callerFilePath ?? string.Empty;
string[] input = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(GetSourceFilePathName()) ?? string.Empty, "input.txt"));

static IEnumerable<int> FindMax(string[] inputs, int max_count)
{
    List<int> counts = new();
    int total = 0;
    foreach (string line in inputs.Select(elem => elem.Trim()))
    {
        if (string.IsNullOrEmpty(line))
        {
            counts.Add(total);
            total = 0;
        }
        else
        {
            total += int.Parse(line);
        }
    }
    counts.Add(total);
    return counts.OrderByDescending(e => e).Take(max_count);
}

Console.WriteLine($"Max(1): {FindMax(input, 1).Sum()}");
Console.WriteLine($"Max(3): {FindMax(input, 3).Sum()}");

