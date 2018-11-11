using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGenius
{
    /// <summary>
    /// Used to create and return necessary StatusEffects
    /// </summary>
    class EffectFactory
    {
        public EffectFactory() { }
        //
        /// <summary>
        /// Generates an effect class and returns it
        /// </summary>
        /// <param name="effectKind">The type of effect</param>
        /// <param name="duration">How many turns the effect should be in effect for (may vary slightly)</param>
        /// <param name="severity">How severe the effect is. Necessary for poison and burn</param>
        /// <returns>Returns a IEffectOrBuff effect</returns>
        public IEffectOrBuff Create(EffectKind effectKind, int duration, EffectSeverity severity)
        {
            switch (effectKind)
            {
                case EffectKind.poison:
                    return new Poison(duration, severity);
                case EffectKind.burn:
                    return new Burn(duration, severity);
                case EffectKind.freeze:
                    return new Freeze(duration);
                case EffectKind.stun:
                    return new Stun(duration);
                default: throw new NotImplementedException();
            }
        }
    }
}
