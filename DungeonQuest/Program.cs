using DungeonQuest.Domain.Repositories;
using System.Runtime.InteropServices;


string welcome = "Welcome to the Dungeon!\nTo win, you need to defeat 10 monsters.\nGood luck!\n\nTo start, enter your nickname:";

while (true)
{
    //  CHARACTER CREATION

    Hero Player = new Hero();
    string playerName = GetHeroName(welcome);

    Console.Clear();
    int heroId = ChooseHero();  // 1 - Gladiator, 2 - Enchanter, 3 - Marksman
    Player = CreateHero(Player, heroId, playerName);


    Console.Clear();
    CustomHp(Player);           // The player can set themselves a custom amount of HP if they want
    Player.MaxHP = Player.HP;

    
    
    
    
    // GENERATING ENEMIES

    List<int> monsters = GenerateMonsters(10);  // creates a list of n IDs (75% -> Goblin (ID = 1), 20% -> Brute (ID = 2), 5% - Witch (ID = 3))

    
    
    
    
    
    
    // COMBAT

    Console.Clear();

    int i = 0;
    bool flag = false;
    int monsterId = new int();

    for (int j = 0; j < monsters.Count; j++)    // for loop cycles through all generated enemies and faces them against the Player
    {
        i++;
        monsterId = monsters[i - 1];

        Monster currentEnemy = CreateEnemy(monsterId);  

        Entity winner = Duel(Player, currentEnemy, i); 



        if (winner as Monster != null)  // game over
        {
            i--;
            Console.WriteLine("\nYou lost. Better luck next time :(");
            flag = true;
            break;  // exits for-loop and ends the game
        }

        // this part is reachable only if the player has won the Duel
        
        // XP and HP gain

        Player.XP += currentEnemy.XP;

        Player.HP += Player.MaxHP / 4;

        if (Player.HP > Player.MaxHP)
            Player.HP = Player.MaxHP;


        if (currentEnemy as Witch != null)  // if the Player has defeated a Witch, it spawns another 2 enemies and the Player faces them immediately 
        {
            List<int> minions = new List<int>();

            do
            {
                minions = GenerateMonsters(2);

            } while (minions.Contains(3));

            monsters.Insert(i, minions[1]);
            monsters.Insert(i, minions[0]);

            Console.WriteLine($"You have defeated the witch, but she has spawned 2 new enemies");
            Console.WriteLine("To win the game, you now have to defeat 2 enemies extra");

        }
    }

    //  GAME END

    if (!flag)
        Console.WriteLine("\nCongratulations. You won!");

    Console.WriteLine($"You have passed {i} enemies and reached level {Player.XP / 100}");

    bool again = GoAgain();

    if (!again)
        break;
    
    Console.Clear();
}

static string GetHeroName(string welcome)
{
    string newName = "";
    bool firstTime = true;

    while (newName == "")
    {
        if (!firstTime)
        {
            Console.Clear();
            Console.WriteLine("Wrong input, try again");
        }
        firstTime = false;

        Console.WriteLine(welcome);
        newName = Console.ReadLine();
    }
    
    return newName;
}

static int ChooseHero()
{
    while (true)
    {
        Console.WriteLine("Choose your hero:\n1 - Gladiator (150 HP, 20 DMG, Rage)\n2 - Enchanter (50 HP, 50 DMG, Mana, Resurrection)\n3 - Marksman (100 HP, 35 DMG, Critical Chance, Stun Chance)");
        string choice = Console.ReadLine();
        switch(choice)
        {
            case "1":
                return 1;
            case "2": 
                return 2;
            case "3": 
                return 3;
            default:
                Console.Clear();
                Console.WriteLine("Wrong input, try again");
                break;
        }
    }
}

static Hero CreateHero(Hero Player, int heroId, string playerName)
{
    switch (heroId)
    {
        case 1:
            return new Gladiator(playerName);
            
        case 2:
            return new Enchanter(playerName);
            
        case 3:
            return new Marksman(playerName);    
    }
    return new Hero(); //added because of error - not all code paths return a value
}

static void CustomHp(Hero Player)
{
    string customHp = "";
    int newHp = new int();
    bool firstTime = true;
    while (!int.TryParse(customHp, out newHp))
    {
        Console.Clear();
        if (!firstTime)
            Console.WriteLine("Wrong input, try again");
        firstTime = false;
        
        Console.WriteLine($"If you want your hero to have a custom amount of HP, enter it now.\nIf you enter 0 or a negative number, HP will remain at its default value ({Player.HP}):");
        customHp = Console.ReadLine();        
    }
    if(newHp > 0)
        Player.HP = newHp;
}

