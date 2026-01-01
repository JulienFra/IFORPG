namespace ifoRPG.Models;

public class Player
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public Player(int startX, int startY)
    {
        X = startX;
        Y = startY;
    }
    public Inventory Inventory { get; } = new();

    public void Move(int dx, int dy, Map map)
    {
        int newX = X + dx;
        int newY = Y + dy;

        if (map.CanMoveTo(newX, newY))
        {
            X = newX;
            Y = newY;
        }
    }
}
