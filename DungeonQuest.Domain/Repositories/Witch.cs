using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonQuest.Domain.Repositories
{
    public class Witch : Monster, IEntity   
    {
        public Witch(int hp, int damage, int xp)
        {
            HP = hp;
            Damage = damage;
            XP = xp;
        }
        public void Damage(Entity enemy, int damage)
        {

        }
    }
}
