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
            PhysSkill heavySlash = new PhysSkill("Heavy Slash", Skill.SkillTarget.TargetOneEnemy, 20, 10, 20);
            PhysSkill spinSlash = new PhysSkill("Spin Slash", Skill.SkillTarget.TargetAllEnemies, 17, 18, 50, false);
            MagSkill fireBall = new MagSkill("Fire Ball", Skill.SkillTarget.TargetOneEnemy, 25, 20, 10);
            MagSkill blizzard = new MagSkill("Blizzard", Skill.SkillTarget.TargetAllEnemies, 20, 40, 50, true);
            Player testMan = new Player("Jack", 3, 150, 12, 7, 50, 10, 13);
            Player testman2 = new Player("Dan", 2, 160, 11, 8, 65, 15, 14);
            Enemy goblin1 = new Enemy("Goblin 1", 7, 45, 5, 3, 20, 3, 1);
            Enemy goblin2 = new Enemy("Goblin 2", 7, 45, 5, 3, 20, 3, 1);
            Enemy goblinLeader = new Enemy("Goblin Leader", 4, 90, 8, 4, 30, 4, 4);
            battle.AddEntity(testMan);
            battle.AddEntity(testman2);
            battle.AddEntity(goblin1);
            battle.AddEntity(goblin2);
            battle.AddEntity(goblinLeader);
            foreach(KeyValuePair<Entity, int> e in battle.EntityReg)
            {
                e.Key.Weapon = ironSword;
                e.Key.Skills.Add(heavySlash);
                e.Key.Skills.Add(spinSlash);
                e.Key.Skills.Add(fireBall);
                e.Key.Skills.Add(blizzard);
            }
            battle.Start();
        }
    }
    public enum StatsSet
    {
        basic = 1,
        advanced,
        complex
    }
}
