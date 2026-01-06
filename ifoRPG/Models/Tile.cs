namespace ifoRPG.Models;

public enum TileType { Ground, Window, Bench, PC, Wall } // Wall servira pour les bordures de la salle

public class Tile
{
    public TileType Type { get; }
    // On définit ce qui bloque le passage : Bench, PC et Wall bloquent. Ground et Window non.
    public bool IsWalkable => Type == TileType.Ground || Type == TileType.Window;

    public Tile(TileType type)
    {
        Type = type;
    }
}