using System.Runtime.CompilerServices;

static string GetSourceFilePathName([CallerFilePath] string? callerFilePath = null) => callerFilePath ?? string.Empty;
string[] input = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(GetSourceFilePathName()) ?? string.Empty, "input.txt"));
string[] test_input = @"A Y
B X
C Z".Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);


static RPS Translate(string input, string[] opts) => (RPS)Array.IndexOf(opts, input);
static RESULT TranslateResult(string input, string[] opts) => (RESULT)Array.IndexOf(opts, input);


static int Score(RPS opponent, RPS self)
{
    return (int)DUM.scores[(int)self, (int)opponent] * 3;
}


static RPS FromResult(RPS opponent, RESULT result)
{
    foreach (RPS self in Enum.GetValues<RPS>())
    {
        if (DUM.scores[(int)self, (int)opponent] == result)
            return self;
    }

    return RPS.Rock;
}


static int CalcScore_RPS_RPS(string[] inputs)
{
    int score = 0;
    foreach (string line in inputs)
    {
        string[] data = line.Split(new char[] { ' ' });
        RPS opponent = Translate(data[0], new string[] { "A", "B", "C" });
        RPS self = Translate(data[1], new string[] { "X", "Y", "Z" });

        score += (1 + (int)self) + Score(opponent, self);
    }
    return score;
}

static int CalcScore_RPS_WIN(string[] inputs)
{
    int score = 0;
    foreach (string line in inputs)
    {
        string[] data = line.Split(new char[] { ' ' });
        RPS opponent = Translate(data[0], new string[] { "A", "B", "C" });
        RESULT result = TranslateResult(data[1], new string[] { "X", "Y", "Z" });
        RPS self = FromResult(opponent, result);

        score += (1 + (int)self) + Score(opponent, self);
    }
    return score;
}

Console.WriteLine($"Test score(15): {CalcScore_RPS_RPS(test_input)}");
Console.WriteLine($"Result: {CalcScore_RPS_RPS(input)}");

Console.WriteLine($"Test score(12): {CalcScore_RPS_WIN(test_input)}");
Console.WriteLine($"Result: {CalcScore_RPS_WIN(input)}");


enum RPS
{
    Rock,
    Paper,
    Scissors
};

enum RESULT
{
    Lose,
    Draw,
    Win_,
}

class DUM
{
    public static RESULT[,] scores = new RESULT[,] {
    //     Rock,        Paper,       Scissors
  /*R*/  { RESULT.Draw, RESULT.Lose, RESULT.Win_ },
  /*P*/  { RESULT.Win_, RESULT.Draw, RESULT.Lose },
  /*S*/  { RESULT.Lose, RESULT.Win_, RESULT.Draw }
};
}
