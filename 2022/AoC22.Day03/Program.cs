using System.Diagnostics;
using System.Runtime.CompilerServices;

static string GetSourceFilePathName([CallerFilePath] string? callerFilePath = null) => callerFilePath ?? string.Empty;
string[] input = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(GetSourceFilePathName()) ?? string.Empty, "input.txt"));
string[] test_input = @"vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw".Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

static int Priority(char c)
{
    if (c >= 'a' && c <= 'z')
        return c - 'a' + 1;
    else if (c >= 'A' && c <= 'Z')
        return c - 'A' + 27;
    Debug.Assert(false);
    return 0;
}


static int CalcScore(string[] inputs)
{
    Debug.Assert(Priority('a') == 1);
    Debug.Assert(Priority('z') == 26);
    Debug.Assert(Priority('A') == 27);
    Debug.Assert(Priority('Z') == 52);

    int sum = 0;
    foreach (string line in inputs)
    {
        string front = line.Substring(0, line.Length / 2);
        string back = line.Substring(line.Length / 2);
        Debug.Assert(front.Length == back.Length);

        char[] intersect = front.Intersect(back).ToArray();
        Debug.Assert(intersect.Length == 1);
        sum += Priority(intersect[0]);
    }
    return sum;
}

static int CalcScore_Group(string[] inputs)
{
    Debug.Assert(Priority('a') == 1);
    Debug.Assert(Priority('z') == 26);
    Debug.Assert(Priority('A') == 27);
    Debug.Assert(Priority('Z') == 52);

    int sum = 0;
    for (int i = 0; i < inputs.Length; i += 3)
    {
        char[] intersect = inputs[0+i].Intersect(inputs[1 + i]).Intersect(inputs[2 + i]).ToArray();
        Debug.Assert(intersect.Length == 1);
        sum += Priority(intersect[0]);
    }
    return sum;
}


Console.WriteLine($"Test score(157): {CalcScore(test_input)}");
Console.WriteLine($"Result: {CalcScore(input)}");

Console.WriteLine($"Test score(70): {CalcScore_Group(test_input)}");
Console.WriteLine($"Result: {CalcScore_Group(input)}");

