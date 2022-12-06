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
    public void Part1_RearrangeCargo()
    {
        var (moves, stacks) = ExtractCargoData();
 
        var Move = (int number, int from, int to) =>
        {
            for (var i = 0; i < number; i++)
            {
                var item = stacks[from - 1].Pop();
                stacks[to - 1].Push(item);
            }
        };
        
        moves.ForEach(move => Move(move[0], move[1], move[2]));
        
        var topmostCrates = stacks.Select(x => x.Pop())
            .Aggregate((a,b) => a + b);
        
        _logger.WriteLine("Topmost crates after rearrangement: " + topmostCrates);
    }

    [Fact]
    public void Part2_RearrangeCargoMoreEffiently()
    {
        var (moves, stacks) = ExtractCargoData();
        
        var Move = (int number, int from, int to) =>
        {
            var tempStack = new Stack<string>();
            for (var i = 0; i < number; i++)
            {
                var item = stacks[from - 1].Pop();
                tempStack.Push(item);    
            }
            tempStack.ToList().ForEach(x => stacks[to - 1].Push(tempStack.Pop()));
        };
        
        moves.ForEach(move => Move(move[0], move[1], move[2]));
        
        var topmostCrates = stacks.Select(x => x.Pop())
            .Aggregate((a,b) => a + b);
        
        _logger.WriteLine("Topmost crates after rearrangement: " + topmostCrates);
    }
    
    private (List<List<int>> moves, List<Stack<string>> cargoStacks) ExtractCargoData()
    {
        var parts = File.ReadLines("Day5/input.txt")
            .Split(string.IsNullOrWhiteSpace).ToList();
        
        var moves = parts[1]
            .Select(move => move.Split(' ').Where(x => char.IsNumber(x[0])))
            .Select(x => x).Select(move => move.Select(int.Parse).ToList()).ToList();

        var cargoStacks = parts[0]
            .Select(row => row.Chunk(4)
                .Select(x => new string(x).Trim()))
            .SkipLast().Transpose()
            .Select(x=> x.SkipWhile(string.IsNullOrWhiteSpace).Select(x => x.Trim('[').Trim(']')))
            .Select(x => new Stack<string>(x.Reverse())).ToList();

        return (moves, cargoStacks);
    }
}