using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGenius
{
    /// <summary>
    /// A skill that deals no damage, and instead applies an effect
    /// </summary>
    class SupportSkill : Skill
    {
        /// <summary>
        /// Creates a support skill that can impart/clear a status effect or (de)buff
        /// </summary>
        /// <param name="name">The name of the skill</param>
        /// <param name="targetOptions">The targeting options available</param>
        /// <param name="mpCost">The ammount of MP required for using the skill</param>
        /// <param name="effectKind">The effect that the skill relates to</param>
        /// <param name="effectChance">The chance of the effect being imparted (only applies when targeting enemies, guaranteed to hit friends)</param>
        /// <param name="effectDuration">The number of turns the effect lasts (used for enemy targets skills and buffs, not applicable to clearing status effects</param>
        /// <param name="severity">The severity of the effect (only used for imparting poison or burn on enemies, and all buffs and debuffs)</param>
        /// <param name="isPositive">Determines whether a stat change is a buff or a debuff (only necessary for stat changes)</param>
        public SupportSkill(string name, SkillTarget targetOptions, int mpCost, EffectKind effectKind, int effectChance, int effectDuration, EffectSeverity severity = EffectSeverity.light, bool isPositive = true)
            : base(name, targetOptions, mpCost, effectKind, effectDuration, effectChance, severity) { }
    }
}
