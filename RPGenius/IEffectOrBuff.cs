using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGenius
{
    interface IEffectOrBuff
    {
        void Apply(Entity target);
        void Handle(Entity target);
        string Display();
    }
}
