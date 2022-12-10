using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MoreLinq;
using Xunit;
using Xunit.Abstractions;

using static LanguageExt.Prelude;

namespace AdventOfCode2022.Day10;

public class CathodeRayTube
{
    private readonly ITestOutputHelper _logger;

    public CathodeRayTube(ITestOutputHelper logger) => _logger = logger;

    [Fact]
    public async Task Part1_CalculateTotalSignalStrength()
    {
        var memory = new Stack<Instruction>(File.ReadLines("Day10/input.txt")
            .Reverse()
            .Select(l => l.Split(' '))
            .Select(c => new Instruction(c[0], c.Length == 2 ? c[1] : "", c[0] == "noop" ? 1 : 2 )));

        long totalSignalStrength = 0;
        var cpu = new Cpu(memory);
        cpu.Changed += (_, args) =>
        {
            var signalInterval = MoreEnumerable.Generate(20, c => c + 40).Take(10);
            if (signalInterval.Any(x=> x == args.Cycle )) 
                totalSignalStrength += args.Cycle * args.X;
        };

        while (memory.Count > 0 || cpu.CurrentInstruction != None)
            await Task.Delay(100);
        
        _logger.WriteLine("Total signal strength: " + totalSignalStrength);
    }
}