using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGenius
{
    /// <summary>
    /// An object representing a skill that can be used by enemies or players
    /// </summary>
    abstract class Skill
    {
        protected int _atk;
        protected int _mag;
        protected bool _allOrNothing;
        protected int _missChance;
        //
        
        public int ATK { get => _atk; }
        public int MAG { get => _mag; }
        public int MPCost { get; }
        public SkillUse UseMethod { get; }   //note: when making a skill creation system, offensive skills shouldn't be friendly targeted, only buffs should be
        public string Name { get; }
        public int MissChance { get => _missChance; }
        public bool MultiAllOrNothing { get => _allOrNothing; }  //if true, multi target attacks either hit everyone, or no one (meaning miss chance is only checked once, instead of for each enemy)
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
        public Skill(string name, SkillTarget targetOptions, int mpCost, EffectKind effectKind, int effectDuration, int effectChance, EffectSeverity severity, bool isPositive = false)
        {
            Name = name;
            MPCost = mpCost;
            EffectChance = effectChance;
            switch (effectKind)
            {
                case EffectKind.none:
                    break;
                case EffectKind.poison:
                case EffectKind.burn:
                case EffectKind.freeze:
                    EffectFactory effectFactory = new EffectFactory();
                    Effect = effectFactory.Create(effectKind, effectDuration, severity);
                    break;
                case EffectKind.ATK:
                case EffectKind.DEF:
                case EffectKind.MAG:
                case EffectKind.SPR:
                    StatChangeFactory statChangeFactory = new StatChangeFactory();
                    Effect = statChangeFactory.Create(effectKind, severity, effectDuration, isPositive);
                    break;
                default: throw new ArgumentOutOfRangeException();
            }
            SkillUseFactory skillUseFactory = new SkillUseFactory();
            UseMethod = skillUseFactory.Create(targetOptions, this);

        }
    }
    /// <summary>
    /// Enumeration relating to possible targets for a skill
    /// </summary>
    public enum SkillTarget
    {
        TargetOneEnemy,
        TargetAllEnemies,
        TargetOneFriend,
        TargetAllFriends,
        TargetSelf,
        /*TargetRandomEnemy,
          TargetRandomPlayer,
          TargetRandom    */
    }
    /// <summary>
    /// Enumeration relating to possible effects for a skill
    /// </summary>
    public enum EffectKind
    {
        none,
        poison,     //saps HP at end of turn (long-lasting, won't kill, if fatal, reduce to 1HP)
        burn,       //saps HP at end of turn (more Hp, shorter lifespan than poison)
        freeze,     //skips turn
        stun,       //forces entity to move last, increases miss chances
        fear,       //unlikely to attack, (players will defend if they choose attack but fear passes the check), may cause enemies to run from battle
        confusion,  //may attack themselves or friendly targets instead
        /*more effects go here*/
        ATK,        //}
        DEF,        //} buffs or debuffs
        MAG,        //} selected stat
        SPR,        //}
        //turn        //delays or hastens an entity's turn
        /*more (de)buffs go here*/
    }
    /// <summary>
    /// Enumeration containing the possible severities for effects
    /// </summary>
    public enum EffectSeverity
    {
        light,
        moderate,
        heavy
    }
}

