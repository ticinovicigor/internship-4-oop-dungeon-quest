using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonQuest.Domain.Repositories
{
    public class Goblin : Monster
    {
        public Goblin(int hp, int damage, int xp)
        {
            Name = "Goblin";
            HP = hp;
            Dmg = damage;
            XP = xp;
            MaxHP = hp;
        }
    }
}
