using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGenius
{
    class PhysSkill : Skill
    {
        public PhysSkill(string name, SkillTarget targetOptions, int atk, int mpCost, int missChance, bool allOrNothing = false) : base(name, targetOptions, mpCost, missChance, allOrNothing)
        {
            _atk = atk;
        }
    }
}
