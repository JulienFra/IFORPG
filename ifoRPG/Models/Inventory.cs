namespace ifoRPG.Models;

public class Inventory
{
    public List<Item> Items { get; } = new();

    public void Add(Item item)
    {
        Items.Add(item);
    }

    public bool HasChatGPT()
    {
        return Items.Any(i => i is ChatGPTItem);
    }

    public void UseChatGPT()
    {
        var item = Items.FirstOrDefault(i => i is ChatGPTItem);
        if (item != null)
            Items.Remove(item);
    }

    public bool HasDurum()
    {
        return Items.Any(i => i is DurumItem);
    }

    public DurumItem? GetDurum()
    {
        return Items.FirstOrDefault(i => i is DurumItem) as DurumItem;
    }
}