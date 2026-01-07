namespace ifoRPG.Models;

public class Room
{
    public int RoomNumber { get; }
    public Boss Boss { get; }

    public bool Completed => Boss.Defeated;

    public Room(int roomNumber, Boss boss)
    {
        RoomNumber = roomNumber;
        Boss = boss;
    }
}
