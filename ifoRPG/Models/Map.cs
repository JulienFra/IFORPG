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
    public int RoomNumber { get; set; }

    public Map(int width, int height, int floor, int roomNumber = -1, bool isCorridor = false)
    {
        Width = width;
        Height = height;
        FloorNumber = floor;
        Tiles = new Tile[width, height];
        RoomNumber = roomNumber; // On stocke le numéro de salle
        Generate(isCorridor); // On passe l'info ici
    }

    private void Generate(bool isCorridor)
    {
        // 1. Initialisation du sol
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                Tiles[x, y] = new Tile(TileType.Ground);
            }
        }

        // 2. Éléments fixes
        StairsX = Width - 1;
        StairsY = Height - 1;
        ChatGPTX = 8; ChatGPTY = 1; // Position par défaut

        // 3. Placement des meubles par salle UNIQUE
        if (!isCorridor)
        {
            // --- ÉTAGE 1 ---
            if (FloorNumber == 1)
            {
                if (RoomNumber == 1)
                {
                    for (int i = 0; i < Width; i++) { Tiles[i, 0] = new Tile(TileType.Window); }
                    for (int i = 1; i <= 7; i++) { Tiles[i, 3] = new Tile(TileType.PC); }
                    // Ligne verticale droite
                    for (int i = 4; i <= 6; i++) { Tiles[7, i] = new Tile(TileType.PC); }
                    // Ligne horizontale basse
                    for (int i = 1; i <= 7; i++) { Tiles[i, 7] = new Tile(TileType.PC); }

                    BossX = 8; BossY = 5;
                }
                else if (RoomNumber == 2)
                {
                    Tiles[1, 1] = new Tile(TileType.PC);
                    Tiles[8, 1] = new Tile(TileType.PC);
                    Tiles[4, 5] = new Tile(TileType.Bench);
                    BossX = 4; BossY = 6;
                }
                else if (RoomNumber == 3)
                {
                    for (int i = 3; i < 7; i++) Tiles[i, 0] = new Tile(TileType.Window);
                    Tiles[5, 4] = new Tile(TileType.Bench);
                    BossX = 8; BossY = 2;
                }
            }
            // --- ÉTAGE 2 ---
            else if (FloorNumber == 2)
            {
                if (RoomNumber == 1)
                {
                    for (int i = 2; i < 8; i++) Tiles[i, 1] = new Tile(TileType.PC);
                    Tiles[2, 2] = new Tile(TileType.Bench);
                    BossX = 8; BossY = 8;
                }
                else if (RoomNumber == 2)
                {
                    Tiles[0, 2] = new Tile(TileType.Window);
                    Tiles[0, 7] = new Tile(TileType.Window);
                    Tiles[5, 5] = new Tile(TileType.PC);
                    BossX = 2; BossY = 2;
                }
                else if (RoomNumber == 3)
                {
                    Tiles[4, 4] = new Tile(TileType.PC);
                    Tiles[4, 5] = new Tile(TileType.Bench);
                    BossX = 1; BossY = 8;
                }
            }
            // --- ÉTAGE 3 ---
            else if (FloorNumber == 3)
            {
                if (RoomNumber == 1)
                {
                    Tiles[1, 2] = new Tile(TileType.PC);
                    Tiles[3, 2] = new Tile(TileType.PC);
                    Tiles[5, 2] = new Tile(TileType.PC);
                    BossX = 5; BossY = 8;
                }
                else if (RoomNumber == 2)
                {
                    for (int i = 1; i < 9; i++) Tiles[i, 4] = new Tile(TileType.Bench);
                    BossX = 5; BossY = 1;
                }
                else if (RoomNumber == 3)
                {
                    Tiles[2, 2] = new Tile(TileType.PC);
                    Tiles[7, 7] = new Tile(TileType.PC);
                    BossX = 5; BossY = 5;
                }
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