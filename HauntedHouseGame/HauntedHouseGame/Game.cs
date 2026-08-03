using System;
using System.Collections.Generic;

public class Game
{
    private Player player;
    private Room currentRoom;
    private Room ghostRoom;
    private MapRenderer map;
    private GameData gameData;
    private Random random;

    private bool isRunning;
    private bool exitLocked = true;

    public Game()
    {
        gameData = new GameData();

        player = new Player();
        currentRoom = gameData.GetStartingRoom();
        ghostRoom = gameData.Basement;
        random = new Random();

        map = new MapRenderer(gameData.AllRooms());

        isRunning = true;
    }

    public void Start()
    {
        ShowIntro();

        while (isRunning)
        {
            currentRoom.Visited = true;

            ShowStatus();
            currentRoom.ShowRoom();

            map.Draw(currentRoom);

            Console.Write("\n> ");
            string input = Console.ReadLine()?.ToLower().Trim() ?? "";

            HandleInput(input);

            if (!isRunning)
            {
                break;
            }

            MoveGhost();
            CheckGhostAttack();
            CheckWinCondition();
            CheckLoseCondition();
        }
    }

    private void HandleInput(string input)
    {
        if (input == "help" || input == "commands")
        {
            ShowHelp();
        }
        else if (input == "story")
        {
            ShowStory();
        }
        else if (input == "map")
        {
            map.Draw(currentRoom);
        }
        else if (input == "look")
        {
            currentRoom.ShowRoom();
        }
        else if (input == "inventory")
        {
            player.ShowInventory();
        }
        else if (input == "take key")
        {
            if (currentRoom == gameData.Kitchen && gameData.KitchenHasKey)
            {
                Console.WriteLine("You found a rusty KEY in the kitchen!");
                player.AddItem("key");
                gameData.KitchenHasKey = false;
            }
            else
            {
                Console.WriteLine("Nothing to take here.");
            }
        }
        else if (input == "use key")
        {
            if (currentRoom == gameData.Exit && player.HasItem("key"))
            {
                Console.WriteLine("You unlock the door...");
                exitLocked = false;
                player.RemoveItem("key");
            }
            else
            {
                Console.WriteLine("Nothing happens.");
            }
        }
        else if (input.StartsWith("go "))
        {
            Move(input.Substring(3));
        }
        else if (input == "quit")
        {
            isRunning = false;
        }
        else
        {
            Console.WriteLine("The house does not understand. Type 'help' to steady your thoughts.");
        }
    }

    private void Move(string direction)
    {
        Room? next = currentRoom.GetLinkedRoom(direction);

        if (next == null)
        {
            Console.WriteLine("You press forward, but the darkness gives you no path that way.");
            return;
        }

        currentRoom = next;
    }

    private void CheckWinCondition()
    {
        if (currentRoom == gameData.Exit)
        {
            if (!exitLocked)
            {
                Console.WriteLine("\nTHE DOOR OPENS...");
                Console.WriteLine("YOU ESCAPED!");
                isRunning = false;
            }
            else
            {
                Console.WriteLine("The door is locked. You need a key.");
            }
        }
    }

    private void CheckLoseCondition()
    {
        if (player.Health <= 0)
        {
            Console.WriteLine("You died...");
            isRunning = false;
        }
    }

    private void ShowStatus()
    {
        Console.WriteLine($"\nHealth: {player.Health}");
        Console.WriteLine($"Location: {currentRoom.Description}");
    }

    private void MoveGhost()
    {
        if (random.Next(100) < 35)
        {
            List<Room> rooms = gameData.AllRooms();
            ghostRoom = rooms[random.Next(rooms.Count)];
        }
    }

    private void CheckGhostAttack()
    {
        if (currentRoom == ghostRoom)
        {
            Console.WriteLine("\nThe air turns ice-cold.");
            Console.WriteLine("A ghost lunges from the dark!");
            player.TakeDamage(20);
        }
    }

    private void ShowIntro()
    {
        Console.Clear();
        Console.WriteLine("=================================");
        Console.WriteLine("       HAUNTED HOUSE ESCAPE      ");
        Console.WriteLine("=================================");
        Console.WriteLine("Rain claws at the windows.");
        Console.WriteLine("Somewhere above you, slow footsteps cross an empty room.");
        Console.WriteLine("The front door has vanished behind peeling wallpaper.");
        Console.WriteLine("A cold thought settles in your chest:");
        Console.WriteLine("find the key, unlock the exit, and leave before the house wakes fully.");
        Console.WriteLine("\nType 'help' or 'commands' if fear makes you forget what to do.");
        Console.WriteLine("=================================\n");
    }

    private void ShowStory()
    {
        Console.WriteLine("\nYou remember only flashes:");
        Console.WriteLine("- a locked gate in the rain");
        Console.WriteLine("- a whisper calling your name");
        Console.WriteLine("- a rusted key scraping across a kitchen floor");
        Console.WriteLine("\nThe house is old, hungry, and listening.");
    }

    private void ShowHelp()
    {
        Console.WriteLine("\n=== HELP ===");
        Console.WriteLine("Goal:");
        Console.WriteLine("- Find the key hidden in the kitchen.");
        Console.WriteLine("- Reach the locked exit.");
        Console.WriteLine("- Use the key before your health reaches 0.");
        Console.WriteLine("- Avoid the ghost moving through the house.");

        Console.WriteLine("\nMovement:");
        Console.WriteLine("- go north");
        Console.WriteLine("- go south");
        Console.WriteLine("- go east");
        Console.WriteLine("- go west");
        Console.WriteLine("- go up");
        Console.WriteLine("- go down");

        Console.WriteLine("\nActions:");
        Console.WriteLine("- look       : describe the current room again");
        Console.WriteLine("- map        : show the house map");
        Console.WriteLine("- take key   : pick up the key if it is here");
        Console.WriteLine("- use key    : unlock the exit if you are there");
        Console.WriteLine("- inventory  : show your items");
        Console.WriteLine("- story      : remember why you are here");
        Console.WriteLine("- quit       : leave the game");
        Console.WriteLine("============");
    }
}
