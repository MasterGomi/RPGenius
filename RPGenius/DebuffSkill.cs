using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGenius
{
    class DebuffSkill : Skill
    {
        public DebuffSkill(string name, SkillTarget targetOptions, int mpCost, StatChange stat,
            int effectChance, int effectLength, EffectSeverity severity, bool allOrNothing = false)
            : base(name, targetOptions, mpCost, 0, allOrNothing, effectLength, effectChance, severity)
        {
            _buff = stat;
        }
    }
}
