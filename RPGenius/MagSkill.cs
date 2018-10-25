using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGenius
{
    class MagSkill : Skill
    {
        public MagSkill(string name, SkillTarget targetOptions, int mag, int mpCost, int missChance, bool allOrNothing = false, BonusEffect bonus = BonusEffect.none,
            StatusEffect status = StatusEffect.none, StatChange debuff = StatChange.none, int effectLength = 0, int effectChance = 0, EffectSeverity severity = EffectSeverity.light)
            : base(name, targetOptions, mpCost, missChance, allOrNothing, effectLength, effectChance, severity)
        {
            _mag = mag;
            _bonus = bonus;
            if (bonus == BonusEffect.HasStatusEffect) { _status = status; }
            else { _status = StatusEffect.none; }
            if (bonus == BonusEffect.HasStatDebuff) { _debuff = debuff; }
            else { _status = StatusEffect.none; }
        }
    }
}
