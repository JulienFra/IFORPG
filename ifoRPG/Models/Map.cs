namespace ifoRPG.Models;

public class Map
{
    public int Width { get; }
    public int Height { get; }
    public Tile[,] Tiles { get; }
    public int FloorNumber { get; set; }

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
        RoomNumber = roomNumber;
        Generate(isCorridor); 
    }

    private void Generate(bool isCorridor)
    {
        // 1. Initialisation du sol pour toute la grille par défaut
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                Tiles[x, y] = new Tile(TileType.Ground);
            }
        }

        // 2. Définition des coordonnées importantes
        StairsX = Width - 1; // 9
        StairsY = Height - 1; // 9
        ChatGPTX = Width - 1; // 9 (pour la fleur en haut à droite)
        ChatGPTY = 0;

        // 3. Remplissage selon si c'est un couloir ou une salle
        if (isCorridor)
        {
            // --- LOGIQUE DU COULOIR ---
            
            // Fleur en haut à droite (Loot ChatGPT)
            Tiles[ChatGPTX, ChatGPTY] = new Tile(TileType.Flower);
            
            // Fleur en bas à gauche (Déco)
            Tiles[0, Height - 1] = new Tile(TileType.Flower);

            // IMPORTANT : L'escalier est géré visuellement par le CSS dans Game.razor 
            // via les variables StairsX/Y, donc on laisse le sol (Ground) ici.
        }
        else
        {
            // --- TOUTES LES SALLES IDENTIQUES (Design Salle 1) ---
            
            // Fenêtres en haut
            for (int i = 0; i < Width; i++) { Tiles[i, 0] = new Tile(TileType.Window); }
            
            // Bureau en U (PC)
            for (int i = 1; i <= 7; i++) { Tiles[i, 3] = new Tile(TileType.PC); }
            for (int i = 4; i <= 6; i++) { Tiles[7, i] = new Tile(TileType.PC); }
            for (int i = 1; i <= 7; i++) { Tiles[i, 7] = new Tile(TileType.PC); }

            // Position du Boss
            BossX = 8; 
            BossY = 5;
        }
    }

    public bool CanMoveTo(int x, int y)
    {
        if (x < 0 || y < 0 || x >= Width || y >= Height)
            return false;

        // Vérifie si la tuile autorise le passage (IsWalkable)
        return Tiles[x, y].IsWalkable;
    }
}