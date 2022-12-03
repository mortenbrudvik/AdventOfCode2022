namespace AdventOfCode2022.Day3;

public record Rucksack(string Compartment1, string Compartment2)
{
    public string Items => Compartment1 + Compartment2;
}