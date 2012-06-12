#region

using System;
using System.Threading;
using D3;
using Snowshoes.Common;

#endregion

namespace Snowshoes.Classes
{
    public static class Wizard
    {
        public static readonly Spell Primary = new Spell(SNOPowerId.Wizard_ShockPulse, 0, 0);
        public static readonly Spell Secondary = new Spell(SNOPowerId.Wizard_ArcaneOrb, 55, 0);

        public static readonly Spell DiamondSkin = new Spell(SNOPowerId.Wizard_DiamondSkin, 0, 15);
        public static readonly Spell Teleport = new Spell(SNOPowerId.Wizard_Teleport, 0, 0);
        public static readonly Spell Familiar = new Spell(Wizard_Familiar, 0, 240);
        public static readonly Spell MagicWeapon = new Spell(SNOPowerId.Wizard_MagicWeapon, 0, 300);


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



        public static bool MoveSuperFuckingFast(float x, float y)
        {
            while (Sherpa.GetDistance(x,y) > 10)
            {
                if (Sherpa.GetDistance(x, y) > 50 && Teleport.IsAvailableNow())
                {
                    Teleport.Use(x, y);
                    Thread.Sleep(200);
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