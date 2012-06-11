#region

using System;
using D3;

#endregion

namespace Snowshoes.Common
{
    internal class GoldMonitor : Sherpa
    {
        private int _goldStart;
        private int _ticksStart;

        public GoldMonitor(int delay) : base(delay)
        {
            ImmuneToPause = true;
        }

        private static String FriendlyNumber(decimal number)
        {
            if (number < 1000)
                return string.Format("{0}  ", number);
            if (number < 1000000)
                return string.Format("{0} k", Math.Round(number/1000m, 2));
            return string.Format("{0} M", Math.Round(number/1000000m, 2));
        }

        protected override void Loop()
        {
            var gold = GetData(() => Game.Ingame && Me.LevelArea.ToString() != "Axe_Bad_Data" ? Me.Gold : 0);
            if (gold == 0) return;

            if (_goldStart == 0)
            {
                _goldStart = gold;
                _ticksStart = Environment.TickCount;
                return;
            }

            var hoursElapsed = (Environment.TickCount - _ticksStart)/(1000.0m*3600.0m);
            var goldEarned = gold - _goldStart;
            var goldPerHour = goldEarned/hoursElapsed;

            Snowshoes.GoldCount(FriendlyNumber(gold), FriendlyNumber(goldEarned), FriendlyNumber(goldPerHour));
        }
    }
}