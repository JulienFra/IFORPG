namespace ifoRPG.Models;

public class Floor
{
    public int FloorNumber { get; }
    public List<Room> Rooms { get; }

    public Floor(int floorNumber)
    {
        FloorNumber = floorNumber;
        Rooms = new List<Room>
        {
            new Room(1, BossFactory.CreateBoss(floorNumber, 1)),
            new Room(2, BossFactory.CreateBoss(floorNumber, 2)),
            new Room(3, BossFactory.CreateBoss(floorNumber, 3))
        };
    }

    public bool IsCompleted()
    {
        return Rooms.All(r => r.Completed);
    }
}
