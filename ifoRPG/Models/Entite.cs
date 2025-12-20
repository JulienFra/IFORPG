namespace ifoRPG.Models;
public abstract class Entite {
    public string Nom { get; set; }
    public int X { get; set; } = 1;
    public int Y { get; set; } = 1;
}