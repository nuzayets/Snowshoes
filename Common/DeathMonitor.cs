﻿#region

using System.Threading;
using D3;

#endregion

namespace Snowshoes.Common
{
    internal class DeathMonitor : Sherpa
    {
        private readonly Sherpa _dependent;
        private int _deaths;

        public DeathMonitor(int delay, Sherpa caller)
            : base(delay)
        {
            _dependent = caller;
        }

        protected override void Loop()
        {
            if (GetBool(() => Game.Ingame && Me.Life > 0) || !Game.Ingame) return;
            Thread.Sleep(500);
            if (GetBool(() => Game.Ingame && Me.Life > 0) || !Game.Ingame) return;

            Snowshoes.Print(string.Format("Death {0}!", ++_deaths));
            _dependent.HardRestart();
        }
    }
}