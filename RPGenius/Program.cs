using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGenius
{
    class Program
    {
        static void Main(string[] args)
        {
            Battle battle = new Battle();
            Sword ironSword = new Sword("Iron Sword", 10, 5);
            PhysSkill heavySlash = new PhysSkill("Heavy Slash", Skill.SkillTarget.TargetOneEnemy, 27, 10, 20);
            PhysSkill spinSlash = new PhysSkill("Spin Slash", Skill.SkillTarget.TargetAllEnemies, 23, 18, 40, false);
            MagSkill fireBall = new MagSkill("Fire Ball", Skill.SkillTarget.TargetOneEnemy, 30, 20, 10);
            MagSkill blizzard = new MagSkill("Blizzard", Skill.SkillTarget.TargetAllEnemies, 27, 40, 10, true);
            PhysSkill poisonStrike = new PhysSkill("Poison Strike", Skill.SkillTarget.TargetOneEnemy, 10, 10, 0, false, Skill.BonusEffect.HasStatusEffect, Skill.StatusEffect.poison, Skill.StatChange.none, 2, 100, Skill.EffectSeverity.light);
            Player testMan = new Player("Jack", 3, 15/*0*/, 12, 7, 50, 10, 13);
            Player testMan2 = new Player("Dan", 2, 160, 11, 8, 65, 15, 14);
            //Player testMan3 = new Player("Rory", 3, 150, 12, 7, 50, 10, 13);
            Enemy goblin1 = new Enemy("Goblin 1", 7, 45, 5, 3, 20, 3, 1);
            Enemy goblin2 = new Enemy("Goblin 2", 7, 45, 5, 3, 20, 3, 1);
            Enemy goblinLeader = new Enemy("Goblin Leader", 4, 90, 8, 4, 30, 4, 4);
            battle.AddEntity(testMan);
            battle.AddEntity(testMan2);
            //battle.AddEntity(testMan3);
            battle.AddEntity(goblin1);
            battle.AddEntity(goblin2);
            battle.AddEntity(goblinLeader);
            foreach (KeyValuePair<Entity, int> e in battle.EntityReg)
            {
                e.Key.Weapon = ironSword;
                e.Key.Skills.Add(heavySlash);
                e.Key.Skills.Add(spinSlash);
                e.Key.Skills.Add(fireBall);
                e.Key.Skills.Add(blizzard);
                e.Key.Skills.Add(poisonStrike);
            }
            battle.Start();
        }
    }
}
