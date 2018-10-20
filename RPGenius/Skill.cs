using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGenius
{
    abstract class Skill
    {
        protected int _atk;
        protected int _mag;
        protected string _effect;
        //
        public enum SkillTarget
        {
            TargetOneEnemy = 1,
            TargetAllEnemies,
            TargetOnePlayer,
            TargetAllPlayers,
            TargetSelf
        }
        public int ATK { get => _atk; }
        public int MAG { get => _mag; }
        public int MPCost { get; }
        public SkillTarget TargetOptions { get; }   //note: when making a skill creation system, magic and physical skills shouldn't be friendly targeted, only buffs should be
        public string Effect { get => _effect; }
        public string Name { get; }
        public int MissChance { get; }
        public bool MultiAllOrNothing { get; }  //if true, multi target attacks either hit everyone, or no one (meaning miss chance is only checked once, instead of for each enemy)
        //
        public Skill(string name, SkillTarget targetOptions, int mpCost, int missChance, bool allOrNothing)
        {
            Name = name;
            TargetOptions = targetOptions;
            MPCost = mpCost;
            MissChance = missChance;
            MultiAllOrNothing = allOrNothing;
        }
    }
}
