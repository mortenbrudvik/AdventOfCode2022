using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2022.Day7;

public class NoSpaceLeftOnDevice
{
    private readonly ITestOutputHelper _logger;
    public NoSpaceLeftOnDevice(ITestOutputHelper logger) => _logger = logger;
    
    [Fact]
    public void Part_()
    {
        var lines = File.ReadLines("Day7/input.txt").Skip(1);
        var currentDir = "/";
        var sizes = new Dictionary<string, long> {{currentDir, 0}};

        foreach (var line in lines.Select(x => x.Split(' ')))
        {
            if (line[0] =="$")
            {
                if(line[1] == "ls") 
                    continue;
                
                if (line[1] == "cd" && line[2] == "..")
                {
                    _logger.WriteLine("" + currentDir);
                    currentDir = currentDir.Trim('/').Split('/').SkipLast().Aggregate("/", (a, b) => a + b + "/");
                } 
                
                if (line[1] == "cd" && line[2].All(char.IsLetter)) 
                    currentDir += line[2] + "/"; 
            }
            
            if(line[0] == "dir") sizes.Add(currentDir + line[1] + "/", 0);
            if (line[0].All(char.IsDigit)) sizes[currentDir] += int.Parse(line[0]);

        }
        
        _logger.WriteLine("Sum of file space: " + sizes.Values.Sum());
    }
}


