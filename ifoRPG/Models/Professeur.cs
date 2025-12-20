namespace ifoRPG.Models;

public class Professeur : Entite {
    public string Matiere { get; set; }
    // Assure-toi que le nom est exactement "QuestionQCM"
    public string QuestionQCM { get; set; } 
    // Assure-toi que le nom est exactement "Options"
    public List<string> Options { get; set; } = new(); 
    public int IndexCorrect { get; set; }
}