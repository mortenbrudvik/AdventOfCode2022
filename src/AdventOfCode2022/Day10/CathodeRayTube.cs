using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

using static LanguageExt.Prelude;

namespace AdventOfCode2022.Day10;

public class CathodeRayTube
{
    private readonly ITestOutputHelper _logger;

    public CathodeRayTube(ITestOutputHelper logger) => _logger = logger;
    
    [Theory]
    //[InlineData("Day10/input.txt", -1)]
    [InlineData("Day10/sample.txt", -1)]
    public async Task Part1(string input, int expected)
    {
        var memory = new Stack<Instruction>(File.ReadLines(input)
            .Reverse()
            .Select(l => l.Split(' '))
            .Select(c => new Instruction(c[0], c.Length == 2 ? c[1] : "", c[0] == "noop" ? 1 : 2 )));
            
        var cpu = new Cpu(memory);
        cpu.Changed += (_, _) => _logger.WriteLine("" + cpu);

        while (memory.Count > 0 || cpu.CurrentInstruction != None)
            await Task.Delay(100);
    }
}