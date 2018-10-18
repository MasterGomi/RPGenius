using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGenius
{
    class Skill
    {
        public enum SkillTarget
        {
            TargetOneEnemy = 1,
            TargetAllEnemies,
            TargetOnePlayer,
            TargetAllPlayers,
            TargetSelf
        }
        public enum SkillType
        {
            ATK,
            MAG,
            buff,
            debuff
        }
        public int ATK { get; }
        public int MAG { get; }
        public int MPCost { get; }
        public SkillTarget TargetOptions { get; }
        public SkillType Type { get; }
        public string Effect;
        //
        public Skill(SkillTarget targetOptions, SkillType type, int atk, int mag, string effect, int mpCost)
        {
            TargetOptions = targetOptions;
            Type = type;
            ATK = atk;
            MAG = mag;
            Effect = effect;
            MPCost = mpCost;
        }
    }
}
