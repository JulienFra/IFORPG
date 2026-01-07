namespace ifoRPG.Models;

public static class QuestionFactory
{
    public static List<Question> CreateQuestions(int floor, int room)
    {
        return new List<Question>
        {
            new Question("Question 1 ?", new() { "Bonne", "Faux" }, 0),
            new Question("Question 2 ?", new() { "Bonne", "Faux" }, 0),
            new Question("Question 3 ?", new() { "Bonne", "Faux" }, 0),
            new Question("Question 4 ?", new() { "Bonne", "Faux" }, 0),
            new Question("Question 5 ?", new() { "Bonne", "Faux" }, 0),
        };
    }
}
