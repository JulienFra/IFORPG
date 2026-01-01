namespace ifoRPG.Models;

public class Boss
{
    public string Name { get; }
    public List<Question> Questions { get; }
    public int CurrentQuestionIndex { get; private set; }
    public int CorrectAnswers { get; private set; }
    public bool Defeated { get; private set; }

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
            Defeated = CorrectAnswers >= 2; // 2 bonnes réponses sur 3
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

}
