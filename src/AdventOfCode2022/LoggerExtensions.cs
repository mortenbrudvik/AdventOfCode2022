using System.Collections.Generic;
using Xunit;

namespace AdventOfCode2022;

public static class LoggerExtensions
{
    public static void LogResult<T>(this IEnumerable<T> data) =>
        XunitContext.WriteLine("" + string.Join(',', data));
}