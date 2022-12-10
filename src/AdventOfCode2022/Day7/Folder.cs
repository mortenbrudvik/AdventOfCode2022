using System.Collections.Generic;

namespace AdventOfCode2022.Day7;

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