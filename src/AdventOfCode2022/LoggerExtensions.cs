using System.Collections.Generic;
using Xunit.Abstractions;

namespace AdventOfCode2022;

public static class LoggerExtensions
{
    public static void LogResult<T>(this IEnumerable<T> data, ITestOutputHelper logger) =>
        logger.WriteLine("" + string.Join(',', data));

}