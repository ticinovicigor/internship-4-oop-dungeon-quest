using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonQuest.Domain.Repositories
{
    public class Witch : Monster
    {
        public Witch(int hp, int damage)
        {
            HP = hp;
            Damage = damage;
        }
    }
}
