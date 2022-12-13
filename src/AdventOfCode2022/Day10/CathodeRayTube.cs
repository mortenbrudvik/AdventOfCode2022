using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MoreLinq;
using Xunit;
using Xunit.Abstractions;

using static LanguageExt.Prelude;

namespace AdventOfCode2022.Day10; // EHBZLRJR

public class CathodeRayTube : XunitContextBase
{
    public CathodeRayTube(ITestOutputHelper logger) : base(logger) {}

    [Fact]
    public async Task Part1_CalculateTotalSignalStrength()
    {
        var memory = LoadDataIntoMemory("Day10/input.txt");

        long totalSignalStrength = 0;
        var cpu = new Cpu(memory);
        cpu.Changed += (_, args) =>
        {
            var signalInterval = MoreEnumerable.Generate(20, c => c + 40).Take(10);
            if (signalInterval.Any(x=> x == args.Cycle )) 
                totalSignalStrength += args.Cycle * args.X;
        };

        await WaitUntilProcessingCompletes(memory, cpu);
        
        WriteLine("Total signal strength: " + totalSignalStrength);
    }

    [Fact]
    public async Task Part2_DrawOnCrt()
    {
        var memory = LoadDataIntoMemory("Day10/input.txt");
        var cpu = new Cpu(memory);
        cpu.Changed += (_, args) => { DrawPixel(args.X, args.Cycle); };
        
        await WaitUntilProcessingCompletes(memory, cpu);
    }

    private static async Task WaitUntilProcessingCompletes(Stack<Instruction> memory, Cpu cpu)
    {
        while (memory.Count > 0 || cpu.CurrentInstruction != None)
            await Task.Delay(100);
    }

    private static void DrawPixel(int x, long cycle)
    {
        var spritePos = cycle % 40;
        var pixel = spritePos-1 >= x - 1 && spritePos-1 <= x + 1 ? "#" : ".";

        if(spritePos == 0 )
            XunitContext.WriteLine(pixel);
        else
            XunitContext.Write(pixel);
    }

    private static Stack<Instruction> LoadDataIntoMemory(string filePath) =>
        new(File.ReadLines(filePath)
            .Reverse()
            .Select(l => l.Split(' '))
            .Select(c => new Instruction(c[0], c.Length == 2 ? c[1] : "", c[0] == "noop" ? 1 : 2 )));
}