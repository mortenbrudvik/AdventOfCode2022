namespace AdventOfCode2022.Day3;

public record Item
{
    private readonly char _item;

    public Item(char item) => _item = item;

    public int Priority => char.IsLower(_item)
        ? _item % 32
        : _item % 32 + 26;
}