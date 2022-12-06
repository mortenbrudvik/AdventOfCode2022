using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

using static MoreLinq.Extensions.TakeUntilExtension;

namespace AdventOfCode2022.Day6;

public class TuningTrouble
{
    private readonly ITestOutputHelper _logger;

    public TuningTrouble(ITestOutputHelper logger) => _logger = logger;

    [Fact]
    public void Part1_FindPacketMarker()
    {
        var markedPosition = File.ReadAllText("Day6/input.txt")
            .Scan("", (a, b) => a + b)
            .TakeUntil(x => 
                x.TakeLast(4).Length() == 4 &&
                x.TakeLast(4).Distinct().Length() == x.TakeLast(4).Length())
            .Last().Length;
        
        _logger.WriteLine("Start of packet marker received at position: " + markedPosition);
    }

    [Fact]
    public void Part2_FindMessageMarker()
    {
        var markedPosition = File.ReadAllText("Day6/input.txt")
            .Scan("", (a, b) => a + b)
            .TakeUntil(x => 
                x.TakeLast(14).Length() == 14 &&
                x.TakeLast(14).Distinct().Length() == x.TakeLast(14).Length())
            .Last().Length;
        
        _logger.WriteLine("Start of message marker received at position: " + markedPosition);
    }
}