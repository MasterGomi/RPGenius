using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGenius
{
    /// <summary>
    /// A (de)buff to an entities attack stat
    /// </summary>
    class AtkChange : StatChange
    {
        public AtkChange(bool isPositive, int duration, EffectSeverity severity) : base(isPositive, duration, severity)
        {
            _name = "ATK";
        }
        //
        protected override void FirstTimeHandle(Entity target)
        {
            double changeMulti = ChangePercentage * 0.01;
            _difference = Convert.ToInt32(target.ATK * (changeMulti));  //this yields the ammount to increase/decrease by
            // example: where target.ATK = 100 and ChangeMulti = 0.25; _difference will be equal to 0.25 * 100, or 25
            if (!IsPositive) { _difference = _difference * -1; }
            target.ATK += _difference;
        }
        protected override void Restore(Entity target, int index)
        {
            target.ATK -= _difference;  //this restores the difference made
            base.Restore(target, index);
        }
    }
    //
    //
    /// <summary>
    /// A (de)buff to an entities magic stat
    /// </summary>
    class MagChange : StatChange
    {
        public MagChange(bool isPositive, int duration, EffectSeverity severity) : base(isPositive, duration, severity)
        {
            _name = "MAG";
        }
        //
        protected override void FirstTimeHandle(Entity target)
        {
            double changeMulti = ChangePercentage * 0.01;
            _difference = Convert.ToInt32(target.MAG * (changeMulti));
            if (!IsPositive) { _difference = _difference * -1; }
            target.MAG += _difference;
        }
        protected override void Restore(Entity target, int index)
        {
            target.MAG -= _difference;
            base.Restore(target, index);
        }
    }
    //
    //
    /// <summary>
    /// A (de)buff to an entities defence stat
    /// </summary>
    class DefChange : StatChange
    {
        public DefChange(bool isPositive, int duration, EffectSeverity severity) : base(isPositive, duration, severity)
        {
            _name = "DEF";
        }
        //
        protected override void FirstTimeHandle(Entity target)
        {
            double changeMulti = ChangePercentage * 0.01;
            _difference = Convert.ToInt32(target.DEF * (changeMulti));
            if (!IsPositive) { _difference = _difference * -1; }
            target.DEF += _difference;
        }
        protected override void Restore(Entity target, int index)
        {
            target.DEF -= _difference;
            base.Restore(target, index);
        }
    }
    //
    //
    /// <summary>
    /// A (de)buff to an entities resistance stat
    /// </summary>
    class SprChange : StatChange
    {
        public SprChange(bool isPositive, int duration, EffectSeverity severity) : base(isPositive, duration, severity)
        {
            _name = "SPR";
        }
        //
        protected override void FirstTimeHandle(Entity target)
        {
            double changeMulti = ChangePercentage * 0.01;
            _difference = Convert.ToInt32(target.SPR * (changeMulti));
            if (!IsPositive) { _difference = _difference * -1; }
            target.SPR += _difference;
        }
        protected override void Restore(Entity target, int index)
        {
            target.SPR -= _difference;
            base.Restore(target, index);
        }
    }
    //
    //
    //class TurnChange : StatChange
    //{
    //
    //}
}
