using System;
using System.Collections.Generic;
using LanguageExt;

namespace AdventOfCode2022.Day10;

public class Cpu
{
    public Option<Instruction> CurrentInstruction = null;
    private long _instructionCycleStarted;
    public int X { get; private set; } = 1;
    public long Cycle;

    public event EventHandler<CpuEventArgs> Changed;

    public override string ToString() => 
        Cycle + " X: " + X + "  " + CurrentInstruction.Match(x => x.Command + " " + x.Args, () => "");

    public Cpu(Stack<Instruction> memory)
    {
        Circuit.Clock.Subscribe(clock =>
        {
            Cycle = clock+1; 


            CurrentInstruction.IfSome(i =>
            {
                if (i.TimeToComplete + _instructionCycleStarted == Cycle)
                {
                    if (i.Command == "addx") X += int.Parse(i.Args);
                    CurrentInstruction = null;
                }
            });

            CurrentInstruction.IfNone(() =>
            {
                if( memory.Count == 0 ) return;
                    
                CurrentInstruction = memory.Pop();
                _instructionCycleStarted = Cycle;
            });
                
            Changed?.Invoke(this, new CpuEventArgs(X, Cycle));
        });
    }
}