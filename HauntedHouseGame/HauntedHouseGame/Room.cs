using System;
using System.Collections.Generic;

public class Room
{
    public string Description { get; private set; }

    public int X { get; private set; }
    public int Y { get; private set; }

    public bool Visited { get; set; }

    private Dictionary<string, Room> exits = new Dictionary<string, Room>();

    public Room(string description)
    {
        Description = description;
        Visited = false;
    }

    public void SetPosition(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void LinkRoom(string direction, Room room)
    {
        exits[direction] = room;
    }

    public Room? GetLinkedRoom(string direction)
    {
        if (exits.ContainsKey(direction))
        {
            return exits[direction];
        }

        return null;
    }

    public void ShowRoom()
    {
        Console.WriteLine("\n" + Description);
        Console.WriteLine("\nExits:");

        foreach (var exit in exits)
        {
            Console.WriteLine("- " + exit.Key);
        }
    }
}
