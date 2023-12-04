using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonQuest.Domain.Repositories
{
    public class Hero : Entity, IEntity
    {

        public string SpecialChoiceMessage { get; set; }
        public string SpecialAbility { get; set; }

        public int Mana { get; set; }

        public bool HasRespawned = false;

        public int CriticalChance { get; set; }

        public void TakeDamage(Entity enemy, bool specialAbilityUsed)
        {
            HP -= enemy.Dmg;
        }
    }
}
