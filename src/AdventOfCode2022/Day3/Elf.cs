using System.Linq;

namespace AdventOfCode2022.Day3;

public record Elf(Rucksack Rucksack)
{
    public int CalculateItemPriority() => Rucksack.Compartment1
        .Intersect(Rucksack.Compartment2)
        .Select(item => 
            char.IsLower(item) 
                ? item % 32 
                : item % 32 + 26).
        Single();
}