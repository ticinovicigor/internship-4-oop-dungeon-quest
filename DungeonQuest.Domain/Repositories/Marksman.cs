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
        public Marksman(string name)
        {
            Name = name;
            HP = 100;
            XP = 0;
            CriticalChance = 0;
            StunChance = 0;
            Dmg = 35;
        }
    }
}
