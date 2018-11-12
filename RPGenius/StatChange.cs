using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGenius
{
    /// <summary>
    /// The parent class for all (de)buffs
    /// </summary>
    abstract class StatChange : IEffectOrBuff
    {
        public bool IsPositive { get; }
        private int _duration;
        public int ChangePercentage { get; }
        protected int _difference;    //the difference between the stat before application and following
        protected string _name;
        //
        /// <summary>
        /// Creates a StatChange object
        /// </summary>
        /// <param name="isPositive">Determines if the StatChange is a buff or debuff</param>
        /// <param name="duration">Number representing how many turns the change lasts for</param>
        /// <param name="severity">Severity of the (de)buff</param>
        public StatChange(bool isPositive, int duration, EffectSeverity severity)
        {
            IsPositive = isPositive;
            _duration = duration;
            switch (severity)
            {
                case EffectSeverity.light:
                    ChangePercentage = 10;
                    break;
                case EffectSeverity.moderate:
                    ChangePercentage = 25;
                    break;
                case EffectSeverity.heavy:
                    ChangePercentage = 50;
                    break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
        //
        /// <summary>
        /// Applies the (de)Buff
        /// </summary>
        /// <param name="target">The recieving entity</param>
        public void Apply(Entity target)
        {
            StatChange original = CheckForExisting(target);
            if (original != null)
            {
                int determinant = ApplySameType(target, original);
                if (determinant == 1) { return; }
            }
            else { ApplyNew(target); }
            FirstTimeHandle(target);
        }
        /// <summary>
        /// Checks for an existing (de)buff of the same variety and returns it, or null if none are found
        /// </summary>
        /// <param name="target">reciever</param>
        /// <returns>Returns StatChange object if match is found, otherwise, null is returned</returns>
        private StatChange CheckForExisting(Entity target)
        {
            foreach(StatChange s in target.StatChanges)
            {
                if (s.GetType() == GetType())
                {
                    return s;
                }
            }
            return null;
        }
        /// <summary>
        /// Handles the application of a (de)buff of the same kind as one the entity already has
        /// </summary>
        /// <param name="target">Reciever</param>
        /// <param name="original">The StatChange that the target already has</param>
        /// <returns>1 or 0. 1 = nothing more is neaded. 0 = first-time handling is needed</returns>
        private int ApplySameType(Entity target, StatChange original)
        {
            int index = target.StatChanges.IndexOf(original);
            if (this.IsPositive != original.IsPositive) //if one is positive and the other is negative, they cancel each other out
            {
                target.StatChanges.Remove(original);
                target.StatChangeDurations.RemoveAt(index);
                Console.WriteLine("> {0}'s {1} is back to normal", target.Name, _name);
                return 1;
            }
            if (original.ChangePercentage < this.ChangePercentage)  // if the new one is more powerful, it overwrites the old one
            {
                target.StatChanges[index] = this;
                target.StatChangeDurations[index] = _duration;
                if (IsPositive) { Console.WriteLine("> {0}'s {1} has been increased", target.Name, _name); }
                else { Console.WriteLine("> {0}'s {1} has been lowered", target.Name, _name); }
                return 0;
            }
            if (original.ChangePercentage == this.ChangePercentage)     //if they are of equal strength, the original effect is extended by the duration of the new effect
            {
                target.StatChangeDurations[index] += _duration;
                Console.WriteLine("> {0}'s {1} change has been extended", target.Name, _name);
                return 1;
            }
            //else   - if the new one is weaker than the old one, the effect is extended by one turn
            target.StatChangeDurations[index] += 1;
            Console.WriteLine("> It was not very effective on {0}", target.Name);
            return 1;
        }
        /// <summary>
        /// Applies a (de)buff of a kind that the target doesn't already have
        /// </summary>
        /// <param name="target">Reciever</param>
        private void ApplyNew(Entity target)
        {
            target.StatChanges.Add(this);
            target.StatChangeDurations.Add(_duration);
            if (IsPositive) { Console.WriteLine("> {0}'s {1} has been increased", target.Name, _name); }
            else { Console.WriteLine("> {0}'s {1} has been lowered", target.Name, _name); }
        }
        /// <summary>
        /// Does necessary handling for a StatChange that has just been applied. Namely, the original altering of stats and storage of difference
        /// </summary>
        /// <param name="target">Entity that possess the StatChange</param>
        protected abstract void FirstTimeHandle(Entity target);
        /// <summary>
        /// Decrements the (de)buff's time remaining and removes it if it reaches zero
        /// </summary>
        /// <param name="target">Subject</param>
        /// <param name="turnProgress">Position in the turn</param>
        public void Handle(Entity target, int turnProgress)
        {
            if (turnProgress == 3)
            {
                int index = target.StatChanges.IndexOf(this);
                target.StatChangeDurations[index]--;
                if (target.StatChangeDurations[index] <= 0) { Restore(target, index); }
            }
        }
        /// <summary>
        /// Restores the target's stat back to normal, called when a StatChange has expired
        /// </summary>
        /// <param name="target">Subject</param>
        /// <param name="index">The position of the StatChange in the target.StatChanges list</param>
        protected virtual void Restore(Entity target, int index)
        {
            Console.WriteLine("> {0}'s {1} has reverted", target.Name, _name);
            target.StatChanges.Remove(this);
            target.StatChangeDurations.RemoveAt(index);
        }
        /// <summary>
        /// Returns a string representing the StatChange the target has
        /// </summary>
        /// <returns>String containing current StatChange</returns>
        public string Display()
        {
                return IsPositive ? "*" + _name + " up*" : "*" + _name + " down*";
        }
    }
}
