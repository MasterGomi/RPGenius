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
        protected IEffectOrBuff _effect;
        //
        
        public int ATK { get => _atk; }
        public int MAG { get => _mag; }
        public int MPCost { get; }
        public SkillTarget TargetOptions { get; }   //note: when making a skill creation system, magic and physical skills shouldn't be friendly targeted, only buffs should be
        public string Name { get; }
        public int MissChance { get; }
        public bool MultiAllOrNothing { get; }  //if true, multi target attacks either hit everyone, or no one (meaning miss chance is only checked once, instead of for each enemy)
        public IEffectOrBuff Effect { get; }
        public int EffectChance { get; }
        //
        /// <summary>
        /// Creates a Skill object
        /// </summary>
        /// <param name="name">Name of the skill</param>
        /// <param name="targetOptions">The possible targets for the skill</param>
        /// <param name="mpCost">The MP cost for using the skill</param>
        /// <param name="missChance">The (percentile) chance of the skill missing</param>
        /// <param name="allOrNothing">Determines if the attack hitting is checked once, or for each enemy</param>
        /// <param name="effectKind">Type of effect the skill has (use none for no effect)</param>
        /// <param name="effectDuration">Number of turns the effect lasts for</param>
        /// <param name="effectChance">The (percentile) chance of the effect being applied</param>
        /// <param name="severity">The severity of the effect</param>
        public Skill(string name, SkillTarget targetOptions, int mpCost, int missChance, bool allOrNothing, EffectKind effectKind, int effectDuration, int effectChance, EffectSeverity severity)
        {
            Name = name;
            TargetOptions = targetOptions;
            MPCost = mpCost;
            MissChance = missChance;
            MultiAllOrNothing = allOrNothing;
            EffectChance = effectChance;
            if (effectKind != EffectKind.none)
            {
                EffectFactory effectFactory = new EffectFactory();
                Effect = effectFactory.Create(effectKind, effectDuration, severity);
            }
        }
    }
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
    /*public enum BonusEffect //for use with physical and magical skills
    {
        none,
        DrainHP,
        DrainMP,
        HasStatusEffect,
        HasStatDebuff
    }*/
    public enum EffectKind
    {
        none,
        poison,     //saps HP at end of turn (long-lasting, won't kill, if fatal, reduce to 1HP)
        burn,       //saps HP at end of turn (more Hp, shorter lifespan than poison)
        freeze,     //skips turn
        stun,       //forces entity to move last, increases miss chances
        fear,       //unlikely to attack, (players will defend if they choose attack but fear passes the check), may cause enemies to run from battle
        confusion,   //may attack themselves or friendly targets instead
        /*more effects go here*/
        ATK,
        DEF,
        MAG,
        SPR,
        turn        //delays or hastens an entity's turn
        /*more (de)buffs go here*/
    }
    public enum EffectSeverity
    {
        light,
        moderate,
        heavy
    }
}

