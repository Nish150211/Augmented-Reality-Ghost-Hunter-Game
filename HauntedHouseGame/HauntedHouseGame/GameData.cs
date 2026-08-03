using System.Collections.Generic;

public class GameData
{
    public Room Kitchen { get; private set; }
    public Room Basement { get; private set; }
    public Room Exit { get; private set; }

    private Room entrance;
    private Room hallway;

    public bool KitchenHasKey { get; set; } = true;

    public GameData()
    {
        entrance = new Room("Entrance of a haunted house.");
        hallway = new Room("A dark dusty hallway.");

        Kitchen = new Room("A rotten kitchen. Something shiny glows under debris...");
        Basement = new Room("A freezing basement filled with whispers...");
        Exit = new Room("A heavy locked door leading outside.");

        // positions
        entrance.SetPosition(0, 0);
        hallway.SetPosition(0, 1);
        Kitchen.SetPosition(1, 1);
        Basement.SetPosition(0, -1);
        Exit.SetPosition(0, 2);

        // links
        entrance.LinkRoom("north", hallway);

        hallway.LinkRoom("south", entrance);
        hallway.LinkRoom("east", Kitchen);
        hallway.LinkRoom("down", Basement);
        hallway.LinkRoom("north", Exit);

        Kitchen.LinkRoom("west", hallway);
        Basement.LinkRoom("up", hallway);
        Exit.LinkRoom("south", hallway);
    }

    public Room GetStartingRoom()
    {
        return entrance;
    }

    public List<Room> AllRooms()
    {
        return new List<Room>
        {
            entrance,
            hallway,
            Kitchen,
            Basement,
            Exit
        };
    }
}