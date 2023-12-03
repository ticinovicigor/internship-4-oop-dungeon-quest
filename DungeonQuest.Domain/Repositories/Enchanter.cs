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
        public Enchanter(string name)
        {
            Name = name;
            HP = 50;
            XP = 0;
            Mana = 50;
            Dmg = 50;
        }
    }
}
