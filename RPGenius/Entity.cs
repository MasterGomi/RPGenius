using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RPGenius
{
    /// <summary>
    /// The base class for both players and enemies
    /// </summary>
    abstract class Entity
    {
        private int _turnOrder;
        private int _hp;
        private int _mp;
        private int _atk;
        private int _def;
        private int _mag;
        private int _spr;
        //
        //
        public int BaseHp { get; }
        public int BaseAtk { get; }
        public int BaseDef { get; }
        public int BaseMp { get; }
        public int BaseMag { get; }
        public int BaseSpr { get; }
        //
        public int HP
        {
            get
            {
                return _hp;
            }
            set
            {
                _hp = value;
                if (_hp < 0) { _hp = 0; }
                if (_hp > BaseHp) { _hp = BaseHp; }
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
                if (_atk < Convert.ToInt32(BaseAtk * 0.5)) { _atk = Convert.ToInt32(BaseAtk * 0.5); }
                if (_atk > Convert.ToInt32(BaseAtk * 1.5)) { _atk = Convert.ToInt32(BaseAtk * 1.5); }
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
        public int MP
        {
            get
            {
                return _mp;
            }
            set
            {
                _mp = value;
                if (_mp < 0) { _mp = 0; }
                if (_mp > BaseMp) { _mp = BaseMp; }
            }
        }
        public int MAG
        {
            get
            {
                return _mag;
            }
            set
            {
                _mag = value;
                if (_mag < Convert.ToInt32(BaseMag * 0.5)) { _mag = Convert.ToInt32(BaseMag * 0.5); }
                if (_mag > Convert.ToInt32(BaseMag * 1.5)) { _mag = Convert.ToInt32(BaseMag * 1.5); }
            }
        }
        public int SPR
        {
            get
            {
                return _spr;
            }
            set
            {
                _spr = value;
                if (_spr < Convert.ToInt32(BaseSpr * 0.5)) { _spr = Convert.ToInt32(BaseSpr * 0.5); }
                if (_spr > Convert.ToInt32(BaseSpr * 1.5)) { _spr = Convert.ToInt32(BaseSpr * 1.5); }
            }
        }
        //
        public string Name { get; }
        public int TurnOrder
        {
            get { return _turnOrder; }
            set
            {
                _turnOrder = value;
                if (_turnOrder > 10) { _turnOrder = 10; }
                if (_turnOrder < 0) { _turnOrder = 0; }
            }
        }
        public bool IsDefending { get; set; }
        public Weapon Weapon { get; set; }
        public List<Skill> Skills = new List<Skill>();
        public IEffectOrBuff Effect;
        public int EffectDurationRemaining { get; set; }
        public List<IEffectOrBuff> StatChanges = new List<IEffectOrBuff>();
        public List<int> StatChangeDurations = new List<int>();
        public string EffectString
        {
            get
            {
                string result = "  ";
                if (Effect != null) { result += "  " + Effect.Display(); ; }
                foreach (IEffectOrBuff stat in StatChanges)
                {
                    result += "  " + stat.Display();
                }
                return result;
            }
        }
        public bool CanUseTurn { get; set; }
        public bool HaveTurnLater { get; set; }
        public bool Afraid { get; set; }
        public bool Confused { get; set; }
        public bool EffectHandleLatter { get; set; }
        //
        //
        /// <summary>
        /// Creates an entity
        /// </summary>
        /// <param name="name">Name of the entity</param>
        /// <param name="turnOrder">Number representing the entities position in the turn order</param>
        /// <param name="hp">Integer representing the base hp of the entity</param>
        /// <param name="atk">Integer representing the base attack of the entity</param>
        /// <param name="def">Integer representing the base defence of the entity</param>
        /// <param name="mp">Integer representing the base mp of the entity</param>
        /// <param name="mag">Integer representing the base magic of the entity</param>
        /// <param name="spr">Integer representing the base resistance of the entity</param>
        public Entity(string name, int turnOrder, int hp, int atk, int def, int mp, int mag, int spr)
        {
            Name = name;
            _turnOrder = turnOrder;
            IsDefending = false;
            BaseHp = hp;
            HP = hp;
            BaseAtk = atk;
            ATK = atk;
            BaseDef = def;
            DEF = def;
            BaseMp = mp;
            MP = mp;
            BaseMag = mag;
            MAG = mag;
            BaseSpr = spr;
            SPR = spr;
            CanUseTurn = true;
            HaveTurnLater = false;
        }
        //
        //
        /// <summary>
        /// Allows an entity to execute their turn
        /// </summary>
        /// <param name="battle">The battle object</param>
        public abstract void ExecuteTurn(Battle battle);
            // (private) effect handling
            // (abstract [int]) course of action determining   (because each sub-class will handle differently
            // switch ([int])
                //(private) attack
                //(private) use skill
            // effect handling again maybe
            //etc.
        /// <summary>
        /// Carries out an attack against the specified targeted entity
        /// </summary>
        /// <param name="target">Entity to be attacked</param>
        /// <param name="battle">The battle object</param>
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
            int missChance = 15;    // 15% chance to miss, will be different and varied depending on stat settings
            int hit = rnd.Next(1, 101);
            if (hit > missChance)    //if the attack hits
            {
                target.HP -= damage;
                Console.WriteLine("> {0}'s attack hits {1} for {2} damage. {1}'s HP now {3}/{4}", Name, target.Name, damage, target.HP, target.BaseHp);     //eg: Jack's attack hits Goblin for 30 damage. Goblin's HP now 15/50
                if (target.HP == 0)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("\n> {0} has been defeated!", target.Name);
                    battle.RemoveEntity(target);
                }
            }
            else { Console.WriteLine("> The attack misses"); }
        }
    }
}
