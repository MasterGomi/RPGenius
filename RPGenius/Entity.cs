﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RPGenius
{
    abstract class Entity
    {
        private int _turnOrder;
        private int _hp;
        private int _mp;
        private int _atk;
        private int _def;
        private int _mag;
        private int _spr;
        //
        public int BaseHp { get; }
        public int BaseAtk { get; }
        public int BaseDef { get; }
        public int BaseMp { get; }
        public int BaseMag { get; }
        public int BaseSpr { get; }
        public int HP
        {
            get
            {
                return _hp;
            }
            set
            {
                _hp = value;
                if(_hp < 0) { _hp = 0; }
                if(_hp > BaseHp) { _hp = BaseHp; }
            }
        }
        public int ATK
        {
            get
            {
                return _atk;
            }
            set
            {
                _atk = value;
                if(_atk < Convert.ToInt32(BaseAtk * 0.5)) { _atk = Convert.ToInt32(BaseAtk * 0.5); }
                if(_atk > Convert.ToInt32(BaseAtk * 1.5)) { _atk = Convert.ToInt32(BaseAtk * 1.5); }
            }
        }
        public int DEF
        {
            get
            {
                return _def;
            }
            set
            {
                _def = value;
                if (_def < BaseAtk * 0.5) { _def = Convert.ToInt32(BaseDef * 0.5); }
                if (_def > BaseAtk * 1.5) { _def = Convert.ToInt32(BaseDef * 1.5); }
            }
        }
        public int MP
        {
            get
            {
                return _mp;
            }
            set
            {
                _mp = value;
                if (_mp < 0) { _mp = 0; }
                if (_mp > BaseMp) { _mp = BaseMp; }
            }
        }
        public int MAG
        {
            get
            {
                return _mag;
            }
            set
            {
                _mag = value;
                if (_mag < Convert.ToInt32(BaseMag * 0.5)) { _mag = Convert.ToInt32(BaseMag * 0.5); }
                if (_mag > Convert.ToInt32(BaseMag * 1.5)) { _mag = Convert.ToInt32(BaseMag * 1.5); }
            }
        }
        public int SPR
        {
            get
            {
                return _spr;
            }
            set
            {
                _spr = value;
                if (_spr < Convert.ToInt32(BaseSpr * 0.5)) { _spr = Convert.ToInt32(BaseSpr * 0.5); }
                if (_spr > Convert.ToInt32(BaseSpr * 1.5)) { _spr = Convert.ToInt32(BaseSpr * 1.5); }
            }
        }
        public string Name { get; }
        public int TurnOrder
        {
            get { return _turnOrder; }
            set
            {
                _turnOrder = value;
                if (_turnOrder > 10) { _turnOrder = 10; }
                if(_turnOrder < 0) { _turnOrder = 0; }
            }
        }
        public bool IsDefending { get; set; }
        public Weapon Weapon { get; set; }
        public List<Skill> Skills = new List<Skill>();
        public Skill.StatusEffect StatusEffect { get; set; }
        public int EffectDurationRemaining { get; set; }
        public Skill.EffectSeverity EffectSeverity { get; set; }
        public List<Skill.StatChange> Buffs = new List<Skill.StatChange>();
        public List<int> BuffDurationRemaining = new List<int>();
        public List<Skill.EffectSeverity> BuffSeverity = new List<Skill.EffectSeverity>();
        public List<Skill.StatChange> Debuffs = new List<Skill.StatChange>();
        public List<int> DebuffDurationRemaining = new List<int>();
        public List<Skill.EffectSeverity> DebuffSeverity = new List<Skill.EffectSeverity>();
        public bool CanUseTurn { get; set; }
        //
        public Entity(string name, int turnOrder, int hp, int atk, int def, int mp = 0, int mag = 0, int spr = 0)
        {
            Name = name;
            _turnOrder = turnOrder;
            IsDefending = false;
            BaseHp = hp;
            HP = hp;
            BaseAtk = atk;
            ATK = atk;
            BaseDef = def;
            DEF = def;
            BaseMp = mp;
            MP = mp;
            BaseMag = mag;
            MAG = mag;
            BaseSpr = spr;
            SPR = spr;
            CanUseTurn = true;
        }
        //
        public abstract void ExecuteTurn(Battle battle);
        protected void Attack(Entity target, Battle battle)
        {
            Console.WriteLine("> {0} attacks {1}", Name, target.Name);
            Thread.Sleep(1500);
            Console.WriteLine("");
            Random rnd = new Random();
            int damage = 0;
            int weaponDamage = Weapon.ATK + rnd.Next(-Weapon.Varience, Weapon.Varience);
            damage = ATK + weaponDamage - target.DEF;
            if (target.IsDefending) { damage = Convert.ToInt32(damage * 0.5); }
            if (damage == 0) { damage = 1; }
            int missChance = 15;    // 15% chance to miss, will be different and varied depending on stat settings
            int hit = rnd.Next(1, 100);
            if (hit > missChance)    //if the attack hits
            {
                target.HP -= damage;
                Console.WriteLine("{0}'s attack hits {1} for {2} damage. {1}'s HP now {3}/{4}", Name, target.Name, damage, target.HP, target.BaseHp);     //eg: Jack's attack hits Goblin for 30 damage. Goblin's HP now 15/50
                if(target.HP == 0)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("{0} has been defeated!", target.Name);
                    battle.RemoveEntity(target);
                }
            }
            else { Console.WriteLine("The attack misses"); }
        }
        protected void UseSkill(Battle battle, Skill s, Entity target = null)
        {
            if (target == null || target == this) { Console.WriteLine("{0} uses {1}", Name, s.Name); }     //if the target is all enemies, all players or self, don't specify target
            else { Console.WriteLine("{0} uses {1} on {2}", Name, s.Name, target.Name); }
            Thread.Sleep(1500);
            Random rnd = new Random();
            _mp -= s.MPCost;
            int hit;
            List<bool> multiHits = new List<bool>();
            bool hitAny = false;
            if((target != null && target != this) || s.MultiAllOrNothing == true)
            {
                hit = rnd.Next(1, 100);
                if(hit > s.MissChance) { hitAny = true; }
            }
            else
            {
                if(s.TargetOptions == Skill.SkillTarget.TargetAllEnemies)
                {
                    if(GetType() == typeof(Player))
                    {
                        foreach(Enemy e in battle.Enemies)
                        {
                            hit = rnd.Next(1, 100);
                            if(hit > s.MissChance) { multiHits.Add(true); }
                            else { multiHits.Add(false); }
                        }
                    }
                    else
                    {
                        foreach(Player p in battle.Players)
                        {
                            hit = rnd.Next(1, 100);
                            if(hit > s.MissChance) { multiHits.Add(true); }
                            else { multiHits.Add(false); }
                        }
                    }
                }
                else    //if target type is "Target all players"
                {
                    if(GetType() == typeof(Player))
                    {
                        foreach(Player p in battle.Players)
                        {
                            hit = rnd.Next(1, 100);
                            if(hit > s.MissChance) { multiHits.Add(true); }
                            else { multiHits.Add(false); }
                        }
                    }
                    else
                    {
                        foreach(Enemy e in battle.Enemies)
                        {
                            hit = rnd.Next(1, 100);
                            if(hit > s.MissChance) { multiHits.Add(true); }
                            else { multiHits.Add(false); }
                        }
                    }
                }
                foreach(bool h in multiHits)    //loop determines if *any* of the oposing side got hit. Breaks the loops as soon as one hit is found.   (h is for hit, btw)
                {
                    if(h == true)
                    {
                        hitAny = true;
                        break;
                    }
                }
            }
            if (!hitAny)
            {
                if(s.TargetOptions == Skill.SkillTarget.TargetAllEnemies || s.TargetOptions == Skill.SkillTarget.TargetAllFriends)
                {
                    Console.WriteLine("{0}'s {1} missed everyone", Name, s.Name);
                }
                else { Console.WriteLine("{0}'s {1} missed", Name, s.Name); }
                return;
            }
            //
            //At this point all missed skill uses have been dealt with and multi hits have been determined
            //
            if(s.GetType() == typeof(PhysSkill) && target != null)  //this handles *all* single target physical attacks
            {
                int damage = Convert.ToInt32(s.ATK + 0.75 * _atk + rnd.Next(0, Convert.ToInt32(_atk * 0.5)) - target.DEF * 0.7);   //damage of a physical skill is the skill's attack stat, plus a portion of the player's attack stat, plus a number anywhere between 0 and half of their attack stat (i.e. it can recieve anywhere form 75% to 125% of player attack as a bonus), minus 70% of the target's defense stat
                if (target.IsDefending) { damage = Convert.ToInt32(damage * 0.65); }
                target.HP -= damage;
                Console.WriteLine("{0}'s {1} hits {2} for {3} damage. {2}'s HP now {4}/{5}", Name, s.Name, target.Name, damage, target.HP, target.BaseHp);  //eg: Jack's Heavy Slash hits Goblin for 45 damage. Goblin's HP now 5/50
                if (target.HP == 0)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("{0} has been defeated!", target.Name);
                    battle.RemoveEntity(target);
                }
                else { EffectApplier(s, target); }
                return;
            }
            if (s.GetType() == typeof(MagSkill) && target != null)  //this handles *all* single target magic attacks
            {
                int damage = Convert.ToInt32(s.MAG + 0.25 * _mag + rnd.Next(0, Convert.ToInt32(_mag * 1.5)) - target.SPR * 0.6);   //damage of a magic skill is the skill's magic stat, plus a portion of the player's magic stat, plus a number anywhere between 0 and 1.5x their magic stat (i.e. it can recieve anywhere form 25% to 175% of player magic as a bonus), minus 60% of the target's resistance
                if (target.IsDefending) { damage = Convert.ToInt32(damage * 0.85); }
                target.HP -= damage;
                Console.WriteLine("{0}'s {1} hits {2} for {3} damage. {2}'s HP now {4}/{5}", Name, s.Name, target.Name, damage, target.HP, target.BaseHp);  //eg: Jack's Heavy Slash hits Goblin for 45 damage. Goblin's HP now 5/50
                if (target.HP == 0)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("{0} has been defeated!", target.Name);
                    battle.RemoveEntity(target);
                }
                return;
            }
            List<Entity> deaths = new List<Entity>();
            /*single target buffs/debuffs*/
            if (s.GetType() == typeof(PhysSkill))
            {
                if(GetType() == typeof(Player)) //multi-target physical and magic attacks always hit oposing side, therefore target is Enemies
                {
                    if (s.MultiAllOrNothing)
                    {
                        foreach(Enemy e in battle.Enemies) { multiHits.Add(true); }
                    }
                    int enemyIterate = 0;
                    int damage;
                    foreach(Enemy e in battle.Enemies)
                    {
                        if(multiHits[enemyIterate] == false)
                        {
                            Console.WriteLine("{0}'s {1} misses {2}", Name, s.Name, e.Name);
                            Thread.Sleep(700);
                            continue;
                        }
                        damage = Convert.ToInt32(s.ATK + 0.75 * _atk + rnd.Next(0, Convert.ToInt32(_atk * 0.5)) - e.DEF * 0.7);   //damage of a physical skill is the skill's attack stat, plus a portion of the player's attack stat, plus a number anywhere between 0 and half of their attack stat (i.e. it can recieve anywhere form 75% to 125% of player attack as a bonus), minus 70% of the target's defense stat
                        if (e.IsDefending) { damage = Convert.ToInt32(damage * 0.65); }
                        e.HP -= damage;
                        if(e.HP == 0) { deaths.Add(e); }
                        Console.WriteLine("{0}'s {1} hits {2} for {3} damage. {2}'s HP is now {4}/{5}", Name, s.Name, e.Name, damage, e.HP, e.BaseHp);
                        Thread.Sleep(700);
                        enemyIterate++;
                    }
                }
                else /*enemy targeting all players*/
                {
                    if (s.MultiAllOrNothing)
                    {
                        foreach (Player p in battle.Players) { multiHits.Add(true); }
                    }
                    int playerIterate = 0;
                    int damage;
                    foreach (Player p in battle.Players)
                    {
                        if (multiHits[playerIterate] == false)
                        {
                            Console.WriteLine("{0}'s {1} misses {2}", Name, s.Name, p.Name);
                            Thread.Sleep(700);
                            continue;
                        }
                        damage = Convert.ToInt32(s.ATK + 0.75 * _atk + rnd.Next(0, Convert.ToInt32(_atk * 0.5)) - p.DEF * 0.7);   //damage of a physical skill is the skill's attack stat, plus a portion of the player's attack stat, plus a number anywhere between 0 and half of their attack stat (i.e. it can recieve anywhere form 75% to 125% of player attack as a bonus), minus 70% of the target's defense stat
                        if (p.IsDefending) { damage = Convert.ToInt32(damage * 0.65); }
                        p.HP -= damage;
                        if(p.HP == 0) { deaths.Add(p); }
                        Console.WriteLine("{0}'s {1} hits {2} for {3} damage. {2}'s HP is now {4}/{5}", Name, s.Name, p.Name, damage, p.HP, p.BaseHp);
                        Thread.Sleep(700);
                        playerIterate++;
                    }
                }
            }
            else if(s.GetType() == typeof(MagSkill))
            {
                if (GetType() == typeof(Player)) //multi-target physical and magic attacks always hit oposing side, therefore target is Enemies
                {
                    if (s.MultiAllOrNothing)
                    {
                        foreach (Enemy e in battle.Enemies) { multiHits.Add(true); }
                    }
                    int enemyIterate = 0;
                    int damage;
                    foreach (Enemy e in battle.Enemies)
                    {
                        if (multiHits[enemyIterate] == false)
                        {
                            Console.WriteLine("{0}'s {1} misses {2}", Name, s.Name, e.Name);
                            Thread.Sleep(700);
                            continue;
                        }
                        damage = Convert.ToInt32(s.MAG + 0.25 * _mag + rnd.Next(0, Convert.ToInt32(_mag * 1.5)) - e.SPR * 0.6);   //damage of a magic skill is the skill's magic stat, plus a portion of the player's magic stat, plus a number anywhere between 0 and 1.5x their magic stat (i.e. it can recieve anywhere form 25% to 175% of player magic as a bonus), minus 60% of the target's resistance
                        if (e.IsDefending) { damage = Convert.ToInt32(damage * 0.85); }
                        e.HP -= damage;
                        if(e.HP == 0) { deaths.Add(e); }
                        Console.WriteLine("{0}'s {1} hits {2} for {3} damage. {2}'s HP is now {4}/{5}", Name, s.Name, e.Name, damage, e.HP, e.BaseHp);
                        Thread.Sleep(700);
                        enemyIterate++;
                    }
                }
                else /*enemy targeting all players*/
                {
                    if (s.MultiAllOrNothing)
                    {
                        foreach (Player p in battle.Players) { multiHits.Add(true); }
                    }
                    int playerIterate = 0;
                    int damage;
                    foreach (Player p in battle.Players)
                    {
                        if (multiHits[playerIterate] == false)
                        {
                            Console.WriteLine("{0}'s {1} misses {2}", Name, s.Name, p.Name);
                            Thread.Sleep(700);
                            continue;
                        }
                        damage = Convert.ToInt32(s.MAG + 0.25 * _mag + rnd.Next(0, Convert.ToInt32(_mag * 1.5)) - p.SPR * 0.6);   //damage of a magic skill is the skill's magic stat, plus a portion of the player's magic stat, plus a number anywhere between 0 and 1.5x their magic stat (i.e. it can recieve anywhere form 25% to 175% of player magic as a bonus), minus 60% of the target's resistance
                        if (p.IsDefending) { damage = Convert.ToInt32(damage * 0.85); }
                        p.HP -= damage;
                        if(p.HP == 0) { deaths.Add(p); }
                        Console.WriteLine("{0}'s {1} hits {2} for {3} damage. {2}'s HP is now {4}/{5}", Name, s.Name, p.Name, damage, p.HP, p.BaseHp);
                        Thread.Sleep(700);
                        playerIterate++;
                    }
                }
            }
            else /*(buff/debuff)*/
            {
                throw new NotImplementedException();
            }
            Console.WriteLine("");
            if(deaths.Count != 0) { Thread.Sleep(1000); }
            foreach (Entity e in deaths)
            {
                Thread.Sleep(500);
                Console.WriteLine("{0} has been defeated!", e.Name);
                battle.RemoveEntity(e);
            }
        }
        /// <summary>
        /// Applies a skill's status effect
        /// </summary>
        /// <param name="s">The skill being used</param>
        /// <param name="target">The target the skill is being used on</param>
        private void EffectApplier(Skill s, Entity target)
        {
            Random rnd = new Random();
            int effectHit = rnd.Next(1, 100);
            if(effectHit > s.EffectChance) { return; }  //if the random number is higher than the chance of the effect hitting, it misses, and so we return from the method
            target.StatusEffect = s.Effect;
            target.EffectSeverity = s.Severity;
            switch (s.Effect)
            {
                case Skill.StatusEffect.none:
                    return;
                case Skill.StatusEffect.poison:
                    target.EffectDurationRemaining = s.EffectLength + rnd.Next(-1, 3);
                    Console.WriteLine("{0} has been poisoned", target.Name);
                    break;
                case Skill.StatusEffect.burn:
                    target.EffectDurationRemaining = s.EffectLength + rnd.Next(-1, 1);
                    Console.WriteLine("{0} has been burned", target.Name);
                    break;
                case Skill.StatusEffect.freeze:
                    target.EffectDurationRemaining = s.EffectLength + rnd.Next(-1, 1);
                    Console.WriteLine("{0} has been encased in ice", target.Name);
                    break;
                case Skill.StatusEffect.stun:
                    target.EffectDurationRemaining = s.EffectLength + rnd.Next(-1, 2);
                    Console.WriteLine("{0} is seeing stars", target.Name);
                    break;
                case Skill.StatusEffect.confusion:
                    target.EffectDurationRemaining = s.EffectLength + rnd.Next(-1, 4);
                    Console.WriteLine("{0} doesn't know right from left", target.Name);
                    break;
                case Skill.StatusEffect.fear:
                    target.EffectDurationRemaining = s.EffectLength + rnd.Next(-1, 1);
                    Console.WriteLine("{0} is afraid for their life", target.Name);
                    break;
                default:
                    throw new Exception();  // this will only be called if the enum is expanded and not handled by this switch
            }
        }
        /// <summary>
        /// This functions processes necessary handling for effect duration and statements, as well as handling damage for poison and burn
        /// </summary>
        protected void EffectHandler()
        {
            if(StatusEffect == Skill.StatusEffect.none) { return; }
            Random rnd = new Random();
            int damageMod;
            int varience;
            int damage;
            EffectDurationRemaining--;
            switch (StatusEffect)
            {
                case Skill.StatusEffect.poison:
                    if(EffectSeverity == Skill.EffectSeverity.light) { damageMod = 10; varience = 7; }              // 3-17 damage
                    else if(EffectSeverity == Skill.EffectSeverity.moderate) { damageMod = 35; varience = 12; }     // 23-47 damage
                    else /*heavy*/ { damageMod = 70; varience = 20; }                                               // 50-90 damage
                    damage = damageMod + rnd.Next(-varience, varience);
                    if (HP - damage < 1) { HP = 1; }
                    else { HP -= damage; }
                    Console.WriteLine("{0} takes {1} damage from poison. HP now {2}/{3}", Name, damage, HP, BaseHp);
                    if(EffectDurationRemaining <= 0)
                    {
                        Thread.Sleep(700);
                        Console.WriteLine("{0} is no longer poisoned", Name);
                    }
                    break;
                case Skill.StatusEffect.burn:
                    if (EffectSeverity == Skill.EffectSeverity.light) { damageMod = 27; varience = 10; }            // 17-37 damage
                    if (EffectSeverity == Skill.EffectSeverity.moderate) { damageMod = 63; varience = 20; }         // 43-83 damage
                    else /*heavy*/ { damageMod = 117; varience = 23; }                                              // 94-140 damage
                    damage = damageMod + rnd.Next(-varience, varience);
                    HP -= damage;
                    Console.WriteLine("{0} takes {1} damage from . HP now {2}/{3}", Name, damage, HP, BaseHp);
                    if (EffectDurationRemaining <= 0)
                    {
                        Thread.Sleep(700);
                        Console.WriteLine("{0} is no longer burned", Name);
                    }
                    break;
                case Skill.StatusEffect.freeze:
                    if (EffectDurationRemaining > 0) { Console.WriteLine("{0} is frozen in place", Name); }
                    else { Console.WriteLine("{0} is no longer frozen", Name); }
                    break;
                case Skill.StatusEffect.stun:
                    if (EffectDurationRemaining > 0) { Console.WriteLine("{0} is still feeling dizzy", Name); }
                    else { Console.WriteLine("{0} is no longer stunned", Name); }
                    break;
                case Skill.StatusEffect.fear:
                    if (EffectDurationRemaining <= 0)
                    {
                        Thread.Sleep(700);
                        Console.WriteLine("{0} is no longer afraid", Name);
                    }
                    break;
                case Skill.StatusEffect.confusion:
                    if (EffectDurationRemaining <= 0)
                    {
                        Thread.Sleep(700);
                        Console.WriteLine("{0} is no longer confused", Name);
                    }
                    break;
            }
            if (EffectDurationRemaining <= 0) { StatusEffect = Skill.StatusEffect.none; }
        }
    }
}
