#region

using System;
using System.Threading;
using D3;
using Snowshoes.Common;

#endregion

namespace Snowshoes.Classes
{
    public static class DemonHunter
    {
        public static Spell Primary = new Spell(SNOPowerId.DemonHunter_HungeringArrow, 0, 0, 0, true);
        public static Spell Secondary = new Spell(SNOPowerId.DemonHunter_ClusterArrow, 0, 50, 0, true);


        public static void Init()
        {
            Primary.Init();
            Secondary.Init();
        }

        public static bool AttackUnit(Unit unit, TimeSpan timeout)
        {
            if (Sherpa.GetBool(() => unit.Life <= 0))
            {
                return false;
            }

            var startTime = TimeSpan.FromTicks(Environment.TickCount);


            while (Sherpa.GetBool(() => unit.Life > 0))
            {
                if (!Secondary.Use(unit))
                    Primary.Use(unit);

                Thread.Sleep(200);

                if (TimeSpan.FromTicks(Environment.TickCount).Subtract(startTime) > timeout)
                {
                    return false;
                }
            }

            return true;
        }
    }
}