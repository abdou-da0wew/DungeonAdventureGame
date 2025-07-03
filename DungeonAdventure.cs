using System;
using System.Collections.Generic;

// Made By Abdou_da0wew With ❤️
namespace DungeonAdventure
{
    class Program
    {
        static Player player;
        static bool gameOver;
        
        static void Main(string[] args)
        {
            InitializeGame();
            
            Console.WriteLine("🌑🌑🌑 Welcome to the Dark Dungeon of Riddles! 🌑🌑🌑");
            Console.WriteLine("I’ve heard there’s a legendary treasure hidden deep within this dungeon...");
            
            while (!gameOver)
            {
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("1. Explore ahead");
                Console.WriteLine("2. Open your inventory");
                Console.WriteLine("3. Check your health");
                Console.WriteLine("4. Surrender (End the game)");
                
                string input = Console.ReadLine();
                
                switch (input)
                {
                    case "1":
                        ExploreRoom();
                        break;
                    case "2":
                        player.ShowInventory();
                        break;
                    case "3":
                        player.ShowStatus();
                        break;
                    case "4":
                        gameOver = true;
                        Console.WriteLine("You left the dungeon... Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid input, try again.");
                        break;
                }
            }
        }
        
        static void InitializeGame()
        {
            player = new Player("Hero", 100);
            player.AddItem("Wooden Sword", 10);
            player.AddItem("Lamp", 1);
            gameOver = false;
        }
        
        static void ExploreRoom()
        {
            Random rand = new Random();
            int encounter = rand.Next(1, 11);
            
            if (encounter <= 4) // 40% chance to encounter an enemy
            {
                FightEnemy();
            }
            else if (encounter <= 7) // 30% chance to find an empty room
            {
                Console.WriteLine("\nYou found an empty room... Nothing exciting here.");
            }
            else if (encounter <= 9) // 20% chance to find treasure
            {
                FindTreasure();
            }
            else // 10% chance for a puzzle
            {
                SolvePuzzle();
            }
        }
        
        static void FightEnemy()
        {
            string[] enemies = {"Ghoul", "Giant Spider", "Zombie", "Small Vampire"};
            Random rand = new Random();
            string enemy = enemies[rand.Next(enemies.Length)];
            int enemyHealth = rand.Next(20, 51);
            int enemyDamage = rand.Next(5, 16);
            
            Console.WriteLine($"\n⚔️ You encountered a {enemy}! (Health: {enemyHealth}, Damage: {enemyDamage})");
            
            while (enemyHealth > 0 && player.Health > 0)
            {
                Console.WriteLine("\nWhat will you do?");
                Console.WriteLine("1. Attack");
                Console.WriteLine("2. Try to escape (50% chance to succeed)");
                
                string input = Console.ReadLine();
                
                if (input == "1")
                {
                    int damage = rand.Next(5, 21);
                    enemyHealth -= damage;
                    Console.WriteLine($"You hit {enemy} for {damage} damage!");
                    
                    if (enemyHealth > 0)
                    {
                        player.Health -= enemyDamage;
                        Console.WriteLine($"The {enemy} attacked you! (-{enemyDamage} Health)");
                    }
                }
                else if (input == "2")
                {
                    if (rand.Next(2) == 0)
                    {
                        Console.WriteLine("You successfully escaped!");
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Failed to escape!");
                        player.Health -= enemyDamage;
                        Console.WriteLine($"The {enemy} attacked you! (-{enemyDamage} Health)");
                    }
                }
                
                if (player.Health <= 0)
                {
                    Console.WriteLine("💀 You died... Game over!");
                    gameOver = true;
                    return;
                }
            }
            
            if (enemyHealth <= 0)
            {
                int gold = rand.Next(10, 51);
                Console.WriteLine($"🎉 You defeated the {enemy}! You found {gold} gold.");
                player.Gold += gold;
                
                if (rand.Next(4) == 0) // 25% chance for loot drop
                {
                    string[] loot = {"Health Potion", "Iron Sword", "Leather Armor", "Magic Ring"};
                    string item = loot[rand.Next(loot.Length)];
                    Console.WriteLine($"The {enemy} dropped a {item}!");
                    player.AddItem(item, 1);
                }
            }
        }
        
        static void FindTreasure()
        {
            Random rand = new Random();
            int gold = rand.Next(50, 151);
            
            Console.WriteLine("\n💰 You found a treasure chest!");
            Console.WriteLine($"You opened it and found {gold} gold!");
            player.Gold += gold;
            
            if (rand.Next(3) == 0) // 33% chance for extra loot
            {
                string[] treasures = {"Treasure Map", "Gemstone", "Old Crown", "Magic Key"};
                string item = treasures[rand.Next(treasures.Length)];
                Console.WriteLine($"You also found a {item}!");
                player.AddItem(item, 1);
            }
        }
        
        static void SolvePuzzle()
        {
            string[] puzzles = {
                "What walks without legs and cries without eyes?",
                "The more you take, the bigger it gets. What is it?",
                "It’s your brother but not your brother, who is it?"
            };
            
            string[] answers = {"Cloud", "Hole", "Father"};
            
            Random rand = new Random();
            int index = rand.Next(puzzles.Length);
            
            Console.WriteLine("\n🧩 You found a locked door with a riddle:");
            Console.WriteLine(puzzles[index]);
            Console.Write("Your answer: ");
            string answer = Console.ReadLine();
            
            if (answer.ToLower() == answers[index].ToLower())
            {
                int reward = rand.Next(30, 101);
                Console.WriteLine($"🎯 Correct! The door opens and reveals {reward} gold!");
                player.Gold += reward;
            }
            else
            {
                Console.WriteLine("❌ Incorrect answer! The door remains closed.");
            }
        }
    }
    
    class Player
    {
        public string Name { get; }
        public int Health { get; set; }
        public int Gold { get; set; }
        private Dictionary<string, int> Inventory;
        
        public Player(string name, int health)
        {
            Name = name;
            Health = health;
            Gold = 0;
            Inventory = new Dictionary<string, int>();
        }
        
        public void AddItem(string item, int quantity)
        {
            if (Inventory.ContainsKey(item))
            {
                Inventory[item] += quantity;
            }
            else
            {
                Inventory.Add(item, quantity);
            }
        }
        
        public void ShowInventory()
        {
            Console.WriteLine("\n🎒 Your Inventory:");
            if (Inventory.Count == 0)
            {
                Console.WriteLine("Your inventory is empty!");
            }
            else
            {
                foreach (var item in Inventory)
                {
                    Console.WriteLine($"- {item.Key}: {item.Value}");
                }
            }
            Console.WriteLine($"💰 Gold: {Gold}");
        }
        
        public void ShowStatus()
        {
            Console.WriteLine($"\n❤️ {Name}'s Status:");
            Console.WriteLine($"Health: {Health}/100");
        }
    }
}
