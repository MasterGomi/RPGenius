using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGenius
{
    class SkillUseFactory
    {
        public SkillUseFactory() { }
        //
        public SkillUse Create(SkillTarget options, Skill s)
        {
            switch (options)
            {
                case SkillTarget.TargetAllEnemies:
                    return new AllEnemies(s);
                case SkillTarget.TargetAllFriends:
                    return new AllFriends(s);
                case SkillTarget.TargetOneEnemy:
                    return new OneEnemy(s);
                case SkillTarget.TargetOneFriend:
                    return new OneFriend(s);
                case SkillTarget.TargetSelf:
                    return new Self(s);
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}
