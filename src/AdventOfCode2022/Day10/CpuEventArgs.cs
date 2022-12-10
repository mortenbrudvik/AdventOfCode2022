using System;

namespace AdventOfCode2022.Day10;

public class CpuEventArgs : EventArgs
{
    public int X { get; }
    public long Cycle { get; }

    public CpuEventArgs(int x, long cycle)
    {
        X = x;
        Cycle = cycle;
    }
}