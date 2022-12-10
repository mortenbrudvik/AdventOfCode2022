using System;
using System.Reactive.Linq;

namespace AdventOfCode2022.Day10;

public static class Circuit
{
    public static IObservable<long> Clock => Observable.Interval(TimeSpan.FromSeconds(1));
}