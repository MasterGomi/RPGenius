using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGenius
{
    /// <summary>
    /// A physical skill that deals damage
    /// </summary>
    class PhysSkill : OffensiveSkill
    {
        /// <summary>
        /// Creates an offensive physical skill with an effect
        /// </summary>
        /// <param name="name">The name of the skill</param>
        /// <param name="targetOptions">The targeting options availiable</param>
        /// <param name="atk">The base physical damage stat fro the skill</param>
        /// <param name="mpCost">The amount of MP needed to use the skill</param>
        /// <param name="missChance">The (percentile) chance of the skill missing</param>
        /// <param name="effectKind">The type of effect that the skill imparts</param>
        /// <param name="effectDuration">The amount of turns the effect lasts</param>
        /// <param name="effectChance">The (percentile) chance that the effect will be applied</param>
        /// <param name="severity">The severity of the effeect. Used for poison and burn</param>
        /// <param name="allOrNothing">Determines if the hit is checked once for all targets, or individually (used for skills with multiple targets)</param>
        public PhysSkill(string name, SkillTarget targetOptions, int atk, int mpCost, int missChance, EffectKind effectKind, int effectDuration, int effectChance, EffectSeverity severity = EffectSeverity.light, bool allOrNothing = false)
            : base(name, targetOptions, mpCost, missChance, allOrNothing, effectKind, effectDuration, effectChance, severity)
        {
            _atk = atk;
        }
        /// <summary>
        /// Creates a offensive physical skill
        /// </summary>
        /// <param name="name">The name of the skill</param>
        /// <param name="targetOptions">The targeting options available</param>
        /// <param name="atk">The base attack stat of the skill</param>
        /// <param name="mpCost">The ammount of MP necessary to use the skill</param>
        /// <param name="missChance">The (percentile) chance of the skill missing</param>
        /// <param name="allOrNothing">Determines if the hit is checked once for all targets, or individually (used for skills with multiple targets)</param>
        public PhysSkill(string name, SkillTarget targetOptions, int atk, int mpCost, int missChance, bool allOrNothing = false)
            : this(name, targetOptions, atk, mpCost, missChance, EffectKind.none, 0, 0, EffectSeverity.light, allOrNothing) { }
    }
}
