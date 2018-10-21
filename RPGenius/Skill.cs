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
        public enum StatusEffect
        {
            none,
            poison,     //saps HP at end of turn
            burn,       //saps HP at end of turn (more Hp, shorter lifespan than poison)
            freeze,     //skips turn
            stun,       //forces entity to move last, increases miss chances
            fear,       //unlikely to attack, (players will defend if they choose attack but fear passes the check), may cause enemies to run from battle
            confusion   //may attack themselves or friendly targets instead
        }
        public enum StatChange  //enum to be used for debuffs and buffs
        {
            none,
            ATK,
            DEF,
            MAG,
            SPR,
            /*add all stats*/
            TurnOrder
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
