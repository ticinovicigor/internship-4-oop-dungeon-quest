using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonQuest.Domain.Repositories
{
    public class Enchanter : Hero
    {
        int Mana {  get; set; }
        public Enchanter(string name, int hp, int damage)
        {
            Name = name;
            HP = hp;
            XP = 0;
            Damage = damage;
            Mana = 50;
        }
    }
}
