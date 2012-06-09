using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;
using Snowshoes.Common;

namespace Snowshoes.Classes
{
    public static class DemonHunter
    {
        public static Spell primary = new Spell(D3.SNOPowerId.DemonHunter_HungeringArrow, 0, 0, 0, true);
        public static Spell secondary = new Spell(D3.SNOPowerId.DemonHunter_ClusterArrow, 0, 50, 0, true);


        public static void init()
        {
            primary.init();
            secondary.init();
        }

        public static bool AttackUnit(D3.Unit unit, TimeSpan timeout)
        {
            if (Sherpa.getBool(() => unit.Life <= 0))
            {
                return false;
            }

            TimeSpan startTime = TimeSpan.FromTicks(System.Environment.TickCount);

            while (Sherpa.getBool(() => unit.Life > 0))
            {
                if (!secondary.use(unit))
                    primary.use(unit);

                Thread.Sleep(200);

                if (TimeSpan.FromTicks(System.Environment.TickCount).Subtract(startTime) > timeout)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
