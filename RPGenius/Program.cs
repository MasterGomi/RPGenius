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
            PhysSkill heavySlash = new PhysSkill("Heavy Slash", SkillTarget.TargetOneEnemy, 27, 10, 20);
            PhysSkill spinSlash = new PhysSkill("Reckless Slash", SkillTarget.TargetAllEnemies, 23, 18, 40);
            MagSkill fireBall = new MagSkill("Fire Ball", SkillTarget.TargetOneEnemy, 30, 20, 10, EffectKind.burn, 2, 50, EffectSeverity.moderate);
            MagSkill blizzard = new MagSkill("Blizzard", SkillTarget.TargetAllEnemies, 27, 40, 10, true);
            PhysSkill poisonStrike = new PhysSkill("Poison Strike", SkillTarget.TargetOneEnemy, 10, 10, 0, EffectKind.poison, 2, 100);
            SupportSkill freeze = new SupportSkill("Freeze", SkillTarget.TargetOneEnemy, 4, EffectKind.freeze, 100, 3);
            SupportSkill dispelPoison = new SupportSkill("Dispel poison", SkillTarget.TargetOneFriend, 2, EffectKind.poison, 100, 0);
            SupportSkill atkDownAll = new SupportSkill("Mass weakening", SkillTarget.TargetAllEnemies, 15, EffectKind.ATK, 100, 3, EffectSeverity.moderate, false);
            SupportSkill bigAtkDown = new SupportSkill("Enfeeblement", SkillTarget.TargetOneEnemy, 15, EffectKind.ATK, 100, 3, EffectSeverity.heavy, false);
            SupportSkill smallAtkDown = new SupportSkill("Gentle weakening", SkillTarget.TargetOneEnemy, 5, EffectKind.ATK, 100, 3, EffectSeverity.light, false);
            SupportSkill moderateAtkDown = new SupportSkill("Weakening", SkillTarget.TargetOneEnemy, 10, EffectKind.ATK, 100, 3, EffectSeverity.moderate, false);
            SupportSkill defDownAll = new SupportSkill("Mass armour break", SkillTarget.TargetAllEnemies, 15, EffectKind.DEF, 100, 3, EffectSeverity.moderate, false);

            Player testMan = new Player("Jack", 3, 1500, 12, 7, 500, 10, 13);
            Player testMan2 = new Player("Dan", 2, 1600, 11, 8, 650, 15, 14);
            //Player testMan3 = new Player("Rory", 3, 150, 12, 7, 50, 10, 13);
            Enemy goblin1 = new Enemy("Goblin 1", 7, 450, 5, 3, 200, 3, 1);
            Enemy goblin2 = new Enemy("Goblin 2", 7, 450, 5, 3, 200, 3, 1);
            Enemy goblinLeader = new Enemy("Goblin Leader", 4, 900, 8, 4, 300, 4, 4);
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
                e.Key.Skills.Add(freeze);
                e.Key.Skills.Add(dispelPoison);
                e.Key.Skills.Add(smallAtkDown);
                e.Key.Skills.Add(moderateAtkDown);
                e.Key.Skills.Add(bigAtkDown);
                e.Key.Skills.Add(atkDownAll);
                e.Key.Skills.Add(defDownAll);
            }


            battle.Start();
        }
    }
}
