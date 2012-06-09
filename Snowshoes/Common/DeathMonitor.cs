using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace Snowshoes.Common
{
    class DeathMonitor : Sherpa
    {
        Sherpa dependent;
        int deaths = 0;

        public DeathMonitor(int delay, Sherpa caller)
            : base(delay)
        {
            dependent = caller;
        }

        override protected void loop()
        {
            if (getBool(() => D3.Game.Ingame && D3.Me.LevelArea.ToString() != "Axe_Bad_Data") && D3.Me.Life <= 0)
            {
                
                Snowshoes.Print(string.Format("Death {0}!", ++deaths));
                dependent.HardRestart();
                 
            }
        }

    }
}
