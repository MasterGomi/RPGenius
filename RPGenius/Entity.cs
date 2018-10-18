using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RPGenius
{
    abstract class Entity
    {
        protected int _turnOrder;
        protected int _hp;
        protected int _mp;
        protected int _atk;
        protected int _def;
        protected int _mag;
        protected int _spr;
        protected int _spd;
        //
        public abstract int BaseHp { get; }
        public abstract int BaseAtk { get; }
        public abstract int BaseDef { get; }
        public int HP
        {
            get
            {
                return _hp;
            }
            set
            {
                _hp = value;
                if(_hp < 0) { _hp = 0; }
                if(_hp > BaseHp) { _hp = BaseHp; }
            }
        }
        public int ATK
        {
            get
            {
                return _atk;
            }
            set
            {
                _atk = value;
                if(_atk < Convert.ToInt32(BaseAtk * 0.5)) { _atk = Convert.ToInt32(BaseAtk * 0.5); }
                if(_atk > Convert.ToInt32(BaseAtk * 1.5)) { _atk = Convert.ToInt32(BaseAtk * 1.5); }
            }
        }
        public int DEF
        {
            get
            {
                return _def;
            }
            set
            {
                _def = value;
                if (_def < BaseAtk * 0.5) { _def = Convert.ToInt32(BaseDef * 0.5); }
                if (_def > BaseAtk * 1.5) { _def = Convert.ToInt32(BaseDef * 1.5); }
            }
        }
        public string Name { get; set;/* maybe no need for set? */ }
        public int TurnOrder
        {
            get { return _turnOrder; }
            set
            {
                _turnOrder = value;
                if (_turnOrder > 10) { _turnOrder = 10; }
                if(_turnOrder < 0) { _turnOrder = 0; }
            }
        }
        public bool IsDefending { get; set; }
        public Weapon Weapon { get; set; }
        //
        public Entity(string name, int turnOrder)
        {
            Name = name;
            _turnOrder = turnOrder;
            IsDefending = false;
        }
        //
        public abstract void ExecuteTurn(Battle battle);
        protected void Attack(Entity target, Battle battle)
        {
            Console.WriteLine("> {0} attacks {1}", Name, target.Name);
            Thread.Sleep(1500);
            Console.WriteLine("");
            Random rnd = new Random();
            int damage = 0;
            int weaponDamage = Weapon.ATK + rnd.Next(-Weapon.Varience, Weapon.Varience);
            damage = ATK + weaponDamage - target.DEF;
            if (target.IsDefending) { damage = Convert.ToInt32(damage * 0.5); }
            if (damage == 0) { damage = 1; }
            int missChance = 20;    // 20% chance to miss, will be different and varied depending on stat settings
            int hit = rnd.Next(1, 100);
            if (hit > missChance)    //if the attack hits
            {
                target.HP -= damage;
                Console.WriteLine("{0}'s attack hits {1} for {2} damage. {1}'s HP now {3}/{4}", Name, target.Name, damage, target.HP, target.BaseHp);     //eg: The attack hits Goblin for 30 damage. Goblin's HP now 15/50
                if(target.HP == 0)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("{0} has been defeated!", target.Name);
                    battle.RemoveEntity(target);
                }
            }
            else { Console.WriteLine("The attack misses"); }
        }
    }
}
