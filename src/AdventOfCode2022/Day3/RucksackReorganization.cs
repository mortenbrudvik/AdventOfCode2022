using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2022.Day3;

public class RucksackReorganization
{
    private readonly ITestOutputHelper _logger;

    public RucksackReorganization(ITestOutputHelper logger) => _logger = logger;

    [Fact]
    public void Part1_TwoMatchingItems_CalculateSumOfPriorities()
    {
        var sumOfPriorities = File.ReadLines("Day3/input.txt")
            .Select(line => (line[..(line.Length / 2)], line[(line.Length / 2)..]))
            .Select(comp => comp.Item1.Intersect(comp.Item2).Single())
            .Select(item => char.IsLower(item) ? item % 32 : item % 32 + 26)    
            .Sum();

        _logger.WriteLine("Sum of priorities: " + sumOfPriorities);
    }
    
    [Fact]
    public void Part2_ThreeMatchingBadges_CalculateSumOfPriorities()
    {
        var sumOfPriorities = File.ReadLines("Day3/input.txt")
            .Chunk(3)
            .Select(group => group[0].Intersect(group[1]).Intersect(group[2]).Single())
            .Select(item => char.IsLower(item) ? item % 32 : item % 32 + 26)
            .Sum();
        
        _logger.WriteLine("Total Badge priority sum: " + sumOfPriorities);
    }
}