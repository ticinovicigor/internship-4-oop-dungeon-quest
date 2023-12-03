using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonQuest.Domain.Repositories
{
    public class Gladiator : Hero
    {
        public Gladiator(string name)
        {
            Name = name;
            HP = 150;
            XP = 0;
            Dmg = 25;
        }
    }
}
