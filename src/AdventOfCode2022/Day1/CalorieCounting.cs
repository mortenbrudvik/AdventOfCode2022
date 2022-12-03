using System.IO;
using System.Linq;
using MoreLinq.Extensions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2022.Day1;

public class CalorieCounting
{
    private readonly ITestOutputHelper _logger;

    public CalorieCounting(ITestOutputHelper logger) => _logger = logger;

    [Fact]
    public void Part1_FindElfCarryingTheMostCalories()
    {
        var mostCalories = File.ReadLines("day1/elves-food-calories.txt")
            .Split(string.IsNullOrWhiteSpace)
            .Max(calories => calories.Sum(int.Parse));

        _logger.WriteLine("Most calories: "+ mostCalories);
    }
}