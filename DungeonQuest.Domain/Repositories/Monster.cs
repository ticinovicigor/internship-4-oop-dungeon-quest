using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonQuest.Domain.Repositories
{
    public class Monster : Entity, IEntity
    {
        public void TakeDamage(Entity enemy, bool specialAbilityUsed)
        {
            HP -= enemy.Dmg;

            if(specialAbilityUsed && enemy as Gladiator != null)    //if gladiator uses his special ability the enemy takes double damage
                HP -= enemy.Dmg;
        }
    }
}
