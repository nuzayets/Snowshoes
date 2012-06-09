using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Snowshoes.Common
{
    class GoldMonitor : Sherpa
    {

        int gold_start = 0;
        int ticks_start = 0;

        public GoldMonitor(int delay) : base(delay) 
        {
            ImmuneToPause = true;
        }

        String friendly_number(decimal number)
        {
            if (number < 1000)
                return string.Format("{0}  ", number);
            else if (number < 1000000)
                return string.Format("{0} k", Math.Round(number / 1000m, 2));
            else
                return string.Format("{0} M", Math.Round(number / 1000000m, 2));
        }

        override protected void loop()
        {
            int gold = getData<int>(() => D3.Game.Ingame && D3.Me.LevelArea.ToString() != "Axe_Bad_Data" ? D3.Me.Gold : 0);
            if (gold != 0)
            {
                if (gold_start == 0)
                {
                    gold_start = gold;
                    ticks_start = System.Environment.TickCount;
                    return;
                }

                decimal hours_elapsed = (System.Environment.TickCount - ticks_start) / (1000.0m * 3600.0m);
                int gold_earned =  gold - gold_start;
                decimal gold_per_hour = gold_earned / hours_elapsed;

                Snowshoes.GoldCount(friendly_number(gold), friendly_number(gold_earned), friendly_number(gold_per_hour));
            }
        }
    }
}
