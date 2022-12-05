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
            .Select(CalculateScore)
            .Sum();

        _logger.WriteLine("Score according to strategy guide: " + score);
    }
    
    [Fact]
    public void Part2_ScoreAccordingToStrategyGuideTake2()
    {
        var score = File.ReadLines("Day2/input.txt")
            .Select(strategy => strategy[..2] + strategy switch
            {
                "C Z" or "A Y" or "B X" => "X",
                "A Z" or "B Y" or "C X" => "Y",
                "B Z" or "C Y" or "A X" => "Z"
             })
            .Select(CalculateScore)
            .Sum();
        
        _logger.WriteLine("Score according to strategy guide: " + score);
    }

    private static int CalculateScore(string strategy) =>
        strategy[^1] % 29 +
        strategy switch
        {
            "A Y" or "B Z" or "C X" => 6,
            "A X" or "B Y" or "C Z" => 3,
            _ => 0
        };

}

