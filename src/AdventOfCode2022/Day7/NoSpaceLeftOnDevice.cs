using System.Collections.Generic;  
using System.IO;  
using System.Linq;
using FluentAssertions;
using Xunit;  
using Xunit.Abstractions;  
  
namespace AdventOfCode2022.Day7;

public class NoSpaceLeftOnDevice
{
    private readonly ITestOutputHelper _logger;
    public NoSpaceLeftOnDevice(ITestOutputHelper logger) => _logger = logger;

    [Fact]
    public void Part1_CalculateSumOfFoldersBelowAGivenSize()
    {
        var lines = File.ReadLines("Day7/input.txt").Skip(1);
        var rootFolder = new Folder("/", null);
        var currentFolder = rootFolder;

        foreach (var line in lines.Select(x => x.Split(' ')))
        {
            if (line[0] == "$")
            {
                currentFolder = line[1] switch
                {
                    "cd" when line[2] == ".." => currentFolder.Parent!,
                    "cd" when line[2].All(char.IsLetter) => currentFolder.Children.Single(x => x.Name == line[2]),
                    _ => currentFolder
                };
            }

            if (line[0] == "dir")
                currentFolder.Children.Add(new Folder(line[1], currentFolder));
            else if (line[0].All(char.IsDigit))
                currentFolder.Size += long.Parse(line[0]);
        }

        var total = SetTotalFolderSize(rootFolder);
        var totalWithSizeBelow = CalculateFolders(rootFolder, 100000);
        
        _logger.WriteLine("Total size of all folders: " + total);
        _logger.WriteLine("Total size of all folders below : 100000 bytes: " + totalWithSizeBelow);

        totalWithSizeBelow.Should().Be(1325919);
    }

    private static long SetTotalFolderSize(Folder folder)
    {
        folder.Size += folder.Children.Sum(SetTotalFolderSize);
        return folder.Size;
    }

    private static long CalculateFolders(Folder folder, long withSizeBelow)
    {
        var size = folder.Children.Sum(x => CalculateFolders(x, withSizeBelow));
        return folder.Size <= withSizeBelow ? folder.Size + size : size;
    }

    public class Folder
    {
        public Folder(string name, Folder? parent)
        {
            Name = name;
            Parent = parent;
        }

        public string Name { get; }
        public List<Folder> Children { get; } = new();
        public Folder? Parent { get; }
        public long Size { get; set; }
    }
}