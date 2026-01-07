namespace ifoRPG.Models;

public static class BossFactory
{
    public static Boss CreateBoss(int floor, int room)
    {
        return new Boss(
            $"Examen – Étage {floor}, Salle {room}",
            QuestionFactory.CreateQuestions(floor, room)
        );
    }
}
