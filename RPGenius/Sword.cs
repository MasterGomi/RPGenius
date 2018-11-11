using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGenius
{
    /// <summary>
    /// A weapon sub-type with all-round capabilities
    /// </summary>
    class Sword : Weapon
    {
        public Sword(string name, int atk, int atkVarience) : base(name, atk, atkVarience) { }
    }
}
