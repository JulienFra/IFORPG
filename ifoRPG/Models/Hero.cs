namespace ifoRPG.Models;
public class Hero : Entite {
    public string ClasseScolaire { get; set; }
    public List<Objet> Inventaire { get; set; } = new();
    public int CoursValides { get; set; } = 0;

    public void Deplacer(string touche, int maxX, int maxY) {
        if (touche == "z" && Y > 0) Y--;
        if (touche == "s" && Y < maxY - 1) Y++;
        if (touche == "q" && X > 0) X--;
        if (touche == "d" && X < maxX - 1) X++;
    }
}