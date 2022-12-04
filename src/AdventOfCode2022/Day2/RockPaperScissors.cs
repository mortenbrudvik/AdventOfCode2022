using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2022.Day2;

public class RockPaperScissors
{
    private readonly ITestOutputHelper _logger;

    public RockPaperScissors(ITestOutputHelper logger) => _logger = logger;

    [Fact]
    public void Part1_ScoreAccordingToStrategyGuide()
    {
        var score = File.ReadLines("Day2/input.txt")
            .Select(line => line.Split(' '))
            .Select(x => CalculateScore(
                opponentAction: new GameAction(char.Parse(x.First())),
                yourAction: new GameAction(char.Parse(x.Last()))
                ))
            .Sum();
        
        _logger.WriteLine("Score according to strategy guide: " + score);
    }


    private static int CalculateScore(GameAction opponentAction, GameAction yourAction) => opponentAction switch
    {
        _ when opponentAction.IsPaper && yourAction.IsPaper => 3 + yourAction.Score,
        _ when opponentAction.IsPaper && yourAction.IsRock => yourAction.Score,
        _ when opponentAction.IsPaper && yourAction.IsScissors => 6 + yourAction.Score,
        
        _ when opponentAction.IsRock && yourAction.IsRock => 3 + yourAction.Score,
        _ when opponentAction.IsRock && yourAction.IsPaper => 6 + yourAction.Score,
        _ when opponentAction.IsRock && yourAction.IsScissors => yourAction.Score,
        
        _ when opponentAction.IsScissors && yourAction.IsScissors => 3 + yourAction.Score,
        _ when opponentAction.IsScissors && yourAction.IsRock => 6 + yourAction.Score,
        _ when opponentAction.IsScissors && yourAction.IsPaper => yourAction.Score,
        _ => 0
    };
    
    [Fact]
    public void Part2_ScoreAccordingToStrategyGuideTake2()
    {
        var score = File.ReadLines("Day2/input.txt")
            .Select(line => line.Split(' '))
            .Select(x => CalculateScore(
                opponentAction: new GameAction(char.Parse(x.First())),
                yourAction: SelectAction(
                    new GameAction(char.Parse(x.First()))
                    , char.Parse(x.Last()))))
            .Sum();
        
        _logger.WriteLine("Score according to strategy guide: " + score);
        
    }

    private GameAction SelectAction(GameAction opponentAction, char action) => action switch
    {
        'X' when opponentAction.IsPaper => GameAction.Rock,
        'X' when opponentAction.IsRock => GameAction.Scissors,
        'X' when opponentAction.IsScissors => GameAction.Paper,

        'Y' => opponentAction,

        'Z' when opponentAction.IsPaper => GameAction.Scissors,
        'Z' when opponentAction.IsRock => GameAction.Paper,
        'Z' when opponentAction.IsScissors => GameAction.Rock,
    };
}