namespace ifoRPG.Models;

public class Map
{
    public int Width { get; }
    public int Height { get; }
    public Tile[,] Tiles { get; }
    public int BossX { get; } = 5;
    public int BossY { get; } = 5;
    public int StairsX => Width - 1;
    public int StairsY => Height - 1;
    public int ChatGPTX => 2;
    public int ChatGPTY => 2;
    public Map(int width, int height)
    {
        Width = width;
        Height = height;
        Tiles = new Tile[width, height];

        Generate();
    }

    private void Generate()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                Tiles[x, y] = new Tile(true); // tout est marchable
            }
        }
    }

    public bool CanMoveTo(int x, int y)
    {
        if (x < 0 || y < 0 || x >= Width || y >= Height)
            return false;

        return Tiles[x, y].IsWalkable;
    }
}
