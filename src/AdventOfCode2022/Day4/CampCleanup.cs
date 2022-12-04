using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2022.Day4;

public class CampCleanup
{
    private readonly ITestOutputHelper _logger;

    public CampCleanup(ITestOutputHelper logger) => _logger = logger;

    [Fact]
    public void Part1_CountFullyContainedPairs()
    {
        var numberOfContained = GetTeams()
            .Count(x =>                 
                x.First().IsSubsetOf(x.Last()) ||
                x.Last().IsSubsetOf(x.First()));
        
        _logger.WriteLine("Number of contained teams: " + numberOfContained);
    }

    [Fact]
    public void Part2_CountNumberOfPairsThatOverlap()
    {
        var numberOfOverlapping = GetTeams()
            .Count(x => x.First().Overlaps(x.Last()));

        _logger.WriteLine("Number of overlapping teams: " + numberOfOverlapping);
    }
    
    private IEnumerable<IEnumerable<HashSet<int>>> GetTeams() =>
        File.ReadLines("Day4/cleanup-list.txt")
            .Select(line => line.Split(',')
                .Select(pair => pair.Split('-').Select(int.Parse))
                .Select(pair => new HashSet<int>(
                    Enumerable.Range(pair.First(), pair.Last()-pair.First()+1))));
}