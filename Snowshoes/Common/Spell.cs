using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace Snowshoes.Common
{
    public class Spell
    {
        protected bool available = false;
        protected bool forced = false;
        protected int lastTick = System.Int32.MinValue;
        protected D3.SNOPowerId id;
        protected int cooldown = -1;
        protected int primCost = 0;
        protected int secCost = 0;

        public Spell(D3.SNOPowerId _id, int _cooldown, int _cost1, int _cost2, bool _available)
        {
            id = _id;
            cooldown = _cooldown;
            primCost = _cost1;
            secCost = _cost2;
            available = _available;
            forced = true;
            init();
        }

        public Spell(D3.SNOPowerId _id, int _cooldown, int _cost1, int _cost2)
        {
            id = _id;
            cooldown = _cooldown;
            primCost = _cost1;
            secCost = _cost2;
            init();
        }

        public void init()
        {
            if (!forced)
                available = Sherpa.getBool(() => D3.Me.Skills.Contains(id));
        }

        public bool use(D3.Unit target)
        {
            if (available && Sherpa.getBool(() => D3.Me.PrimaryResource >= primCost && D3.Me.SecondaryResource >= secCost) && System.Environment.TickCount > lastTick + cooldown * 1000 + 100)
            {
                Sherpa.performAction(() => D3.Me.UsePower(id, target));
                lastTick = System.Environment.TickCount;
                Thread.Sleep(75);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool use(float _x, float _y)
        {

            if (available && System.Environment.TickCount > lastTick + cooldown * 1000 + 100)
            {
                Sherpa.performAction(() => D3.Me.UsePower(id, _x, _y, D3.Me.Z));
                lastTick = System.Environment.TickCount;
                Thread.Sleep(75);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool use()
        {
            if (available && System.Environment.TickCount > lastTick + cooldown * 1000 + 100)
            {
                Sherpa.performAction(() => D3.Me.UsePower(id));
                lastTick = System.Environment.TickCount;
                Thread.Sleep(75);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
