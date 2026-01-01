namespace ifoRPG.Models;

public class Tile
{
    public bool IsWalkable { get; }

    public Tile(bool isWalkable)
    {
        IsWalkable = isWalkable;
    }
}
