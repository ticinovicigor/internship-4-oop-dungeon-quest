using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonQuest.Domain.Repositories
{
    public interface IEntity
    {
        public void TakeDamage(Entity enemy, bool specialAbilityUsed);
    }
}
