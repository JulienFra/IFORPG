namespace ifoRPG.Models;

public class Map
{
    public int Width { get; }
    public int Height { get; }
    public Tile[,] Tiles { get; }
    public int FloorNumber { get; set; }

    // On change { get; } par { get; private set; } pour pouvoir les modifier dans Generate()
    public int BossX { get; private set; }
    public int BossY { get; private set; }
    public int ChatGPTX { get; private set; }
    public int ChatGPTY { get; private set; }
    public int StairsX { get; private set; }
    public int StairsY { get; private set; }

    public Map(int width, int height, int floor)
    {
        Width = width;
        Height = height;
        FloorNumber = floor;
        Tiles = new Tile[width, height];
        Generate();
    }

    private void Generate()
    {
        // 1. Initialisation du sol
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                Tiles[x, y] = new Tile(TileType.Ground);
            }
        }

        // 2. Design selon l'étage
        switch (FloorNumber)
        {
            case 1:
                Tiles[2, 4] = new Tile(TileType.Window);
                Tiles[3, 2] = new Tile(TileType.Bench);
                Tiles[4, 2] = new Tile(TileType.Bench);
                
                ChatGPTX = 8; ChatGPTY = 1;
                BossX = 5; BossY = 5;
                break;

            case 2:
                for (int i = 2; i < 8; i++)
                {
                    Tiles[i, 1] = new Tile(TileType.PC);
                }
                
                ChatGPTX = 1; ChatGPTY = 5;
                BossX = 8; BossY = 8;
                break;

            default:
                BossX = 7; BossY = 7;
                ChatGPTX = 2; ChatGPTY = 2;
                break;
        }

        // 3. Position de l'escalier
        StairsX = Width - 1;
        StairsY = Height - 1;
    }

    public bool CanMoveTo(int x, int y)
    {
        if (x < 0 || y < 0 || x >= Width || y >= Height)
            return false;

        return Tiles[x, y].IsWalkable;
    }
}