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
    public void Part1_FindPacketMarker() => 
        _logger.WriteLine("Start of packet marker received at position: " + FindPacketMarkerPosition("Day6/input.txt"));

    [Fact]
    public void Part2_FindMessageMarker() => 
        _logger.WriteLine("Start of message marker received at position: " + FindMessageMarkerPosition("Day6/input.txt"));

    private static int FindPacketMarkerPosition(string filePath) => FindMarkerPosition(filePath, 4); 
    private static int FindMessageMarkerPosition(string filePath) => FindMarkerPosition(filePath, 14);
    
    private static int FindMarkerPosition(string filePath, int length) =>
        File.ReadAllText(filePath)
            .Scan("", (a, b) => a + b)
            .TakeUntil(x => x.Length >= length && x[^length..].Distinct().Length() == x[^length..].Length)
            .Last().Length;}