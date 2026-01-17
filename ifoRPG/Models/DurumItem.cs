namespace ifoRPG.Models;

public class DurumItem : Item
{
    public int HintsRemaining { get; set; } = 3;

    public DurumItem() : base("Durum Stack Overflow") { }

    public bool CanUseHint()
    {
        return HintsRemaining > 0;
    }

    public void UseHint()
    {
        if (HintsRemaining > 0)
            HintsRemaining--;
    }
}