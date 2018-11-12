using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RPGenius
{
    /// <summary>
    /// An enemy. Controlled by rather simple logic
    /// </summary>
    class Enemy : Entity
    {
        //private Player _lastAttackedBy; //consider using something like this to increase complexity of enemy target selection
        //
        /// <summary>
        /// Creates a Enemy
        /// </summary>
        /// <param name="name">The name of the Enemy</param>
        /// <param name="turnOrder">The enemy's position in the turn order</param>
        /// <param name="hp">The enemy's base hp</param>
        /// <param name="atk">The enemy's base attack</param>
        /// <param name="def">The enemy's base defence</param>
        /// <param name="mp">The enemy's base mp</param>
        /// <param name="mag">The enemy's base magic</param>
        /// <param name="spr">The enemy's base resistance</param>
        public Enemy(string name, int turnOrder, int hp, int atk, int def, int mp = 0, int mag = 0, int spr = 0) : base(name, turnOrder, hp, atk, def, mp, mag, spr) { }
        //
        public override void ExecuteTurn(Battle battle)
        {
            Thread.Sleep(2000);
            IsDefending = false;
            if (Effect != null) { Effect.Handle(this, 1); }
            if (CanUseTurn)
            {
                Random rnd = new Random();
                int attackChance = HP / BaseHp * 100;     //Chance of attack is higher if health is higher. (represented as an integer percentage)
                if (attackChance > 90) { attackChance = 90; }           //This statement ensures the chance of defense is always at least 10%
                else if (attackChance > 70) { attackChance = 80; }      // This statement is just a flavour statment, this means that when the enemy is between 70% and 90% of their health, they'll have an 80% chance of attacking
                else if (attackChance < 50) { attackChance = 50; }      //this statement means that as soon as the enemy drops below 50% health, they will always have only a 50% chance of attacking
                int choice = rnd.Next(1, 101);     //Generates a random integer between 1 and 100 inclusive
                if (choice <= attackChance)
                {
                    int target;
                    if (Skills.Count == 0)
                    {
                        target = rnd.Next(1, battle.PlayerCount + 1);   // randomly chooses a player to attack, each with equal likelyhood  -> maybe make it more inclined to attack whoever hit it last, or something even more complicated
                        Attack(battle.Players[target - 1], battle);  //If the random number isn't high enough to trump the chance of attacking, the enemy will attack
                    }
                    else
                    {
                        int skillChance = /*25*/ 100;
                        if (rnd.Next(1, 101) <= skillChance)
                        {
                            Skill skillChoice1 = Skills[rnd.Next(0, Skills.Count)];
                            if (skillChoice1.MPCost <= MP)
                            {
                                Entity[] skillTargets = skillChoice1.UseMethod.Target(this, battle);
                                Entity[] deaths = skillChoice1.UseMethod.Use(this, skillTargets);
                                if (deaths != null) { battle.HandleDeaths(deaths); }
                            }
                            else
                            {
                                Skill skillChoice2 = Skills[rnd.Next(0, Skills.Count)];
                                if (skillChoice2.MPCost <= MP)
                                {
                                    Entity[] skillTargets = skillChoice2.UseMethod.Target(this, battle);
                                    Entity[] deaths = skillChoice2.UseMethod.Use(this, skillTargets);
                                    if (deaths != null) { battle.HandleDeaths(deaths); }
                                }
                            }
                        }
                        else
                        {
                            target = rnd.Next(1, battle.PlayerCount + 1);   // randomly chooses a player to attack, each with equal likelyhood  -> maybe make it more inclined to attack whoever hit it last, or something even more complicated
                            Attack(battle.Players[target - 1], battle);  //If the random number isn't high enough to trump the chance of attacking, the enemy will attack
                        }
                    }
                }
                else
                {
                    IsDefending = true;
                    Console.WriteLine("> {0} defends", Name);
                    Console.WriteLine("");
                }
            }
            if (Effect != null) { Effect.Handle(this, 3); }
            List<IEffectOrBuff> toIterate = StatChanges.ToList();    //this allows for statchange.handle to remove statchange onjects from entity.statchanges, without affecting the list the loop iterates on
            foreach (IEffectOrBuff s in toIterate) { s.Handle(this, 3); }
            HaveTurnLater = false;
            CanUseTurn = true;
            Afraid = false;
            Confused = false;
        }
    }
}
