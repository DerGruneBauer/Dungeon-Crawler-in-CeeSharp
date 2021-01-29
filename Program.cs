using System;
using System.Collections.Generic;

namespace Dungeon_Crawler
{
    class Program
    {
        static void Main(string[] args)
        {
            //CHARACTER SETUP
            Console.WriteLine("Please enter the names of 6 characters:");
            Console.Write("Character 1: ");
            string character1 = Console.ReadLine();
            Console.Write("Character 2: ");
            string character2 = Console.ReadLine();
            Console.Write("Character 3: ");
            string character3 = Console.ReadLine();
            Console.Write("Character 4: ");
            string character4 = Console.ReadLine();
            Console.Write("Character 5: ");
            string character5 = Console.ReadLine();
            Console.Write("Character 6: ");
            string character6 = Console.ReadLine();


            //ENEMY SETUP
            //collection of enemies name, min and max damage
            List<Enemy> enemyList = new List<Enemy>();
            enemyList.Add(new Enemy("Ghoul", 5, 7));
            enemyList.Add(new Enemy("Orc", 15, 22));
            enemyList.Add(new Enemy("Goblin", 8, 15));
            enemyList.Add(new Enemy("Beast", 10, 17));
            enemyList.Add(new Enemy("Elemental", 17, 20));
            enemyList.Add(new Enemy("Demon", 15, 18));
            enemyList.Add(new Enemy("Balrog", 15, 28));
            enemyList.Add(new Enemy("Cultist", 8, 12));

            //pushes 100 enemys into the enemy stack
            //Stack to hold random enemy deck
            Stack<string> enemyDeck = new Stack<string>();
            for (int i = 0; i < 100; i++)
            {
                Random rnd = new Random();
                int rndNum = rnd.Next(8);
                enemyDeck.Push(enemyList[rndNum].Name);
            }

            List<Character> characterList = new List<Character>();
            characterList.Add(new Character(character1, "Library", "Orc", 25, 45, 0));
            characterList.Add(new Character(character2, "Crypt", "Balrog", 40, 55, 0));
            characterList.Add(new Character(character3, "Throne Room", "Cultist", 15, 20, 0));
            characterList.Add(new Character(character4, "Stairwell", "Beast", 20, 30, 0));
            characterList.Add(new Character(character5, "Summoning Room", "Goblin", 30, 40, 0));
            characterList.Add(new Character(character6, "Dungeon Entrance", "Elemental", 25, 35, 0));

            characterList[0].totalHitPoint = getHitpointAmount(characterList[0].minHitPoint, characterList[0].maxHitPoint);
            characterList[1].totalHitPoint = getHitpointAmount(characterList[1].minHitPoint, characterList[1].maxHitPoint);
            characterList[2].totalHitPoint = getHitpointAmount(characterList[2].minHitPoint, characterList[2].maxHitPoint);
            characterList[3].totalHitPoint = getHitpointAmount(characterList[3].minHitPoint, characterList[3].maxHitPoint);
            characterList[4].totalHitPoint = getHitpointAmount(characterList[4].minHitPoint, characterList[4].maxHitPoint);
            characterList[5].totalHitPoint = getHitpointAmount(characterList[5].minHitPoint, characterList[5].maxHitPoint);

            List<Location> locationList = new List<Location>();
            locationList.Add(new Location("Dungeon Entrance", 1, 3));
            locationList.Add(new Location("Library", 1, 2));
            locationList.Add(new Location("Courtyard", 3, 7));
            locationList.Add(new Location("Armory", 2, 5));
            locationList.Add(new Location("Summoning Room", 4, 7));
            locationList.Add(new Location("Throne Room", 5, 8));
            locationList.Add(new Location("Stairwell", 0, 2));
            locationList.Add(new Location("Passageway", 0, 2));
            locationList.Add(new Location("Great Hall", 3, 5));
            locationList.Add(new Location("Crypt", 3, 4));

            //Should this be randomized or set by the dev? I have chosen to set it myself.
            //ALSO unsure if its possible to add entire objects to queues and have it work properly. was unable to make it work, so I am hard coding location in
            Queue<string> locations = new Queue<string>();
            locations.Enqueue("Dungeon Entrance"); //Entrance
            locations.Enqueue("Courtyard"); //Courtyard
            locations.Enqueue("Great Hall"); //Great hall
            locations.Enqueue("Library"); //Library
            locations.Enqueue("Summoning Room"); //Summoning Room
            locations.Enqueue("Crypt"); //Crypt
            locations.Enqueue("Stairwell"); //Stairwell
            locations.Enqueue("Throne Room"); //Throne Room

            Console.WriteLine($"You have selected the characters of {characterList[0].Name}, {characterList[1].Name}, {characterList[2].Name}, {characterList[3].Name}, {characterList[4].Name} and {characterList[5].Name}. Let the game begin!");

            //Step 1 - Characters enter the location
            foreach (var location in locations)
            {
                int num1;
                int num2;
                int numberOfEnemy = 0;
                for (int i = 0; i < locationList.Count; i++)
                {
                    if (location == locationList[i].locationName)
                    {
                        num1 = locationList[i].enemyNumberMin;
                        num2 = locationList[i].enemyNumberMax;
                        numberOfEnemy = getNumberOfEnemies(num1, num2);
                        //...and are informed of # enemies
                        Console.WriteLine("\n");
                        Console.Write($"~~~ The group enters the {location}. They see {numberOfEnemy}");
                        if (numberOfEnemy == 1)
                        {
                            Console.WriteLine($" enemy... ~~~");
                        }
                        else
                        {
                            Console.WriteLine($" enemies.... ~~~");
                        }
                    }
                }
                for (int i = 0; i < characterList.Count; i++)
                {
                    Console.Write($"{characterList[i].name} has {characterList[i].totalHitPoint} hitpoints. ");
                }
                Console.WriteLine("\n~~~~~~~~~~~");

                //Step 2 - An enemy is drawn from the deck 
                for (int i = 0; i < numberOfEnemy; i++)
                {
                    Enemy chosenE = null;
                    for (int j = 0; j < enemyList.Count; j++)
                    {
                        if (enemyDeck.Peek() == enemyList[j].name)
                        {
                            chosenE = enemyList[j];
                            Console.Write($"A {chosenE.name} attacks the group! ");
                        }
                    }

                    //Step 3 - A random character is chosen to fight the enemy
                    Random rnd = new Random();
                    int chosenIndex = rnd.Next(0, characterList.Count);
                    Character chosenChar = characterList[chosenIndex];
                    Console.Write($"{chosenChar.name} steps in to defend the group.\n ");

                    //Step 4 - If that character cannot fight in location or against enemy it takes damage equal to enemy damage output
                    if (chosenChar.locCantFight == location || chosenChar.enemyCantFight == chosenE.name)
                    {
                        if (chosenChar.locCantFight == location)
                        {
                            int damage = getDamageAmount(chosenE.minDamage, chosenE.maxDamage);
                            Console.WriteLine($"{chosenChar.name} decides the {location} is a little too spooky and doesn't want to fight anymore.");
                            Console.Write($"The {chosenE.name} attacks {chosenChar.name} and causes {damage} points of damage.");
                            chosenChar.totalHitPoint = chosenChar.totalHitPoint - damage;
                            Console.Write($" {chosenChar.name} has {chosenChar.totalHitPoint} hitpoints left. \n");
                            if (chosenChar.totalHitPoint <= 0)
                            {
                                Console.WriteLine($" {chosenChar.name} has died. Oh no :(");
                                characterList.Remove(chosenChar);
                            }
                        }
                        else if (chosenChar.enemyCantFight == chosenE.name)
                        {
                            int damage = getDamageAmount(chosenE.minDamage, chosenE.maxDamage);
                            Console.WriteLine($"{chosenChar.name} actually thinks this {chosenE.name} is a really great guy and decides not to fight. They make plans to get beers after work. ");
                            Console.Write($"The {chosenE.name} attacks {chosenChar.name} anyways and causes {damage} points of damage. ");
                            chosenChar.totalHitPoint = chosenChar.totalHitPoint - damage;
                            Console.Write($"{chosenChar.name} has {chosenChar.totalHitPoint} hitpoints left. ");
                            if (chosenChar.totalHitPoint <= 0)
                            {
                                Console.WriteLine($"{chosenChar.name} has died. Oh no :( ");
                                characterList.Remove(chosenChar);
                                if (characterList.Count == 0)
                                {
                                    Console.WriteLine("All of your players have died and you have lost the game.");
                                }
                            }
                        }
                    }
                    //Step 5 - Else that character defeats the enemy
                    enemyDeck.Pop();
                }

            }
            Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("~~~~~~~~~~~~~~~~~Winner~~~~~~~~~~~~~~~~");
            Console.WriteLine("Congratulations! You have defeated all of the monsters in the dungeons. Your remaining players include: ");
            for (int i = 0; i < characterList.Count; i++)
            {
                Console.WriteLine($"{characterList[i].name} with {characterList[i].totalHitPoint} hitpoints left.");
            }
        }

