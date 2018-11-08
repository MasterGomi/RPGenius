using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RPGenius
{
    abstract class StatusEffect : IEffectOrBuff
    {
        protected int _baseDuration;
        protected Random rnd = new Random();
        //
        public virtual void Apply(Entity target)
        {
            Thread.Sleep(500);
            target.EffectDurationRemaining = _baseDuration + rnd.Next(-1, 1);
            target.Effect = this;
        }
        public abstract void Handle(Entity target);
        public abstract string Display();
        //
        public StatusEffect(int baseDuration)
        {
            _baseDuration = baseDuration;
        }
    }
}
