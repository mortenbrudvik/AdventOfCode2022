using System;
using System.IO;
using System.Linq;
using MoreLinq;
using MoreLinq.Extensions;
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
    public void Part2_()
    {
        
    }
}