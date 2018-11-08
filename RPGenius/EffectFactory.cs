using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGenius
{
    class EffectFactory
    {
        public EffectFactory() { }
        //
        /// <summary>
        /// Generates an effect class and returns it
        /// </summary>
        /// <param name="effectKind">The type of effect</param>
        /// <param name="duration">How many turns the effect should be in effect for (may vary slightly)</param>
        /// <param name="severity">How severe the effect is. Necessary for poison and burn (defaults to light)</param>
        /// <returns>Returns a IEffectOrBuff effect</returns>
        public IEffectOrBuff Create(Skill.EffectKind effectKind, int duration, Skill.EffectSeverity severity = Skill.EffectSeverity.light)
        {
            switch (effectKind)
            {
                case Skill.EffectKind.poison:
                    return new Poison(duration, severity);
                case Skill.EffectKind.burn:
                    return new Burn(duration, severity);
                case Skill.EffectKind.freeze:
                    return new Freeze(duration);
                case Skill.EffectKind.stun:
                    return new Stun(duration);
                default: throw new NotImplementedException();
            }
        }
    }
}
