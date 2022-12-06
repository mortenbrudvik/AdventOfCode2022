using System.IO;
using System.Linq;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

using static MoreLinq.Extensions.TakeUntilExtension;

namespace AdventOfCode2022.Day6;

public class TuningTrouble
{
    private readonly ITestOutputHelper _logger;

    public TuningTrouble(ITestOutputHelper logger) => _logger = logger;

    [Fact]
    public void Part1_FindHowManyCharactersNeedToBeProcessed()
    {
        var data = File.ReadAllText("Day6/input.txt");
        
        var markedPosition = FindMarker(data);

        markedPosition.Should().Be(1109);
        
        _logger.WriteLine("Start-of-packet marker received at position: " + markedPosition);
    }

    [Theory]
    [InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 5)]
    [InlineData("nppdvjthqldpwncqszvftbrmjlhg", 6)]
    [InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10)]
    [InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11)]
    public void FindMarker_ShouldFindMarker(string data, int expected)
    {
        FindMarker(data).Should().Be(expected);
    }

    private static int FindMarker(string data) =>
        data.Scan("", (a, b) => a+b)
            .TakeUntil(x => 
                x.TakeLast(4).Length() == 4 &&
                x.TakeLast(4).Distinct().Length() == x.TakeLast(4).Length()).Last().Length;
}
