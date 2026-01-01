namespace ifoRPG.Models;

public class Question
{
    public string Text { get; }
    public List<string> Answers { get; }
    public int CorrectIndex { get; }

    public Question(string text, List<string> answers, int correctIndex)
    {
        Text = text;
        Answers = answers;
        CorrectIndex = correctIndex;
    }

    public bool IsCorrect(int chosenIndex)
    {
        return chosenIndex == CorrectIndex;
    }
}
