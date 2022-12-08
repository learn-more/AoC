using System.Diagnostics;
using System.Runtime.CompilerServices;

static string GetSourceFilePathName([CallerFilePath] string? callerFilePath = null) => callerFilePath ?? string.Empty;
string[] input = System.IO.File.ReadAllLines(Path.Combine(Path.GetDirectoryName(GetSourceFilePathName()) ?? string.Empty, "input.txt"));
string[] test_input = @"30373
25512
65332
33549
35390".Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

static void checkTree(Tree tree, ref char hightest, ref int visible)
{
    if (tree.Height > hightest)
    {
        hightest = tree.Height;
        if (!tree.Visible)
        {
            visible++;
        }
        tree.Visible = true;
    }
}

static int CountVisibleTrees(Tree[,] forest/*, int width, int height*/)
{
    int visible = 0;
    int width = forest.GetLength(1);
    int height = forest.GetLength(0);
    for (int x = 0; x < width; x++)
    {
        char top = '\0';
        char bottom = '\0';
        for (int y = 0; y < height; ++y)
        {
            checkTree(forest[x, y], ref top, ref visible);
            checkTree(forest[x, height - y - 1], ref bottom, ref visible);
        }
    }

    for (int y = 0; y < height; ++y)
    {
        char left = '\0';
        char right = '\0';
        for (int x = 0; x < width; x++)
        {
            checkTree(forest[x, y], ref left, ref visible);
            checkTree(forest[width - x - 1, y], ref right, ref visible);
        }
    }
    return visible;
}

static int ScenicScore(Tree[,] forest, int xStart, int yStart)
{
    int width = forest.GetLength(1);
    int height = forest.GetLength(0);
    int len = 0;
    char current = forest[xStart,yStart].Height;
    for (int x = xStart-1; x >= 0; x--)
    {
        len++;
        if (forest[x, yStart].Height >= current)
            break;
    }
    int score = len;
    len = 0;
    for (int x = xStart+1; x < width; x++)
    {
        len++;
        if (forest[x, yStart].Height >= current)
            break;
    }
    score *= len;
    len = 0;

    for (int y = yStart-1; y >= 0; y--)
    {
        len++;
        if (forest[xStart, y].Height >= current)
            break;
    }
    score *= len;
    len = 0;
    for (int y = yStart+1; y < height; y++)
    {
        len++;
        if (forest[xStart, y].Height >= current)
            break;
    }
    score *= len;
    return score;
}

static int FindHighestScenic(Tree[,] forest)
{
    int width = forest.GetLength(1);
    int height = forest.GetLength(0);
    int highest = 0;
    for (int xStart = 0; xStart < width; xStart++)
    {
        for (int yStart = 0; yStart < height; ++yStart)
        {
            highest = Math.Max(highest, ScenicScore(forest, xStart, yStart));
        }
    }

    return highest;
}

static string GetResult(string[] input)
{
    Tree[,] forest = new Tree[input.Length, input[0].Length];
    for (int x = 0; x < input.Length; x++)
    {
        for (int y = 0; y < input.Length; ++y)
        {
            forest[x, y] = new Tree { Height = input[x][y] };
        }
    }

    int visible = CountVisibleTrees(forest);
    int highestScenic = FindHighestScenic(forest);

    return $"{visible},{highestScenic}";
}

Console.WriteLine($"Test result(21,8): {GetResult(test_input)}");
Console.WriteLine($"Result: {GetResult(input)}");


class Tree
{
    public char Height { get; init; }
    public bool Visible { get; set; } = false;
}
