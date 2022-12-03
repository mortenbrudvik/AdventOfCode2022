using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2022.Day3;

public record Group
{
    private readonly Lazy<Item> _badge;
    
    public List<Elf> Elves { get; }

    public Group(List<Elf> elves)
    {
        Elves = elves;
        _badge = new Lazy<Item>(Elves[0].Rucksack.Items
            .Intersect(Elves[1].Rucksack.Items)
            .Intersect(Elves[2].Rucksack.Items)
            .Select(item => new Item(item))
            .Single());
    }
    
    public Item Badge => _badge.Value;
}