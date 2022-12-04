namespace AdventOfCode2022.Day2;

public record GameAction
{
    private readonly char _action;

    public GameAction(char action) => _action = action;

    public int Score => _action % (_action < 'D' ? 32 : 29);

    public bool IsRock => _action == 'A' || _action == 'X';
    public bool IsPaper => _action == 'B'  || _action == 'Y';
    public bool IsScissors => _action == 'C'  || _action == 'Z';

    public static GameAction Rock => new('A');
    public static GameAction Paper => new('B');
    public static GameAction Scissors => new('C');
}