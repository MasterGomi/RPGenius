using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RPGenius
{
    class Poison : StatusEffect, IEffectOrBuff
    {
        private int _damageVar;
        private int _baseDamage;
        public Poison(int baseDuration, Skill.EffectSeverity severity) : base(baseDuration)
        {
            switch (severity)
            {
                case Skill.EffectSeverity.light:
                    _baseDamage = 10;
                    _damageVar = 7;
                    break;
                case Skill.EffectSeverity.moderate:
                    _baseDamage = 35;
                    _damageVar = 12;
                    break;
                case Skill.EffectSeverity.heavy:
                    _baseDamage = 70;
                    _damageVar = 20;
                    break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
        //
        public override void Apply(Entity target)
        {
            base.Apply(target);
            Console.WriteLine("\n> {0} has been poisoned", target.Name);
        }
        public override void Handle(Entity target)
        {
            int damage = _baseDamage + rnd.Next(-_damageVar, _damageVar);
            if(target.HP - damage < 1) { target.HP = 1; }
            else { target.HP -= damage; }
            Console.WriteLine("\n> {0} takes {1} damage from poison. HP now {2}/{3}", target.Name, damage, target.HP, target.BaseHp);
            target.EffectDurationRemaining--;
            if(target.EffectDurationRemaining <= 0)
            {
                Thread.Sleep(700);
                Console.WriteLine("> {0} is no longer poisoned", target.Name);
            }
        }
        public override string Display()
        {
            return "\t*poisoned*";
        }
    }
    //
    //
    class Burn : StatusEffect, IEffectOrBuff
    {
        private int _damageVar;
        private int _baseDamage;
        public Burn(int baseDuration, Skill.EffectSeverity severity) : base(baseDuration)
        {
            switch (severity)
            {
                case Skill.EffectSeverity.light:
                    _baseDamage = 27;
                    _damageVar = 10;
                    break;
                case Skill.EffectSeverity.moderate:
                    _baseDamage = 63;
                    _damageVar = 20;
                    break;
                case Skill.EffectSeverity.heavy:
                    _baseDamage = 117;
                    _damageVar = 23;
                    break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
        //
        public override void Apply(Entity target)
        {
            base.Apply(target);
            Console.WriteLine("\n> {0} has been burned", target.Name);
        }
        public override void Handle(Entity target)
        {
            int damage = _baseDamage + rnd.Next(-_damageVar, _damageVar);
            target.HP -= damage;
            Console.WriteLine("\n> {0} takes {1} damage from their burns. HP now {2}/{3}", target.Name, damage, target.HP, target.BaseHp);
            target.EffectDurationRemaining--;
            if (target.EffectDurationRemaining <= 0 && target.HP > 0)       //it would just be insulting to let the target know that they aren't on fire after they have already died from it
            {
                Thread.Sleep(700);
                Console.WriteLine("> {0} is no longer burned", target.Name);
            }
        }
        public override string Display()
        {
            return "\t*burned*";
        }
    }
    //
    //
    class Freeze : StatusEffect, IEffectOrBuff
    {
        public Freeze(int baseDuration) : base(baseDuration) { }
        //
        public override void Apply(Entity target)
        {
            base.Apply(target);
            Console.WriteLine("\n> {0} has been encased in ice", target.Name);
        }
        public override void Handle(Entity target)
        {
            target.EffectDurationRemaining--;
            if(target.EffectDurationRemaining > 0)
            {
                Console.WriteLine("\n> {0} is frozen in place", target.Name);
                target.CanUseTurn = false;
            }
            else { Console.WriteLine("> {0} has broken out of the ice", target.Name); }
        }
        public override string Display()
        {
            return "Whoops, something went wrong";  //Should never be seen, as players don't get thier turn when frozen
        }
    }
    //
    //
    class Stun : StatusEffect, IEffectOrBuff
    {
        public Stun(int baseDuration) : base(baseDuration) { }
        //
        public override void Apply(Entity target)
        {
            base.Apply(target);
            Console.WriteLine("\n> {0} is seeing stars", target.Name);
        }
        public override void Handle(Entity target)
        {
            if(target.HaveTurnLater == false)    //this is a clause to prevent stun being handled twice, and is handled such that, if this method is called at the normal time in turn order, "HaveTurnLater" is flagged and nothing else is handled, this is done so that the text and duration handling happens upon returning to this entity for their later turn
            {
                target.HaveTurnLater = true;
                return;
            }
            target.EffectDurationRemaining--;
            if (target.EffectDurationRemaining > 0) { Console.WriteLine("> {0} is still feeling dizzy", target.Name); }
            else { Console.WriteLine("> {0} has recoved from being stunned", target.Name); }
            target.HaveTurnLater = false;
        }
        public override string Display()
        {
            return "\t*stunned*";
        }
    }
    //
    //
    /*class fear*/
    //
    //
    /*class confusion*/
}
