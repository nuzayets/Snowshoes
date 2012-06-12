/*
 *  Copyright (C) 2012 k_os <ben.at.hemio.de>
 * 
 *  This file is part of rndWalker.
 *
 *  rndWalker is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  rndWalker is distributed in the hope that it will be Useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with Foobar.  If not, see <http://www.gnu.org/licenses/>. 
 */
using System;
using System.Threading;
using D3;
using Snowshoes.Common;

namespace Snowshoes.Classes
{
    public static class Monk
    {
        public static readonly Spell mantraOfHealing = new Spell(SNOPowerId.Monk_MantraOfHealing, 50, 0);
        public static readonly Spell mantraOfEvasion = new Spell(SNOPowerId.Monk_MantraOfEvasion, 50, 0);
        public static readonly Spell mantraOfConviction = new Spell(SNOPowerId.Monk_MantraOfConviction, 50, 0);

        public static readonly Spell blindingFlash = new Spell(SNOPowerId.Monk_BlindingFlash, 10, 0);
        public static readonly Spell breathOfHeaven = new Spell(SNOPowerId.Monk_BreathOfHeaven, 25, 0);
        public static readonly Spell serenity = new Spell(SNOPowerId.Monk_Serenity, 10, 0);
        public static readonly Spell sevenSidedStrike = new Spell(SNOPowerId.Monk_SevenSidedStrike, 50, 0);
        public static readonly Spell wayOfTheHundredFists = new Spell(SNOPowerId.Monk_WayOfTheHundredFists, 0, 0);
        public static readonly Spell fistsOfThunder = new Spell(SNOPowerId.Monk_FistsofThunder, 0, 0);
        public static readonly Spell sweepingWind = new Spell(SNOPowerId.Monk_SweepingWind, 75, 0);

        //public static Spell potion = new Spell(SNOPowerId.Axe_Operate_Gizmo, 30, 0, 0, true);

        public static void drinkPot()
        {
            // hotfix as this does not work via power
            //1FB50094: E1F43DD874E42728 Root.NormalLayer.game_dialog_backgroundScreenPC.game_potion (Visible: True)
            var potionButton = Sherpa.GetData(() => UIElement.Get(0xE1F43DD874E42728));
            Sherpa.PerformAction(potionButton.Click);
        }

        public static bool AttackUnit(D3.Unit _unit, TimeSpan _timeout)
        {
            if (Sherpa.GetBool(() => _unit.Life <= 0))
            {
                return false;
            }

            TimeSpan startTime = TimeSpan.FromTicks(System.Environment.TickCount);

            while (Sherpa.GetBool(() => _unit.Life > 0))
            {
                if (Me.MaxLife - Me.Life > 10000)
                {
                    breathOfHeaven.Use();
                }

                blindingFlash.Use();
                if (sevenSidedStrike.Use(_unit))
                    Thread.Sleep(900);
                else
                {
                    var buff = Sherpa.GetData(() => UIElement.Get(0x42A41DDB1E96841A));
                    if (buff == default(UIElement) || !buff.Visible)
                        sweepingWind.Use();

                    wayOfTheHundredFists.Use(_unit);
                    
                }
                Thread.Sleep(100);

                if (TimeSpan.FromTicks(System.Environment.TickCount).Subtract(startTime) > _timeout)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
