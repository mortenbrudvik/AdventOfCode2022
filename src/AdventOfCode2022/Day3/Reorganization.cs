using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2022.Day3;

public class Reorganization
{
    private readonly ITestOutputHelper _logger;

    public Reorganization(ITestOutputHelper logger) => _logger = logger;

    [Fact]
    public void CalculateSumOfPriorities()
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
    
    private static List<Elf> CreateElves() =>
        File.ReadLines("Day3/rucksacks.txt")
            .Select(line =>
                new Elf(new Rucksack(
                    line[..(line.Length / 2)], 
                    line[(line.Length / 2)..]))).ToList();
}