using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace DungeonQuest.Domain.Repositories
{
    public class Marksman : Hero
    {
        public int CriticalChance { get; set; }
        public int StunChance { get; set; }
        public Marksman(string name, int hp, int damage)
        {
            Name = name;
            HP = hp;
            XP = 0;
            Damage = damage;
            CriticalChance = 0;
            StunChance = 0;

        }
    }
}
