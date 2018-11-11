using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGenius
{
    /// <summary>
    /// an interface that encompasses necessary feature of all status effects and (de)buffs
    /// </summary>
    interface IEffectOrBuff
    {
        /// <summary>
        /// Applies the effect or buff to the target
        /// </summary>
        /// <param name="target">Subject of the effect or buff</param>
        void Apply(Entity target);
        /// <summary>
        /// Conducts necessary handling of effects or buffs, such as damage from poison
        /// </summary>
        /// <param name="target">The subject of the effect or buff</param>
        /// <param name="turnProgress">Position called in turn. 1 = start; 2 = middle; 3 = end</param>
        void Handle(Entity target, int turnProgress);
        /// <summary>
        /// Used in a player's turn. returns a string containing the effect
        /// </summary>
        /// <returns>string in format => "*[effect]*"</returns>
        string Display();
    }
}
