using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2022.Day3;

public record Group
{
    private readonly Lazy<Badge> _badge;
    
    public List<Elf> Elves { get; }

    public Group(List<Elf> elves)
    {
        Elves = elves;
        _badge = new Lazy<Badge>(Elves[0].Rucksack.Items
            .Intersect(Elves[1].Rucksack.Items)
            .Intersect(Elves[2].Rucksack.Items)
            .Select(item => new Badge(item))
            .Single());
    }
    
    public Badge Badge => _badge.Value;

    public int CalculateBadgePriority() =>
        char.IsLower(Badge.Item)
            ? Badge.Item % 32
            : Badge.Item % 32 + 26;
}