using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGenius
{
    /// <summary>
    /// A magical skill that deals damage
    /// </summary>
    class MagSkill : OffensiveSkill
    {
        /// <summary>
        /// Creates a offensive magical skill with an effect
        /// </summary>
        /// <param name="name">The name of the skill</param>
        /// <param name="targetOptions">The targeting options available</param>
        /// <param name="mag">The base magic stat</param>
        /// <param name="mpCost">The ammount of MP necessary for using the skill</param>
        /// <param name="missChance">The (percentile) chance of the skill missing</param>
        /// <param name="effectKind">The type of effect the skill imparts</param>
        /// <param name="effectDuration">The number of turns the skill is in effect for</param>
        /// <param name="effectChance">The (percentile) chance of the effect being applied</param>
        /// <param name="severity">The severity of the effect. Used for poison and burn</param>
        /// <param name="allOrNothing">Determines if the hit is checked once for all targets, or individually (used for skills with multiple targets)</param>
        public MagSkill(string name, SkillTarget targetOptions, int mag, int mpCost, int missChance, EffectKind effectKind, int effectDuration, int effectChance, EffectSeverity severity = EffectSeverity.light, bool allOrNothing = false)
            : base(name, targetOptions, mpCost, missChance, allOrNothing, effectKind, effectDuration, effectChance, severity)
        {
            _mag = mag;
        }
        /// <summary>
        /// Creates an offensive magical skill
        /// </summary>
        /// <param name="name">The name of the skill</param>
        /// <param name="targetOptions">The targeting options available</param>
        /// <param name="mag">The base magic stat</param>
        /// <param name="mpCost">The ammount of MP necessary for using the skill</param>
        /// <param name="missChance">The (percentile) chance of the skill missing</param>
        /// <param name="allOrNothing">Determines if the hit is checked once for all targets, or individually (used for skills with multiple targets)</param>
        public MagSkill(string name, SkillTarget targetOptions, int mag, int mpCost, int missChance, bool allOrNothing = false)
            : this(name, targetOptions, mag, mpCost, missChance, EffectKind.none, 0, 0, EffectSeverity.light, allOrNothing) { }
    }
}
