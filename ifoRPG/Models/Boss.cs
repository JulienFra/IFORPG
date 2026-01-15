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
        Reset();
    }

    public Question? CurrentQuestion =>
        CurrentQuestionIndex < Questions.Count
            ? Questions[CurrentQuestionIndex]
            : null;

    public bool Answer(int chosenIndex)
    {
        if (CurrentQuestion == null)
            return true;

        if (CurrentQuestion.IsCorrect(chosenIndex))
            CorrectAnswers++;

        CurrentQuestionIndex++;

        if (CurrentQuestionIndex >= Questions.Count)
        {
            int required = Questions.Count == 3 ? 2 : 3;
            Defeated = CorrectAnswers >= required;
            return true;
        }

        return false;
    }

    public void ForceDefeat()
    {
        Defeated = true;
        CurrentQuestionIndex = Questions.Count;
    }

    public void Reset()
    {
        CurrentQuestionIndex = 0;
        CorrectAnswers = 0;
        Defeated = false;
    }
}