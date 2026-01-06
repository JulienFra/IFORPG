namespace ifoRPG.Models;

public class Boss
{
    public string Name { get; }
    public List<Question> Questions { get; }
    public int CurrentQuestionIndex { get; private set; }
    public int CorrectAnswers { get; private set; }
    public bool Defeated { get; set; }

    public Boss(string name, List<Question> questions)
    {
        Name = name;
        Questions = questions;
        CurrentQuestionIndex = 0;
        CorrectAnswers = 0;
        Defeated = false;
    }

    public Question CurrentQuestion => Questions[CurrentQuestionIndex];

    public bool Answer(int chosenIndex)
    {
        if (CurrentQuestion.IsCorrect(chosenIndex))
            CorrectAnswers++;

        CurrentQuestionIndex++;

        if (CurrentQuestionIndex >= Questions.Count)
        {
            Defeated = CorrectAnswers >= 1; // une seule question
            return true; // fin du boss
        }

        return false; // encore des questions
    }
    public void Reset()
    {
        CurrentQuestionIndex = 0;
        CorrectAnswers = 0;
        Defeated = false;
    }

 public static Boss CreateBossForFloor(int floor)
    {
        return floor switch
        {
            1 => new Boss("M. Durant (POO)", new List<Question> {
new Question("La POO signifie ?", new List<string> {"Programmation Orientée Objet", "Petit Ours Orgueilleux"}, 0)
}),
            2 => new Boss("Mme. Martin (SQL)", new List<Question> {
new Question("Que signifie SQL ?", new List<string> {"Structured Query Language", "Simple Query Language"}, 0)
}),
            _ => new Boss($"Prof Étage {floor}", new List<Question> {
new Question("Prêt pour la suite ?", new List<string> {"Oui", "Non"}, 0)
})
        };
    }

}
