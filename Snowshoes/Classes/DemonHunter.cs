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
        public static readonly Spell AlternativeSecondary = new Spell(SNOPowerId.DemonHunter_Impale, 25, 0);

        public static readonly Spell Vault = new Spell(SNOPowerId.DemonHunter_Vault, 0, 8);
        public static readonly Spell Preparation = new Spell(SNOPowerId.DemonHunter_Preparation, 0, 0);
        public static readonly Spell Companion = new Spell(SNOPowerId.DemonHunter_Companion, 0, 10);
        public static readonly Spell SmokeScreen = new Spell(SNOPowerId.DemonHunter_SmokeScreen, 0, 14);


        public static bool AttackUnit(Unit unit, TimeSpan timeout)
        {
            if (Sherpa.GetBool(() => unit.Life <= 0))
            {
                return false;
            }

            var startTime = TimeSpan.FromTicks(Environment.TickCount);


            while (Sherpa.GetBool(() => unit.Life > 0))
            {
                if (!Secondary.Use(unit) && !AlternativeSecondary.Use(unit))
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
                if (Sherpa.GetDistance(x, y) > 50 && Vault.IsAvailableNow() && Sherpa.GetData(() => Me.SecondaryResource) > 22)
                {
                    Vault.Use(x, y);
                    Thread.Sleep(350);
                } 
                else if (Sherpa.GetDistance(x,y) > 30 && SmokeScreen.IsAvailableNow())
                {
                    SmokeScreen.Use();
                    Sherpa.Walk(x, y);
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