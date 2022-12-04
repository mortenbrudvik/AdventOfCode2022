using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using MoreLinq;
using MoreLinq.Extensions;
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
        var numberOfContained = File.ReadLines("Day4/cleanup-list.txt")
            .Select(line => line.Split(',')
                .Select(pair => pair.Split('-').Select(int.Parse))
                .Select(pair => Enumerable.Range(pair.First(), pair.Last()-pair.First()+1)))
            .Count(twoElves =>                 
                !twoElves.First().Except(twoElves.Last()).Any() ||
                !twoElves.Last().Except(twoElves.First()).Any());
        
        _logger.WriteLine("Number of contained teams: " + numberOfContained);
    }
}