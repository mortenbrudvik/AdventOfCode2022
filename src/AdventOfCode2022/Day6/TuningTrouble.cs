using System.IO;
using System.Linq;
using FluentAssertions;
using LanguageExt;
using Xunit;
using Xunit.Abstractions;

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
    public void FindMarker_ShouldFindMarker(string datastream, long exptected)
    {
        FindMarker(datastream).Should().Be(exptected);
    }

    private static long FindMarker(string data)
    {
        var chars = new Seq<char>();

        for (var i = 0; i < data.Length; i++)
        {
            chars = chars.TakeLast(3).ToSeq().Add(data[i]);

            if (chars.Length == 4 && chars.Distinct().Count() == chars.Count)
                return i + 1;
        }

        return -1;
    }
}
