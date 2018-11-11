using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGenius
{
    /// <summary>
    /// A player, controlled by user input
    /// </summary>
    class Player : Entity
    {
        /// <summary>
        /// Creates a player
        /// </summary>
        /// <param name="name">The name of the Player</param>
        /// <param name="turnOrder">The player's position in the turn order</param>
        /// <param name="hp">The player's base hp</param>
        /// <param name="atk">The player's base attack</param>
        /// <param name="def">The player's base defence</param>
        /// <param name="mp">The player's base mp</param>
        /// <param name="mag">The player's base magic</param>
        /// <param name="spr">The player's base resistance</param>
        public Player(string name, int turnOrder, int hp, int atk, int def, int mp = 0, int mag = 0, int spr = 0) : base(name, turnOrder, hp, atk, def, mp, mag, spr) { }
        //
        public override void ExecuteTurn(Battle battle)
        {
            IsDefending = false;
            if (Effect != null) { Effect.Handle(this, 1); }
            if (CanUseTurn)
            {
                int choice;
                Console.WriteLine("");
                Console.WriteLine("What will {0} do?\nHP: {1}/{2}\tMP: {3}/{4}{5}\n", Name, HP, BaseHp, MP, BaseMp, EffectString);
                do
                {
                    Console.Write("\t1. Attack\t2. Defend");
                    if (Skills.Count != 0) { Console.Write("\t3. Skills"); }     //this isn't ok if any more options are added, as if we have access to the [fourth option], but not skills, the [fourth option] will, in fact be the third option
                    Console.WriteLine("");
                    Console.Write("\t=>  ");
                    int upperLimit = 2;     //there will always be at least two options (attack and defend), but there may be more (like skills)
                    if (Skills.Count != 0) { upperLimit++; }
                    choice = ExSys.ReadIntRange(1, upperLimit);
                    Console.WriteLine("");
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("Who would you like to attack?");
                            int enemyIterate = 1;
                            foreach (Enemy e in battle.Enemies)
                            {
                                Console.WriteLine("\t{0}. {1}\t{2}/{3} HP{4}", enemyIterate, e.Name, e.HP, e.BaseHp, e.EffectString);   // eg:  1. Goblin   45/50 HP
                                enemyIterate++;
                            }
                            Console.WriteLine("\t{0}. [back]", enemyIterate);
                            Console.Write("\t\t=>  ");
                            choice = ExSys.ReadIntRange(1, enemyIterate);
                            if (choice != enemyIterate) { Attack(battle.Enemies[choice - 1], battle); }    //if player's choice isn't "[back]", attack the target
                            else { choice = 0; }    //else, if the choice is to go back, set choice to zero so that the loop triggers
                            break;
                        case 2:
                            IsDefending = true;
                            Console.WriteLine("> {0} defends", Name);
                            Console.WriteLine("");
                            break;
                        case 3:
                            Console.WriteLine("Which skill would you like to use?  (You have {0}MP)", MP);
                            int skillsIterate = 1;
                            foreach (Skill s in Skills)
                            {
                                Console.WriteLine("\t{0}. ({1} MP)  {2}", skillsIterate, s.MPCost, s.Name);   // eg:  1. (30 MP)  Heavy slash
                                skillsIterate++;
                            }
                            Console.WriteLine("\t{0}. [back]", skillsIterate);
                            Console.Write("\t\t=>  ");
                            choice = ExSys.ReadIntRange(1, skillsIterate);
                            if (choice != skillsIterate)
                            {
                                Skill chosenSkill = Skills[choice - 1];
                                if (chosenSkill.MPCost > MP)
                                {
                                    Console.WriteLine("**You don't have enough MP to use {0}", chosenSkill.Name);
                                    choice = 0;
                                }
                                else
                                {
                                    Entity[] skillTargets = chosenSkill.UseMethod.Target(this, battle);
                                    if (skillTargets != null)
                                    {
                                        Entity[] deaths = chosenSkill.UseMethod.Use(this, skillTargets);
                                        if (deaths != null) { battle.HandleDeaths(deaths); }
                                    }
                                    else { choice = 0; }
                                }
                            }
                            else { choice = 0; }
                            break;
                    }
                } while (choice == 0);
            }
            if (Effect != null) { Effect.Handle(this, 3); }
            foreach (IEffectOrBuff s in StatChanges) { s.Handle(this, 3); }
            HaveTurnLater = false;
            CanUseTurn = true;
            Afraid = false;
            Confused = false;
        }
    }
}
