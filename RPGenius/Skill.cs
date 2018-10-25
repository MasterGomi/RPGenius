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
        protected StatusEffect _status;
        protected BonusEffect _bonus;
        protected StatChange _buff;
        protected StatChange _debuff;
        //
        public enum SkillTarget
        {
            TargetOneEnemy = 1,
            TargetAllEnemies,
            TargetOneFriend,
            TargetAllFriends,
            TargetSelf,
          /*TargetRandomEnemy,
            TargetRandomPlayer,
            TargetRandom    */
        }
        public enum StatusEffect
        {
            none,
            poison,     //saps HP at end of turn (long-lasting, won't kill, if fatal, reduce to 1HP)
            burn,       //saps HP at end of turn (more Hp, shorter lifespan than poison)
            freeze,     //skips turn
            stun,       //forces entity to move last, increases miss chances
            fear,       //unlikely to attack, (players will defend if they choose attack but fear passes the check), may cause enemies to run from battle
            confusion   //may attack themselves or friendly targets instead
        }
        public enum BonusEffect //for use with physical and magical skills
        {
            none,
            DrainHP,
            DrainMP,
            HasStatusEffect,
            HasStatDebuff
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
        public enum EffectSeverity
        {
            light,
            moderate,
            heavy
        }
        public int ATK { get => _atk; }
        public int MAG { get => _mag; }
        public int MPCost { get; }
        public SkillTarget TargetOptions { get; }   //note: when making a skill creation system, magic and physical skills shouldn't be friendly targeted, only buffs should be
        public string Name { get; }
        public int MissChance { get; }
        public bool MultiAllOrNothing { get; }  //if true, multi target attacks either hit everyone, or no one (meaning miss chance is only checked once, instead of for each enemy)
        public StatusEffect Effect { get => _status; }
        public BonusEffect Bonus { get => _bonus; }
        public StatChange Buff { get => _buff; }
        public StatChange Debuff { get => _debuff; }
        public int EffectLength { get; }
        public int EffectChance { get; }
        public EffectSeverity Severity { get; }
        //
        public Skill(string name, SkillTarget targetOptions, int mpCost, int missChance, bool allOrNothing, int effectLength, int effectChance, EffectSeverity severity)
        {
            Name = name;
            TargetOptions = targetOptions;
            MPCost = mpCost;
            MissChance = missChance;
            MultiAllOrNothing = allOrNothing;
            EffectLength = effectLength;
            EffectChance = effectChance;
            Severity = severity;
        }
    }
}
