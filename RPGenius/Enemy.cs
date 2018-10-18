using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RPGenius
{
    class Enemy : Entity
    {
        private int _baseHp = 0;    //all base stats are initialised to 0 as some stats may not be in use based on game settings
        private int _baseAtk = 0;
        private int _baseDef = 0;
        //private Player _lastAttackedBy; //consider using something like this to increase complexity of enemy target selection
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
        public Enemy(string name, int turnOrder, int hp, int atk, int def) : base(name, turnOrder)
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
            Thread.Sleep(2000);
            IsDefending = false;
            Random rnd = new Random();
            int attackChance = HP / _baseHp * 100;     //Chance of attack is higher if health is higher. (represented as an integer percentage)
            if(attackChance > 90) { attackChance = 90; }    //This statement ensures the chance of defense is always at least 10%
            else { if(attackChance > 70) { attackChance = 80; } }   // This statement is a fun flavour statment, this means that when the enemy is between 70% and 90% of their health, they'll have an 80% chance of attacking
            if(attackChance < 50) { attackChance = 40; }    //this statement means that as soon as the enemy drops below 50% health, they will always have only a 40% chance of attacking
            int choice = rnd.Next(1, 100);     //Generates a random integer between 1 and 100 inclusive
            int target;
            target = rnd.Next(1, battle.PlayerCount);   // randomly chooses a player to attack, each with equal likelyhood  -> maybe make it more inclined to attack whoever hit it last, or something even more complicated
            if(choice <= attackChance) { Attack(battle.Players[target-1], battle); } //If the random number isn't high enough to trump the chance of attacking, the enemy will attack
            else
            {
                IsDefending = true;
                Console.WriteLine("> {0} defends", Name);
                Console.WriteLine("");
            }
        }
    }
}
