using System.IO;  
using System.Linq;
using LanguageExt;
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
        var rootFolder = ParseFolders();
        SetTotalFolderSize(rootFolder);
        
        var totalWithSizeBelow = CalculateFolders(rootFolder, 100000);
        
        _logger.WriteLine("Total size of all folders below : 100000 bytes: " + totalWithSizeBelow);
    }

    [Fact]
    public void Part2_MakeRoomForUpdate()
    {
        const long totalDiscSpace = 70000000;
        const long requiredForUpdate = 30000000;
        var rootFolder = ParseFolders();
        var totalUsed = SetTotalFolderSize(rootFolder);
        var needToDelete = requiredForUpdate - (totalDiscSpace - totalUsed);

        Option<Folder> FindDirectoryToDelete(Folder folder)
        {
            if (folder.Size == 0 || folder.Size < needToDelete)
                return null;
            
            var child = folder.Children
                .SelectMany(x => FindDirectoryToDelete(x))
                .OrderBy(f => f.Size).
                FirstOrDefault(f => f.Size > needToDelete);
            
            return folder.Size >= child?.Size ? child : folder;
        }

        var folderToDelete = FindDirectoryToDelete(rootFolder);

        _logger.WriteLine("Disc space");
        _logger.WriteLine("Total: " + totalDiscSpace);
        _logger.WriteLine("Used: " + totalUsed);
        _logger.WriteLine("Free: " + (totalDiscSpace - totalUsed));
        _logger.WriteLine("");
        _logger.WriteLine("To run update");
        _logger.WriteLine("Required: " + requiredForUpdate);
        _logger.WriteLine("Need to delete at least: " + needToDelete);
        folderToDelete.IfSome(x =>
            _logger.WriteLine("Folder to delete: " + x.Name + " (" + x.Size + ")"));
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

    private static Folder ParseFolders()
    {
        var rootFolder = new Folder("/", null);
        var currentFolder = rootFolder;

        foreach (var line in File.ReadLines("Day7/input.txt")
                     .Skip(1)
                     .Select(x => x.Split(' ')))
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
        
        return rootFolder;
    }
}