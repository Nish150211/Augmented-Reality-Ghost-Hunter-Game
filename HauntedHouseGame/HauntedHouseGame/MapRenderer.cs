using System;
using System.Collections.Generic;
using System.Linq;

public class MapRenderer
{
    private List<Room> rooms;

    public MapRenderer(List<Room> rooms)
    {
        this.rooms = rooms;
    }

    public void Draw(Room currentRoom)
    {
        Console.WriteLine("\n===== MAP =====\n");

        int minX = rooms.Min(room => room.X);
        int maxX = rooms.Max(room => room.X);
        int minY = rooms.Min(room => room.Y);
        int maxY = rooms.Max(room => room.Y);

        for (int y = maxY; y >= minY; y--)
        {
            for (int x = minX; x <= maxX; x++)
            {
                Room? room = rooms.FirstOrDefault(room => room.X == x && room.Y == y);
                Console.Write(GetSymbol(room, currentRoom));
            }

            Console.WriteLine();
        }

        Console.WriteLine("\nP = Player | * = Visited | ? = Unknown");
        Console.WriteLine("===============\n");
    }

    private string GetSymbol(Room? room, Room currentRoom)
    {
        if (room == null)
        {
            return "   ";
        }

        if (room == currentRoom)
        {
            return "[P]";
        }

        if (room.Visited)
        {
            return "[*]";
        }

        return "[?]";
    }
}
