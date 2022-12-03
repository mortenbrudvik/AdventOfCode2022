using System;
using System.Linq;

namespace AdventOfCode2022.Day3;

public record Rucksack
{
    private readonly Lazy<Item> _duplicate;
    public string Compartment1 { get; }
    public string Compartment2 { get; }

    public Rucksack(string compartment1, string compartment2)
    {
        Compartment1 = compartment1;
        Compartment2 = compartment2;
        _duplicate = new Lazy<Item>(Compartment1
            .Intersect(Compartment2).Select(item => new Item(item)).Single());
    }
    public string Items => Compartment1 + Compartment2;

    public Item Duplicate => _duplicate.Value;
}