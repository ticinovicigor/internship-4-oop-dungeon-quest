using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonQuest.Domain.Repositories
{
    public class Hero : Entity, IEntity
    {
        public string Name { get; set; }
        
        /*
        public int HP { get; set; }
        public int XP { get; set; } 
        public int Damage { get; set; } 
        */
        public void Damage(Entity enemy, int damage)
        {

        }
    }
}
