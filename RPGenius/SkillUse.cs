using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RPGenius
{
    /// <summary>
    /// Class for which various skill attack and targeting methods inherit from
    /// </summary>
    abstract class SkillUse
    {
        public SkillUse(Skill s) { _skill = s; }
        //
        /// <summary>
        /// Returns an array of Entities for which the skill targets (contains necessary user input handling if user is a Player)
        /// </summary>
        /// <param name="user">The entity using the skill</param>
        /// <param name="battle">The battle object</param>
        /// <returns>Array of targeted entities</returns>
        public abstract Entity[] Target(Entity user, Battle battle);
        /// <summary>
        /// Executes the skill's intended function on the entities passed
        /// </summary>
        /// <param name="user">The entity using the skill</param>
        /// <param name="targets">An array of entities being targeted</param>
        /// <returns>Array of defeated entities</returns>
        public abstract Entity[] Use(Entity user, Entity[] targets);
        protected Skill _skill;
        protected Random rnd = new Random();
    }
    //
    //
    /// <summary>
    /// Targeting and use methods for skills that can target one friend
    /// </summary>
    class OneFriend : SkillUse
    {
        public OneFriend(Skill s) : base(s) { }
        //
        public override Entity[] Target(Entity user, Battle battle)
        {
            if (user is Player)
            {
                Console.WriteLine("Who would you like to target?");
                int playerIterate = 1;
                foreach (Player p in battle.Players)
                {
                    Console.WriteLine("\t{0}. {1}\t{2}/{3} HP{4}", playerIterate, p.Name, p.HP, p.BaseHp, p.EffectString);   // eg:  1. Jack   45/50 HP    *poisoned*  *ATK down*
                    playerIterate++;
                }
                Console.WriteLine("\t{0}. [back]", playerIterate);
                Console.Write("\t\t=>  ");
                int choice;
                choice = ExSys.ReadIntRange(1, playerIterate);
                if (choice == playerIterate) { return null; }
                else return new Entity[] { battle.Players[choice - 1] };
            }
            else //enemy
            {
                Enemy target = battle.Enemies[rnd.Next(0, battle.EnemyCount)];
                return new Entity[] { target };
            }
        }
        public override Entity[] Use(Entity user, Entity[] targets)
        {
            //it is implied, given that it targets friends, this this is a support skill of some description
            //i.e. it is either to relieve a friend of a status effect or to buff a friend
            //furthermore, as this is a single target, it is implied that "targets" only has one member
            //
            Console.WriteLine("> {0} uses {1} on {2}", user.Name, _skill.Name, targets[0].Name);       //e.g. Jack uses Cure Poison on Dan
            Thread.Sleep(400);
            if (_skill.Effect is StatusEffect)
            {
                if (targets[0].Effect != null)
                {
                    if (targets[0].Effect.GetType() == _skill.Effect.GetType())
                    {
                        targets[0].Effect = null;
                        StatusEffect effect = targets[0].Effect as StatusEffect;
                        Console.WriteLine("> {0} is no longer {1}", targets[0].Name, effect.RemoveTerm);       //e.g. Dan's Poison has been removed
                    }
                    else { Console.WriteLine("> It had no effect"); }
                }
                else { Console.WriteLine("> It had no effect"); }
            }
            else //Buff
            {
                //implement after implementing buffs
            }
            return null;
        }
    }
    //
    //
    /// <summary>
    /// Targeting and use methods for skills that can target one enemy
    /// </summary>
    class OneEnemy : SkillUse
    {
        public OneEnemy(Skill s) : base(s) { }
        //
        public override Entity[] Target(Entity user, Battle battle)
        {
            if (user is Player)
            {
                Console.WriteLine("Who would you like to target?");
                int enemyIterate = 1;
                foreach (Enemy e in battle.Enemies)
                {
                    Console.WriteLine("\t{0}. {1}\t{2}/{3} HP{4}", enemyIterate, e.Name, e.HP, e.BaseHp, e.EffectString);   // eg:  1. goblin   13/45 HP    *stunned*  *DEF up*
                    enemyIterate++;
                }
                Console.WriteLine("\t{0}. [back]", enemyIterate);
                Console.Write("\t\t=>  ");
                int choice;
                choice = ExSys.ReadIntRange(1, enemyIterate);
                if (choice == enemyIterate) { return null; }
                else return new Entity[] { battle.Enemies[choice - 1] };
            }
            else //enemy
            {
                Player target = battle.Players[rnd.Next(0, battle.PlayerCount)];
                return new Entity[] { target };
            }
        }
        public override Entity[] Use(Entity user, Entity[] targets)
        {
            //it is implied, given that it targets enemies, that this is an offensive skill
            //i.e. a attacks b
            //or a support skill of some description
            //i.e. to apply a status effect or debuff
            //furthermore, as this is a single target, it is implied that "targets" only has one member
            //
            Console.WriteLine("> {0} uses {1} on {2}", user.Name, _skill.Name, targets[0].Name);       //e.g. Jack uses Poison strike on Goblin
            Thread.Sleep(400);
            if (_skill is OffensiveSkill) { return Attack(user, targets[0]); }
            else { Other(user, targets[0]); }
            return null;
        }
        private Entity[] Attack(Entity user, Entity target)
        {
            int hitCheck = rnd.Next(1, 101);
            if (hitCheck > _skill.MissChance)
            {
                int damage;
                if (_skill is PhysSkill)
                {
                    damage = Convert.ToInt32(_skill.ATK + 0.75 * user.ATK + rnd.Next(0, Convert.ToInt32(user.ATK * 0.5)) - target.DEF * 0.8);   //damage of a physical skill is the skill's attack stat, plus a portion of the player's attack stat, plus a number anywhere between 0 and half of their attack stat (i.e. it can recieve anywhere form 75% to 125% of player attack as a bonus), minus 80% of the target's defense stat
                    if (target.IsDefending) { damage = Convert.ToInt32(damage * 0.65); }
                }
                else
                {
                    damage = Convert.ToInt32(_skill.MAG + 0.25 * user.MAG + rnd.Next(0, Convert.ToInt32(user.MAG * 1.5)) - target.SPR * 0.8);   //damage of a magic skill is the skill's magic stat, plus a portion of the player's magic stat, plus a number anywhere between 0 and 1.5x their magic stat (i.e. it can recieve anywhere form 25% to 175% of player magic as a bonus), minus 80% of the target's resistance
                    if (target.IsDefending) { damage = Convert.ToInt32(damage * 0.85); }
                }
                target.HP -= damage;
                Console.WriteLine("> {0}'s {1} does {2} damage to {3}. HP now {4}/{5}", user.Name, _skill.Name, damage, target.Name, target.HP, target.BaseHp);        //e.g. Jack Poison strike does 30 damage to Goblin. HP now 15/45
                if (target.HP == 0) { return new Entity[] { target }; }
                else if (_skill.Effect != null) { Other(user, target); }
                return null;
            }
            else
            {
                Console.WriteLine("> {0}'s {1} missed", user.Name, _skill.Name);
                return null;
            }
        }
        private void Other(Entity user, Entity target)
        {
            int effectCheck = rnd.Next(1, 101);
            if (effectCheck <= _skill.EffectChance)
            {
                Thread.Sleep(400);
                _skill.Effect.Apply(target);
            }
            else if (_skill is SupportSkill) { Console.WriteLine("> {0}'s {1} missed"); }
        }
    }
    //
    /// <summary>
    /// Targeting and use methods for skills that can target all friends
    /// </summary>
    class AllFriends : SkillUse
    {
        public AllFriends(Skill s) : base(s) { }
        //
        public override Entity[] Target(Entity user, Battle battle)
        {
            if (user is Player) { return battle.Players.ToArray(); }
            else /*enemy*/ return battle.Enemies.ToArray();
        }
        public override Entity[] Use(Entity user, Entity[] targets)
        {
            Console.WriteLine("> {0} uses {1}", user.Name, _skill.Name);           //e.g. Dan uses End all fear
            Thread.Sleep(1200);
            if (_skill.Effect is StatusEffect)
            {
                for (int i = 0; i < targets.Length; i++)
                {
                    Thread.Sleep(300);
                    if (targets[i].Effect != null)
                    {
                        if (targets[i].Effect.GetType() == _skill.Effect.GetType())
                        {
                            targets[i].Effect = null;
                            StatusEffect effect = targets[i].Effect as StatusEffect;
                            Console.WriteLine("> {0} is no longer {1}", targets[i].Name, effect.RemoveTerm);       //e.g. Jack's Fear has been removed
                        }
                        else { Console.WriteLine("> It had no effect on {0}", targets[i].Name); }          //e.g. It had no effect on Dan
                    }
                    else { Console.WriteLine("> It had no effect on {0}", targets[i].Name); }
                }
            }
            else //Buff
            {
                //implement after implementing buffs
            }
            return null;
        }
    }
    //
    /// <summary>
    /// Targeting and use methods for skills that can target all enemies
    /// </summary>
    class AllEnemies : SkillUse
    {
        public AllEnemies(Skill s) : base(s) { }
        //
        public override Entity[] Target(Entity user, Battle battle)
        {
            if (user is Player) { return battle.Enemies.ToArray(); }
            else /*enemy*/ return battle.Players.ToArray();
        }
        //
        public override Entity[] Use(Entity user, Entity[] targets)
        {
            Console.WriteLine("> {0} uses {1}", user.Name, _skill.Name);       //e.g. Jack uses Reckless slash
            Thread.Sleep(400);
            if (_skill is OffensiveSkill) { return Attack(user, targets); }
            else { Other(user, targets); }
            return null;
        }
        private Entity[] Attack(Entity user, Entity[] targets)
        {
            bool hitAny = false;
            bool[] multiHits = new bool[targets.Length];
            if (_skill.MultiAllOrNothing)
            {
                int hitCheck = rnd.Next(1, 101);
                if (hitCheck > _skill.MissChance)
                {
                    hitAny = true;
                    for(int i = 0; i < targets.Length; i++)
                    {
                        multiHits[i] = true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < targets.Length; i++)
                {
                    int hitCheck = rnd.Next(1, 101);
                    if (hitCheck > _skill.MissChance)
                    {
                        multiHits[i] = true;
                        hitAny = true;
                    }
                }
            }
            if (!hitAny)
            {
                Console.WriteLine("\n> {0}'s {1} missed everyone", user.Name, _skill.Name);
                return null;
            }
            Console.WriteLine("");
            List<Entity> deaths = new List<Entity>();
            for (int i = 0; i < targets.Length; i++)
            {
                Thread.Sleep(700);
                if (multiHits[i])
                {
                    int damage;
                    if (_skill is PhysSkill)
                    {
                        damage = Convert.ToInt32(_skill.ATK + 0.75 * user.ATK + rnd.Next(0, Convert.ToInt32(user.ATK * 0.5)) - targets[i].DEF * 0.8);   //damage of a physical skill is the skill's attack stat, plus a portion of the player's attack stat, plus a number anywhere between 0 and half of their attack stat (i.e. it can recieve anywhere form 75% to 125% of player attack as a bonus), minus 80% of the target's defense stat
                        if (targets[i].IsDefending) { damage = Convert.ToInt32(damage * 0.65); }
                    }
                    else  // _skill is MagSkill
                    {
                        damage = Convert.ToInt32(_skill.MAG + 0.25 * user.MAG + rnd.Next(0, Convert.ToInt32(user.MAG * 1.5)) - targets[i].SPR * 0.8);   //damage of a magic skill is the skill's magic stat, plus a portion of the player's magic stat, plus a number anywhere between 0 and 1.5x their magic stat (i.e. it can recieve anywhere form 25% to 175% of player magic as a bonus), minus 80% of the target's resistance
                        if (targets[i].IsDefending) { damage = Convert.ToInt32(damage * 0.85); }
                    }
                    targets[i].HP -= damage;
                    Console.WriteLine("> {0}'s {1} hits {2} for {3} damage. HP now {4}/{5}", user.Name, _skill.Name, targets[i].Name, damage, targets[i].HP, targets[i].BaseHp);
                    if (targets[i].HP == 0) { deaths.Add(targets[i]); }
                }
                else
                {
                    Console.WriteLine("> {0}'s {1} missed {2}", user.Name, _skill.Name, targets[i].Name);
                }
            }
            Other(user, targets, multiHits);
            return deaths.Count == 0 ? null : deaths.ToArray(); // is there were no deaths, return null, else return deaths
        }
        private void Other(Entity user, Entity[] targets, bool[] multiHits = null)
        {
            if (multiHits == null)  //because of the nature of the skills passed, if it isn't an offensive skill, we don't need to check for physical contact, as it may apply anyway, so in that case we assume that it hit everyone
            {
                multiHits = new bool[targets.Length];
                for(int i = 0; i < targets.Length; i++) { multiHits[i] = true; }
            }
            for (int i = 0; i < targets.Length; i++)
            {
                Thread.Sleep(500);
                if (multiHits[i])
                {
                    int effectCheck = rnd.Next(1, 101);
                    if (effectCheck <= _skill.EffectChance && targets[i].HP != 0)
                    {
                        _skill.Effect.Apply(targets[i]);
                    }
                    else
                    {
                        if (_skill is SupportSkill)
                        {
                            Console.WriteLine("> {0}'s {1} missed {2}", user.Name, _skill.Name, targets[i].Name);
                        }
                    }
                }
            }
        }
    }
    //
    //
    /// <summary>
    /// Targeting and use methods for skills that can target oneself
    /// </summary>
    class Self : SkillUse
    {
        public Self(Skill s) : base(s) { }
        //
        public override Entity[] Target(Entity user, Battle battle)
        {
            return new Entity[] { user };
        }
        //
        public override Entity[] Use(Entity user, Entity[] targets)
        {
            _skill.Effect.Apply(user);
            return null;
        }
    }
}
