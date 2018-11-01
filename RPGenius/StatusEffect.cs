using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGenius
{
    abstract class StatusEffect : IEffectOrBuff
    {
        protected int _damageVar;
        protected int _baseDamage;
        protected int _baseDuration;
        protected Random rnd = new Random();
        //
        public abstract void Apply(Entity target);
        public abstract void Handle(Entity target);
        public abstract string Display();
        //
        public StatusEffect(int baseDuration)
        {
            _baseDuration = baseDuration;
        }
    }
}
