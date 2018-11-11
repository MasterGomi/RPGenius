using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGenius
{
    /// <summary>
    /// An offensive-type skill (can be a PhysSkill or a MagSkill)
    /// </summary>
    abstract class OffensiveSkill : Skill
    {
        /// <summary>
        /// Creates an offensive (Physical or Magical) skill
        /// </summary>
        /// <param name="name">The name of the skill</param>
        /// <param name="targetOptions">The targeting options availiable</param>
        /// <param name="mpCost">The mp cost of using the spell</param>
        /// <param name="missChance">The (percentile) chance that the skill will miss</param>
        /// <param name="allOrNothing">Determines how many times the hit is checked (used for skills with multiple targets)</param>
        /// <param name="effectKind">The type of effect that the skill has</param>
        /// <param name="effectDuration">The amount of turns the effect lasts</param>
        /// <param name="effectChance">The (percentile) chance of the effect being applied</param>
        /// <param name="severity">The severity of the effect. Used for poison and burn</param>
        public OffensiveSkill(string name, SkillTarget targetOptions, int mpCost, int missChance, bool allOrNothing, EffectKind effectKind, int effectDuration, int effectChance, EffectSeverity severity)
            : base (name, targetOptions, mpCost, effectKind, effectDuration, effectChance, severity)
        {
            _missChance = missChance;
            _allOrNothing = allOrNothing;
        }
    }
}
