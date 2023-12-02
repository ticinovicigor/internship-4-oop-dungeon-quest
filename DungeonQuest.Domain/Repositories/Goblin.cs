using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonQuest.Domain.Repositories
{
    public class Goblin : Monster
    {
        public Goblin() 
        {
            HP = 25;
            Damage = 15;
        }
    }
}
