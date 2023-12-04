using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonQuest.Domain.Repositories
{
    public class Enchanter : Hero
    {

        

        public Enchanter(string name)
        {
            SpecialChoiceMessage = "4 - Spend 75% of your Mana to regain all of your HP";
            SpecialAbility = "Regenerate";
            Name = name;
            HP = 50;
            XP = 0;
            Mana = 40;
            Dmg = 50;
            JoomboosUsed = false;
        }
    }
}
