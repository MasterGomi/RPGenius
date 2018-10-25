using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGenius
{
    class Player : Entity
    {
        //
        public Player(string name, int turnOrder, int hp, int atk, int def, int mp = 0, int mag = 0, int spr = 0) : base(name, turnOrder, hp, atk, def, mp, mag, spr) { }
        //
        public override void ExecuteTurn(Battle battle)
        {
            IsDefending = false;
            if (StatusEffect == Skill.StatusEffect.burn || StatusEffect == Skill.StatusEffect.freeze) { EffectHandler(); }
            int choice;
            Console.WriteLine("");
            
            Console.WriteLine("What will {0} do?\nHP: {1}/{2}\tMP: {3}/{4}{5}\n", Name, HP, BaseHp, MP, BaseMp, DisplayEffect());
            do
            {
                Console.Write("1. Attack\t2. Defend");
                if(Skills.Count != 0) { Console.Write("\t3. Skills"); }     //this isn't ok if any more options are added, as if we have access to the [fourth option], but not skills, the [fourth option] will, in fact be the third option
                Console.WriteLine("");
                Console.Write("\t=>  ");
                int upperLimit = 2;     //there will always be at least two options (attack and defend), but there may be more (like skills)
                if(Skills.Count != 0) { upperLimit++; }
                choice = ExSys.ReadIntRange(1, upperLimit);
                Console.WriteLine("");
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Who would you like to attack?");
                        int enemyIterate = 1;
                        foreach (Enemy e in battle.Enemies)
                        {
                            Console.WriteLine("{0}. {1}\t{2}/{3} HP", enemyIterate, e.Name, e.HP, e.BaseHp);   // eg:  1. Goblin   45/50 HP
                            enemyIterate++;
                        }
                        Console.WriteLine("{0}. [back]", enemyIterate);
                        Console.Write("\t=>  ");
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
                        Console.WriteLine("Which skill would you like to use?");
                        int skillsIterate = 1;
                        foreach (Skill s in Skills)
                        {
                            Console.WriteLine("{0}. ({1} MP)  {2}", skillsIterate, s.MPCost, s.Name);   // eg:  1. (30 MP)  Heavy slash
                            skillsIterate++;
                        }
                        Console.WriteLine("{0}. [back]",skillsIterate);
                        Console.Write("\t=>  ");
                        choice = ExSys.ReadIntRange(1, skillsIterate);
                        if (choice != skillsIterate)
                        {
                            if (Skills[choice - 1].MPCost > MP)
                            {
                                Console.WriteLine("You don't have enough MP to use {0}", Skills[choice - 1].Name);
                                choice = 0;
                            }
                            else
                            {
                                choice = ChooseSkillTarget(Skills[choice - 1], battle);
                            }
                        }
                        else { choice = 0; }
                        break;
                }
            } while (choice == 0);
            if(StatusEffect == Skill.StatusEffect.poison) { EffectHandler(); }
        }
        /// <summary>
        /// Determines (using user input) which target is to be used by the skill
        /// </summary>
        /// <param name="s">The skill that needs a target</param>
        /// <param name="battle">The current Battle object</param>
        /// <returns>
        /// Returns 1 after succesful execution. Returns 0 if player chooses [back]. 
        /// Throws a hard-coded NullReferenceException if TargetOptions isn't playing nice and 
        /// the program doesn't automatically break when starting the switch
        /// </returns>
        private int ChooseSkillTarget(Skill s, Battle battle)
        {
            int choice;
            switch (s.TargetOptions)
            {
                case Skill.SkillTarget.TargetOneEnemy:
                    Console.WriteLine("Who would you like to target?");
                    int enemyIterate = 1;
                    foreach (Enemy e in battle.Enemies)
                    {
                        Console.WriteLine("{0}. {1}\t{2}/{3} HP", enemyIterate, e.Name, e.HP, e.BaseHp);   // eg:  1. Goblin   45/50 HP
                        enemyIterate++;
                    }
                    Console.WriteLine("{0}. [back]", enemyIterate);
                    Console.Write("\t=>  ");
                    choice = ExSys.ReadIntRange(1, enemyIterate);
                    if(choice == enemyIterate) { return 0; }
                    UseSkill(battle, s, battle.Enemies[choice - 1]);
                    return 1;
                case Skill.SkillTarget.TargetAllEnemies:
                    UseSkill(battle, s);
                    return 1;
                case Skill.SkillTarget.TargetOneFriend:
                    Console.WriteLine("Who would you like to target?");
                    int playerIterate = 1;
                    foreach (Player p in battle.Players)
                    {
                        Console.WriteLine("{0}. {1}\t{2}/{3} HP", playerIterate, p.Name, p.HP, p.BaseHp);   // eg:  1. Jack   45/50 HP
                        playerIterate++;
                    }
                    Console.WriteLine("{0}. [back]", playerIterate);
                    Console.Write("\t=>  ");
                    choice = ExSys.ReadIntRange(1, playerIterate);
                    if (choice == playerIterate) { return 0; }
                    UseSkill(battle, s, battle.Players[choice - 1]);
                    return 1;
                case Skill.SkillTarget.TargetAllFriends:
                    UseSkill(battle, s);
                    return 1;
                case Skill.SkillTarget.TargetSelf:
                    UseSkill(battle, s, this);
                    return 1;
                default: throw new NullReferenceException();
            }
        }
        /// <summary>
        /// Determines what needs to be shown to the player to inform them of their status effect. Returns it as a string.
        /// </summary>
        /// <returns>Returns a string with relevant status notification. Returns an empty string if not required</returns>
        private string DisplayEffect()
        {
            switch (StatusEffect)
            {
                case Skill.StatusEffect.none:
                case Skill.StatusEffect.freeze:     //this also returns an empty string as this function should never be called when frozen, as the player doesn't get their turn
                    return "";
                case Skill.StatusEffect.poison:
                    return "\t*poisoned*";
                case Skill.StatusEffect.burn:
                    return "\t*burned*";
                case Skill.StatusEffect.fear:
                    return "\t*afraid";
                case Skill.StatusEffect.confusion:
                    return "\t*confused";
                case Skill.StatusEffect.stun:
                    return "\t*stunned*";
                default:
                    throw new NullReferenceException();
            }
        }
    }
}
