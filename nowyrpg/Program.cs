using System;

public class Info
{
    public static void ShowCharacterStats(Character character)
    {
        Console.WriteLine("==========================");
        Console.WriteLine("Class: {0}", character.Class);
        Console.WriteLine("Health: {0}", character.Health);
        Console.WriteLine("Shield: {0}", character.Shield);
        Console.WriteLine("Attack Power: {0}", character.AttackPower);
        Console.WriteLine("Num Attacks: {0}", character.NumAttacks);
        Console.WriteLine("==========================");
    }
}
public class Creature
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Shield { get; set; }
    public int AttackPower { get; set; }
    public void GiveShield()
    {
        Shield += 20;
        Console.WriteLine("Now you have {0} shield", Shield);
    }
    public void ShowStats()
    {
        Console.WriteLine("Name: {0}", Name);
        Console.WriteLine("Health: {0}", Health);
        Console.WriteLine("Attack Power: {0}", AttackPower);
    }
    public void Suicide()
    {
        Health = 0;
    }
    public virtual void Attack(Creature enemy)
    {
        if (enemy.Shield > 0)
        {
            enemy.Shield -= AttackPower;
        }
        else
        { 
            enemy.Health -= AttackPower;
        }
    }
}

public class Character : Creature
{
    public string Class { get; set; }
    public int NumAttacks { get; set; }
    public int MaxHp { get; set; }

    public Character(string class_, int numAttacks, int shield, int maxhp, int health, int attackPower)
    {
        Class = class_;
        NumAttacks = numAttacks;
        Shield = shield;
        MaxHp = maxhp;
        Health = health;
        AttackPower = attackPower;
    }

    public static void Heal(Character character)
    {
        character.Health = character.MaxHp;
        Console.WriteLine("Now you have max hp");
    }

    public override void Attack(Creature enemy)
    {
        for (int i = 0; i < NumAttacks; i++)
        {
            enemy.Health -= AttackPower;
            Console.WriteLine("You dealt {0} damage to the {1}.", AttackPower, enemy.Name);
            Console.WriteLine("The {1} has {0} health remaining.", enemy.Health, enemy.Name);
            Console.WriteLine("===========================");
        }
    }
}

public class Warrior : Character
{
    public Warrior() : base("Warrior", 2, 0,100, 100, 10)
    {
    }
}

public class Mage : Character
{
    public Mage() : base("Mage", 1, 0, 50, 50, 20)
    {
    }
}

public class Monster : Creature
{
}

public class Game
{
    private Creature player;

    public Game()
    {
        player = null;
    }

    public void Start()
    {

        Console.WriteLine("Choose a character class:");
        Console.WriteLine("(1) Warrior");
        Console.WriteLine("(2) Mage");

        int choice = Convert.ToInt32(Console.ReadLine());

        if (choice == 1)
        {
            player = new Warrior();
        }
        else if (choice == 2)
        {
            player = new Mage();
        }

        while (player.Health > 0)
        {
            Info.ShowCharacterStats((Character)player);

            Monster monster = new Monster
            {
                Name = "Goblin",
                Health = 50,
                AttackPower = 5
            };

            Console.WriteLine("You encounter a {0}.", monster.Name);
            while (player.Health > 0 && monster.Health > 0)
            {
                Console.WriteLine("What do you want to do?");
                Console.WriteLine("(1) Attack");
                Console.WriteLine("(2) Defend");
                Console.WriteLine("(3) Heal");

                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        player.Attack(monster);
                        break;
                    case 2:
                        player.GiveShield();
                        break;
                    case 3:
                        Character.Heal((Character)player);
                        break;
                    case 4:
                        player.Suicide();
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
                monster.Attack(player);
            }
            if (player.Health <= 0)
            {
                Console.WriteLine("You have been defeated.");
                break;
            }
            else if (monster.Health <= 0)
            {
                Console.WriteLine("You have defeated the {0}.", monster.Name);
            }
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Game game = new Game();
        game.Start();
    }
}