        static int getNumberOfEnemies(int num1, int num2)
        {
            Random rnd = new Random();
            int numEnemies = rnd.Next(num1, num2);
            return numEnemies;
        }

        static int getDamageAmount(int num1, int num2)
        {
            Random rnd = new Random();
            int damageAmount = rnd.Next(num1, num2);
            return damageAmount;
        }

        static int getHitpointAmount(int num1, int num2)
        {
            Random rnd = new Random();
            int hitpointAmount = rnd.Next(num1, num2);
            return hitpointAmount;
        }

        public class Enemy
        {
            public string name;
            public int minDamage;
            public int maxDamage;
            public Enemy(string name, int minDamage, int maxDamage)
            {
                this.name = name;
                this.minDamage = minDamage;
                this.maxDamage = maxDamage;
            }
            public string Name
            {
                get { return name; }
                set { name = value; }
            }
            public int MinDamage
            {
                get { return minDamage; }
                set { minDamage = value; }
            }
            public int MaxDamage
            {
                get { return maxDamage; }
                set { maxDamage = value; }
            }

        }


        public class Character
        {
            public string name;
            public string locCantFight;
            public string enemyCantFight;
            public int minHitPoint;
            public int maxHitPoint;
            public int totalHitPoint;
            public Character(string name, string locCantFight, string enemyCantFight, int minHitPoint, int maxHitPoint, int totalHitPoint)
            {
                this.name = name;
                this.locCantFight = locCantFight;
                this.enemyCantFight = enemyCantFight;
                this.minHitPoint = minHitPoint;
                this.maxHitPoint = maxHitPoint;
                this.totalHitPoint = totalHitPoint;
            }
            public string Name
            {
                get { return name; }
                set { name = value; }
            }
            public string LocCantFight
            {
                get { return locCantFight; }
                set { locCantFight = value; }
            }
            public string EnemyCantFight
            {
                get { return enemyCantFight; }
                set { enemyCantFight = value; }
            }
            public int MinHitPoint
            {
                get { return minHitPoint; }
                set { minHitPoint = value; }
            }
            public int MaxHitPoint
            {
                get { return maxHitPoint; }
                set { maxHitPoint = value; }
            }
            public int TotalHitPoint
            {
                get { return totalHitPoint; }
                set { totalHitPoint = value; }
            }
        }

        public class Location
        {
            public string locationName;
            public int enemyNumberMin;
            public int enemyNumberMax;
            public Location(string locationName, int enemyNumberMin, int enemyNumberMax)
            {
                this.locationName = locationName;
                this.enemyNumberMin = enemyNumberMin;
                this.enemyNumberMax = enemyNumberMax;
            }
            public string LocationName
            {
                get { return locationName; }
                set { locationName = value; }
            }
            public int EnemyNumberMin
            {
                get { return enemyNumberMin; }
                set { enemyNumberMin = value; }
            }
            public int EnemyNumberMax
            {
                get { return enemyNumberMax; }
                set { enemyNumberMax = value; }
            }
        }


    }



}
