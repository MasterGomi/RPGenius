using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGenius
{
    class MagSkill : Skill
    {
        public MagSkill(string name, SkillTarget targetOptions, int mag, int mpCost, int missChance, bool allOrNothing = false) : base(name, targetOptions, mpCost, missChance, allOrNothing)
        {
            _mag = mag;
        }
    }
}
