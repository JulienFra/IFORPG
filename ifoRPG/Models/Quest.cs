namespace ifoRPG.Models;

public class Quest
{
    public string Description { get; }
    public bool Completed { get; private set; }

    public Quest(string description)
    {
        Description = description;
        Completed = false;
    }

    public void Complete()
    {
        Completed = true;
    }

    public void Reset()
    {
        Completed = false;
    }
}
