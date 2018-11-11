using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGenius
{
    /// <summary>
    /// Used to create required (de)buffs
    /// </summary>
    class StatChangeFactory
    {
        public StatChangeFactory() { }
        //
        /// <summary>
        /// Returns an object representing the (de)buff required from the skill
        /// </summary>
        /// <param name="effectKind">The (de)buff to be created</param>
        /// <param name="severity">The severity of the (de)buff</param>
        /// <param name="duration">The ammont of turns the (de)buff lasts for</param>
        /// <param name="isPositive">Determins if the effect is a buff or a debuff</param>
        /// <returns>StatChance object of the required variety</returns>
        public StatChange Create(EffectKind effectKind, EffectSeverity severity, int duration, bool isPositive)
        {
            switch (effectKind)
            {
                case EffectKind.ATK:
                    return new AtkChange(isPositive, duration, severity);
                case EffectKind.DEF:
                    return new DefChange(isPositive, duration, severity);
                case EffectKind.MAG:
                    return new MagChange(isPositive, duration, severity);
                case EffectKind.SPR:
                    return new SprChange(isPositive, duration, severity);
                default: throw new ArgumentException();
            }
        }
    }
}