static List<int> GenerateMonsters(int n)
{
    
    List<int> idList = new List<int>();
    Random random = new Random();
    int id = new int();

    for (int i = 0; i < n; i++)
    {
        id = random.Next(1, 101);

        if (id <= 5)
            idList.Add(3);

        else if (id <= 25)
            idList.Add(2);

        else
            idList.Add(1);
    }
    return idList;
}

static Monster CreateEnemy(int id)
{
    Random random = new Random();
    int hp = new int();
    int xp = new int();
    int dmg = new int();
    switch (id)
    {
        case 1: //goblin
            hp = random.Next(20, 36);
            dmg = random.Next(20, 36);
            xp = (hp + dmg) / 2;
            return new Goblin(hp, dmg, xp);
                        
        case 2: //brute
            hp = random.Next(60, 80);
            dmg = random.Next(25, 41);
            xp = (hp + dmg) / 2;
            return new Brute(hp, dmg, xp);

        case 3: //witch
            hp = random.Next(35, 61);
            dmg = random.Next(25, 41);
            xp = (hp + dmg) / 2;
            return new Witch(hp, dmg, xp);
    }
    return new Monster(); //added because of error - not all code paths return a value
}

 

static Entity Duel(Hero Player, Monster currentEnemy, int enemyIndex)
{
    Random random = new Random();
    List<string> attacks = new List<string> { "", "Direct Attack", "Flank Attack", "Counterattack", Player.SpecialAbility};

    // determining Mana for the Enchanter
    int maxMana = 40 + 20 * (Player.XP / 100);
    Player.Mana = maxMana;

    // determining Critical and Stun Chance for the Marksman
    Player.CriticalChance = 10 + 6 * (Player.XP / 100);
    bool critical = false;
    bool stun = false;

    // if a Witch has previously used Joomboos, it is noted in the Player's property JoomboosUsed
    // randomizes HP for every upcoming enemy
    if(Player.JoomboosUsed)
        currentEnemy.HP = random.Next(1, Player.MaxHP);
    

    //repeats fighting until someone dies
    while(Player.HP > 0 && currentEnemy.HP > 0)
    {
        // display data
        Console.WriteLine($"\nEnemy {enemyIndex}: ({currentEnemy.Name})\nYour level is {Player.XP/100}\n");
        Console.WriteLine("Name\t\tHP\t\tDMG\t\tXP");
        Console.WriteLine($"{Player.Name}\t\t{Player.HP}/{Player.MaxHP}\t\t{Player.Dmg}\t\t{Player.XP%100}/100");
        Console.WriteLine($"{currentEnemy.Name}\t\t{currentEnemy.HP}/{currentEnemy.MaxHP}\t\t{currentEnemy.Dmg}\t\t{currentEnemy.XP}\n");

        if(Player as Enchanter != null)
            Console.WriteLine($"You have {Player.Mana}/{maxMana} Mana\n");

        // making moves
        int playerAttack = ChooseAttack(Player);
        int enemyAttack = EnemyChooseAttack(); 

        
        // Player's special attacks
        if (attacks[playerAttack] == "Rage")
        {
            Player.HP -= Player.MaxHP/5;
            if (Player.HP <= 0) //losing the game after using Rage is allowed
                break;

            currentEnemy.TakeDamage(Player, true);
            Console.Clear();
            Console.WriteLine($"You have used Rage and have dealt {Player.Dmg*2} damage!");
            continue;
        }

        if (attacks[playerAttack] == "Regenerate")
        {
            Player.Mana /= 4;
            Player.HP = Player.MaxHP;
            
            Console.Clear();
            Console.WriteLine("You have spent this move on regenerating your HP for 75% of your Mana");
            
            continue;
        }

        if(stun)
        {
            stun = false;

            currentEnemy.TakeDamage(Player, false);

            Console.Clear();
            Console.WriteLine($"The enemy was stunned and you have dealt {Player.Dmg} damage!");
            continue;
        }

        // draw
        if (playerAttack == enemyAttack)
        {
            Console.Clear();
            Console.WriteLine($"{currentEnemy.Name} has used {attacks[enemyAttack]}. It's a draw!");

            Player.Mana -= 10;
            if (Player.Mana < 0)
                Player.Mana = 0;

            continue;
        }

        // determines whose attack won
        bool playerHasWon = AttackWon(playerAttack, enemyAttack);

        critical = false;
        stun = false;

        // determines if the Marksman has used Critical or stun in this move
        if (Player as Marksman != null)
        {
            int chance = random.Next(1, 101);   // critical attack and stun attack CANNOT be used simultaneously
            
            if (chance <= Player.CriticalChance / 2)
                stun = true;

            else if (chance <= Player.CriticalChance)
                critical = true;
        }

        if (playerHasWon)
        {
            // Enchanter special trait
            if(Player as Enchanter != null && Player.Mana <= 0)
            {
                Player.Mana = maxMana;
                Console.Clear();
                Console.WriteLine("You had no Mana left so you have spent this move on regaining it.");
                continue;
            }

            // Marksman special trait
            if (critical)
            {
                currentEnemy.TakeDamage(Player, true);

                Console.Clear();
                Console.WriteLine($"You have landed a Critical Hit and have dealt {Player.Dmg * 2} damage!");
                continue;
            }

            currentEnemy.TakeDamage(Player, false);

            // decreases Mana for the Enchanter
            Player.Mana -= 10;
            if (Player.Mana < 0)
                Player.Mana = 0;

            Console.Clear();
            Console.WriteLine($"{currentEnemy.Name} has used {attacks[enemyAttack]}. You have dealt {Player.Dmg} damage!");
        }

        else
        {
            // Brute special attack
            if (currentEnemy as Brute != null)
            {
                
                int chance = random.Next(1, 101);

                if (chance <= 15)
                {
                    Player.TakeDamage(currentEnemy, true);
                    Console.Clear();
                    Console.WriteLine("The enemy has landed a critical strike and you have lost 25% of your full HP");
                    continue;
                }
            }

            // Witch special attack
            if(currentEnemy as Witch != null)
            {
                
                int chance = random.Next(1, 101);

                if (chance <= 20)
                    Player.JoomboosUsed = true;
                
                Console.Clear();
                Console.WriteLine("Witch has used Joomboos. From now on everyone's HP has been shuffled");
                continue;
            }

            Player.TakeDamage(currentEnemy, false);

            Player.Mana -= 10;
            if (Player.Mana < 0)
                Player.Mana = 0;

            Console.Clear();
            Console.WriteLine($"{currentEnemy.Name} has used {attacks[enemyAttack]}. You have lost {currentEnemy.Dmg} HP!");
        }

        if(stun)
            Console.WriteLine("You have stunned your enemy!");
    }

    if (Player.HP > 0)
        return Player;
         
    // allows Enchanter to respawn if game over
    else if (!Player.HasRespawned && Player as Enchanter != null)
    {
        Console.WriteLine("You have respawned with full HP! Be careful, this is your last life.");
        Player.HP = Player.MaxHP;
        Player.HasRespawned = true;
        return Duel(Player, currentEnemy, enemyIndex);
    }

    else
        return currentEnemy;
} 

