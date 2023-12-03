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
        }
    }
}
