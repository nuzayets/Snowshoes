#region

using System;
using System.Threading;
using D3;
using Snowshoes.Common;

#endregion

namespace Snowshoes.Classes
{
    public static class Barbarian
    {
        public static readonly Spell Primary = new Spell(SNOPowerId.Barbarian_Frenzy, 0, 0);

        public static readonly Spell Warcry = new Spell(SNOPowerId.Barbarian_WarCry, 0, 0);
        public static readonly Spell Sprint = new Spell(SNOPowerId.Barbarian_Sprint, 20, 0);
        public static readonly Spell Earthquake = new Spell(SNOPowerId.Barbarian_Earthquake, 0, 0);
        public static readonly Spell Leap = new Spell(SNOPowerId.Barbarian_Leap, 15, 0);
        public static readonly Spell Shout = new Spell(SNOPowerId.Barbarian_ThreateningShout, 15, 0);


        public static bool AttackUnit(Unit unit, TimeSpan timeout)
        {
            if (Sherpa.GetBool(() => unit.Life <= 0))
            {
                return false;
            }

            var startTime = TimeSpan.FromTicks(Environment.TickCount);


            while (Sherpa.GetBool(() => unit.Life > 0))
            {
                Primary.Use(unit);

                if (TimeSpan.FromTicks(Environment.TickCount).Subtract(startTime) > timeout)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool SprintEverywhere(float x, float y)
        {
            if (Me.PrimaryResource < 20) Warcry.Use();
            while (Sherpa.GetDistance(x, y) > 10)
            {
                if (Sherpa.GetDistance(x, y) > 30 && Sprint.IsAvailableNow() && Sherpa.GetData(() => Me.PrimaryResource) >= 20)
                {
                    Sprint.Use();
                    Thread.Sleep(350);
                }
                else
                {
                    Sherpa.Walk(x, y);
                }
            }
            return true;
        }
    }
}