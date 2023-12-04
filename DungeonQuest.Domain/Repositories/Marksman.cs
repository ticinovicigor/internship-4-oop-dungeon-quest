﻿using System;
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

        
        
        public Marksman(string name)
        {
            Name = name;
            HP = 100;
            XP = 0;
            CriticalChance = 10;
            Dmg = 35;
        }
    }
}
