﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DungeonQuest.Domain.Repositories
{
    public class Brute : Monster
    {

        public Brute(int hp, int damage, int xp)
        {
            Name = "Brute";
            HP = hp;
            Dmg = damage;
            XP = xp;
            MaxHP = hp;
        }
    }
}
