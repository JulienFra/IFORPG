namespace ifoRPG.Models;

public abstract class Item
{
    public string Name { get; }

    protected Item(string name)
    {
        Name = name;
    }
}
