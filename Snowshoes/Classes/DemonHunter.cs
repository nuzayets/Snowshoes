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
        public static readonly Spell Primary = new Spell(SNOPowerId.DemonHunter_HungeringArrow, 0, 0);
        public static readonly Spell Secondary = new Spell(SNOPowerId.DemonHunter_ClusterArrow, 50, 0);

        public static readonly Spell Vault = new Spell(SNOPowerId.DemonHunter_Vault, 0, 0);
        public static readonly Spell Preparation = new Spell(SNOPowerId.DemonHunter_Preparation, 0, 0);
        public static readonly Spell Companion = new Spell(SNOPowerId.DemonHunter_Companion, 0, 0);
        public static readonly Spell SmokeScreen = new Spell(SNOPowerId.DemonHunter_SmokeScreen, 0, 0);


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