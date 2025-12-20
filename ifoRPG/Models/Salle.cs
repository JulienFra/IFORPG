namespace ifoRPG.Models;

public class Salle {
    public string Nom { get; set; }
    public Professeur Prof { get; set; }
    public bool EstReussie { get; set; } = false;
    public int PorteRetourX { get; set; } = 5; // Position de la porte pour sortir
    public int PorteRetourY { get; set; } = 7;
}