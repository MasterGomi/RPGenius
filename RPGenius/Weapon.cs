using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGenius
{
    abstract class Weapon
    {
        private string _name;
        private int _atk;
        private int _varience;
        //
        public string Name { get => _name; }
        public int ATK { get => _atk; }
        public int Varience { get => _varience; }
        //
        public Weapon(string name, int atk, int atkVarience)
        {
            _name = name;
            _atk = atk;
            _varience = atkVarience;
        }
    }
}
