namespace ifoRPG.Models;

public class Player
{
    public int X { get; set; }
    public int Y { get; set; }


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
