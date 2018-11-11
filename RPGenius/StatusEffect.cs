using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RPGenius
{
    /// <summary>
    /// The parent class for all status effects
    /// </summary>
    abstract class StatusEffect : IEffectOrBuff
    {
        protected int _baseDuration;
        protected Random rnd = new Random();
        protected string _removeTerm;
        public string RemoveTerm { get => _removeTerm; }
        //
        /// <summary>
        /// Applies the effect to the target
        /// </summary>
        /// <param name="target">The reciever of the effect</param>
        public virtual void Apply(Entity target)
        {
            Thread.Sleep(500);
            target.EffectDurationRemaining = _baseDuration + rnd.Next(-1, 3);
            target.Effect = this;
        }
        /// <summary>
        /// Used to handle the effect's functionality during the entities turn
        /// </summary>
        /// <param name="target">The entity suffering the effect</param>
        /// <param name="turnProgress">Number relating to the position in the turn. 1 = start; 2 = middle; 3 = end</param>
        public abstract void Handle(Entity target, int turnProgress);
        /// <summary>
        /// Returns the current effect as a string to be displayed to the user
        /// </summary>
        /// <returns>Returns the current effect as a string</returns>
        public abstract string Display();
        //
        public StatusEffect(int baseDuration)
        {
            _baseDuration = baseDuration;
        }
    }
}
