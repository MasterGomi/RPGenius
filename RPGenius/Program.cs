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
            Player testMan = new Player("Jack", 3, 150, 12, 7);
            Player testman2 = new Player("Dan", 2, 160, 11, 8);
            Enemy goblin1 = new Enemy("Goblin 1", 7, 45, 5, 3);
            Enemy goblin2 = new Enemy("Goblin 2", 7, 45, 5, 3);
            Enemy goblinLeader = new Enemy("Goblin Leader", 4, 90, 8, 4);
            battle.AddEntity(testMan);
            battle.AddEntity(testman2);
            battle.AddEntity(goblin1);
            battle.AddEntity(goblin2);
            battle.AddEntity(goblinLeader);
            foreach(KeyValuePair<Entity, int> e in battle.EntityReg)
            {
                e.Key.Weapon = ironSword;
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
