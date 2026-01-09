namespace ifoRPG.Models
{
    // Définition de tous les types de tuiles possibles dans le jeu
    public enum TileType 
    { 
        Ground, 
        Window, 
        Bench, 
        PC, 
        Wall, 
        Board, 
        Flower // Corrigé : Flower au lieu de Flowe
    }

    public class Tile
    {
        public TileType Type { get; }

        // Logique de collision :
        // Le joueur peut marcher sur le sol (Ground), devant les fenêtres (Window)
        // et sur les pots de fleurs (Flower) pour pouvoir trouver l'item caché.
        public bool IsWalkable => Type == TileType.Ground || 
                                  Type == TileType.Window || 
                                  Type == TileType.Flower; 

        public Tile(TileType type)
        {
            Type = type;
        }
    }
}