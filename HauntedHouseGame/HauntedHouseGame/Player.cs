using System;
using System.Collections.Generic;

public class Player
{
    public int Health { get; set; }
    public List<string> Inventory { get; set; }

    public Player()
    {
        Health = 100;
        Inventory = new List<string>();
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;

        if (Health < 0)
            Health = 0;

        Console.WriteLine($"You took {amount} damage. Health: {Health}");
    }

    public void AddItem(string item)
    {
        Inventory.Add(item);
        Console.WriteLine($"You picked up: {item}");
    }

    public bool HasItem(string item)
    {
        return Inventory.Contains(item);
    }

    public bool RemoveItem(string item)
    {
        return Inventory.Remove(item);
    }

    public void ShowInventory()
    {
        Console.WriteLine("\nInventory:");
        if (Inventory.Count == 0)
        {
            Console.WriteLine("- Empty");
            return;
        }

        foreach (var i in Inventory)
        {
            Console.WriteLine("- " + i);
        }
    }
}