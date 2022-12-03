using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
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
        var elves = CreateElves();
        elves.Should().NotBeEmpty();

        var firstElf = elves.First();
        var rucksack = firstElf.Rucksack;
        rucksack.Compartment1
            .Should().NotBeEmpty()
            .And.HaveLength(rucksack.Compartment2.Length);

        var sumOfPriorities = elves.Sum(elf => elf.CalculateItemPriority());
        _logger.WriteLine("Sum of priorities: " + sumOfPriorities);
    }
    
    [Fact]
    public void Part2_ThreeMatchingBadges_CalculateSumOfPriorities()
    {
        var elves = CreateElves();
        var groups = elves.Chunk(3).Select(group => new Group(group.ToList()));

        var sum = groups.Sum(group => group.CalculateBadgePriority());
        _logger.WriteLine("Total Badge priority sum: " + sum);
    }
    
    private static List<Elf> CreateElves() =>
        File.ReadLines("Day3/rucksacks.txt")
            .Select(line =>
                new Elf(
                    new Rucksack(
                        line[..(line.Length / 2)], 
                        line[(line.Length / 2)..]))).ToList();
}