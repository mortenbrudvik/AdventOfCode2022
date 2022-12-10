using System;
using System.Collections.Generic;
using LanguageExt;

namespace AdventOfCode2022.Day10;

public class Cpu
{
    public Option<Instruction> CurrentInstruction = null;
    private long _instructionCycleStarted;
    public int X { get; private set; } = 1;
    private long _cycle;

    public event EventHandler Changed;

    public override string ToString() => 
        _cycle + " X: " + X + "  " + CurrentInstruction.Match(x => x.Command + " " + x.Args, () => "");

    public Cpu(Stack<Instruction> memory)
    {
        Circuit.Clock.Subscribe(clock =>
        {
            _cycle = clock; 

            CurrentInstruction.IfSome(i =>
            {
                if (i.TimeToComplete + _instructionCycleStarted == clock)
                {
                    if (i.Command == "addx") X += int.Parse(i.Args);
                    CurrentInstruction = null;
                }
            });
                
            CurrentInstruction.IfNone(() =>
            {
                if( memory.Count == 0 ) return;
                    
                CurrentInstruction = memory.Pop();
                _instructionCycleStarted = clock;
            });
                
            Changed?.Invoke(this, EventArgs.Empty);
        });
    }
}