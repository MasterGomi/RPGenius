using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGenius
{
    /// <summary>
    /// Used to create and return the class that contains the necessary functionality for targeting and using a skill
    /// </summary>
    class SkillUseFactory
    {
        public SkillUseFactory() { }
        //
        /// <summary>
        /// Creates and returns the required SkillUse object based on the skills requirements
        /// </summary>
        /// <param name="options">The targeting options the skill should have</param>
        /// <param name="s">The skill in question</param>
        /// <returns>Returns the requires SkillUse object</returns>
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
