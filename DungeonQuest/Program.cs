using DungeonQuest.Domain.Repositories;

string input = "";
string welcome = "Dobrodosli u Dungeon Quest!\nKako biste pobijedili, morate proci svih 10 cudovista pred Vama.\nSretno!\n\nZa pocetak unesite ime svog lika:";

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

    // GENERATING ENEMIES

    List<int> monsters = GenerateMonsters();
    
    // COMBAT

    foreach (int monsterId in monsters)
    {
        Monster currentEnemy = CreateEnemy(monsterId);
        
    }

}

static string GetHeroName(string welcome)
{
    string newName = "";
    bool firstTime = true;

    while (newName == "")
    {
        if(!firstTime)
            Console.WriteLine("Nepravilan unos, pokusajte ponovo");
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
        Console.WriteLine("Odaberite vrstu svog heroja:\n1 - Gladiator\n2 - Enchanter\n3 - Marksman");
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
                Console.WriteLine("Nepravilan unos, pokusajte ponovo");
                break;
        }
    }
}

static Hero CreateHero(Hero Player, int heroId, string playerName)
{
    switch (heroId)
    {
        case 1:
            return new Gladiator(playerName, 150, 25);
            
        case 2:
            return new Enchanter(playerName, 50, 50);
            
        case 3:
            return new Marksman(playerName, 100, 35);    
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
            Console.WriteLine("Nepravilan unos, pokusajte ponovo");
        firstTime = false;
        
        Console.WriteLine($"Ako zelite da Vas heroj ima custom iznos HP-a, unesite taj broj sad.\nAko unesete 0 ili negativan broj HP ce ostati na svojoj default vrijednosti ({Player.HP}):");
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