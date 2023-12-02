using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DungeonQuest.Domain.Repositories
{
    public class Brute : Monster
    {
        public Brute()
        {
            HP = 75;
            Damage = 25;
        }
    }
}
