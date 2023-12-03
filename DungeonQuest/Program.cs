using DungeonQuest.Domain.Repositories;
using System.Runtime.InteropServices;

string input = "";
string welcome = "Welcome to the Dungeon!\nTo win, you need to defeat 10 monsters.\nGood luck!\n\nTo start, enter your nickname:";

while (true)
{
    //  CHARACTER CREATION

    Hero Player = new Hero();
    string playerName = GetHeroName(welcome); 
    
    Console.Clear();
    int heroId = ChooseHero();  // 1 - Gladiator, 2 - Enchanter, 3 - Marksman
    Player = CreateHero(Player, heroId, playerName);
    

    Console.Clear() ;
    CustomHp(Player);
    Player.MaxHP = Player.HP;

    // GENERATING ENEMIES

    List<int> monsters = GenerateMonsters();

    // COMBAT

    Console.Clear();
    int i = 0;
    foreach (int monsterId in monsters)
    {
        i++;
        Monster currentEnemy = CreateEnemy(monsterId);
        Entity winner = Duel(Player, currentEnemy, i);
        Console.Clear();
    }

}

static string GetHeroName(string welcome)
{
    string newName = "";
    bool firstTime = true;

    while (newName == "")
    {
        if(!firstTime)
            Console.WriteLine("Wrong input, try again");
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
        Console.WriteLine("Choose your hero:\n1 - Gladiator\n2 - Enchanter\n3 - Marksman");
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
        
        Console.WriteLine($"If you want your hero to have a custom amount of HP, enter it now.\nIf you enter 0 or a negative number, HP will remnain at its default value ({Player.HP}):");
        customHp = Console.ReadLine();        
    }
    if(newHp > 0)
        Player.HP = newHp;
}

static List<int> GenerateMonsters()
{
    //creates a list of 10 IDs (50% -> Goblin (ID = 1), 24% -> Brute (ID = 2), 1% - Witch (ID = 3))
    List<int> idList = new List<int>();
    Random random = new Random();
    int id = new int();

    for (int i = 0; i < 10; i++)
    {
        id = random.Next(1, 101);

        if (id == 1)
            idList.Add(3);

        else if (id < 25)
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
    bool firstTime = true;

    while(Player.HP > 0 && currentEnemy.HP > 0)
    {
        Console.WriteLine($"Enemy {enemyIndex} of 10 ({currentEnemy.Name})\nYour level is {Player.XP/10}\n");
        Console.WriteLine("Name\t\tHP\t\tDMG\t\tXP");
        Console.WriteLine($"{Player.Name}\t\t{Player.HP}/{Player.MaxHP}\t\t{Player.Dmg}\t\t{Player.XP%100}/100");
        Console.WriteLine($"{currentEnemy.Name}\t\t{currentEnemy.HP}/{currentEnemy.MaxHP}\t\t{currentEnemy.Dmg}\t\t{currentEnemy.XP}\n");

        var playerAttack = ChooseAttack();
        var enemyAttack = EnemyChooseAttack(); 

        if(playerAttack == enemyAttack)
        {
            Console.Clear();
            //to do: appropriate message
            continue;
        }

        bool playerHasWon = AttackWon(playerAttack, enemyAttack);
        
        if(playerHasWon)
        {
            currentEnemy.TakeDamage(Player, false);
            Console.Clear();
            //to do: appropriate message
        }

        else
        {
            Player.TakeDamage(currentEnemy, false); 
            Console.Clear();
            //to do: appropriate message
        }

    }

    return Player;
} 

static int ChooseAttack()
{
    while (true)
    {
        Console.WriteLine("Choose your attack:\n1 - Direct Attack\n2 - Flank Attack\n3 - Counterattack");
        string choice = Console.ReadLine();
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