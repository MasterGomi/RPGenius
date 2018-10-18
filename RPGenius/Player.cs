using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGenius
{
    class Player : Entity
    {
        private int _baseHp = 0;    //all base stats are initialised to 0 as some stats may not be in use based on game settings
        private int _baseAtk = 0;
        private int _baseDef = 0;
        //
        public override int BaseHp
        {
            get { return _baseHp; }
        }
        public override int BaseAtk
        {
            get { return _baseAtk; }
        }
        public override int BaseDef
        {
            get { return _baseDef; }
        }
        //
        public Player(string name, int turnOrder, int hp, int atk, int def) : base(name, turnOrder)
        {
            _baseHp = hp;
            HP = hp;
            _baseAtk = atk;
            ATK = atk;
            _baseDef = def;
            DEF = def;
        }
        //
        public override void ExecuteTurn(Battle battle)
        {
            IsDefending = false;
            int choice;
            Console.WriteLine("");
            Console.WriteLine("What will {0} do?\t\tHP: {1}/{2}", Name, HP, BaseHp);
            do
            {
                Console.WriteLine("1. Attack\t2. Defend");
                Console.Write("\t=>  ");
                choice = ExSys.ReadIntRange(1, 2);
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
                }
            } while (choice == 0);
        }
        
    }
}