static int ChooseAttack(Hero Player)
{
    while (true)
    {
        Console.WriteLine("Choose your attack:\n1 - Direct Attack\n2 - Flank Attack\n3 - Counterattack");
        Console.WriteLine(Player.SpecialChoiceMessage); //  string in format "4 - {hero-special-ability}"
        string choice = Console.ReadLine();
        
        if(choice == "4" && Player as Marksman == null) // every hero except marksman has a special "activatable" ability
            return 4;

        switch (choice)
        {
            case "1":
                return 1;
            
            case "2":
                return 2;
            
            case "3":
                return 3;
            
            default:
                Console.Clear();
                Console.WriteLine("Wrong input, try again");
                break;
        }
    }
}

static int EnemyChooseAttack()
{
    Random random = new Random();
    return random.Next(1, 4);
}

static bool AttackWon(int playerAttack, int enemyAttack)
{
    //1 - direct, 2 - flank, 3 - counter
    
    if(playerAttack == 1 && enemyAttack == 2)
        return true;
    
    if(playerAttack == 2 && enemyAttack == 3) 
        return true;
    
    if(playerAttack == 3 && enemyAttack == 1)
        return true;

    else
        return false;
}

static bool GoAgain()
{
    while (true)
    {
        Console.WriteLine("\nThank you for playing.\n\nDo you want to try again?\n1 - Yes\n2 - No");
        string choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                return true;
            case "2":
                return false;
            default:
                Console.Clear();
                Console.WriteLine("Wrong input, try again");
                break;
        }
    }
}