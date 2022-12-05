using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoreLinq;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2022.Day5;

public class SupplyStacks
{
    private readonly ITestOutputHelper _logger;

    public SupplyStacks(ITestOutputHelper logger) => _logger = logger;

    [Fact]
    public void Part1_()
    {
        var lines = File.ReadLines("Day5/input.txt");
        var parts = lines.Split(string.IsNullOrWhiteSpace).ToList();
        var cargoHold = parts[0].ToList();
        var moves = parts[1];

        var stacks = cargoHold
            .Select(row => row.Chunk(4)
                .Select(x => new string(x).Trim()))
            .SkipLast()
            .Transpose()
            .Select(x=> 
                x.SkipWhile(string.IsNullOrWhiteSpace));
            
        ShowCargoHold(cargoHold);
    }
    
    private void ShowCargoHold(List<string> cargoHold) => cargoHold
            .Select(row => 
                row.Chunk(4)
                    .Select(x => new string(x).Trim()))
            .Select(x=> 
                x.Select(y => y.Length() == 0 ? "   " : y[0] == '[' ? y : " " + y + " "))
            .ForEach(x => 
                _logger.WriteLine("" + string.Join(' ', x)));
}