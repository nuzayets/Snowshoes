using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace Snowshoes.Common
{
    class Watchdog : Sherpa
    {
        Sherpa dependent;

        public Watchdog(int delay, Sherpa caller)
            : base(delay)
        {
            dependent = caller;
        }

        override protected void loop()
        {
            if (dependent.tickRunTime() > 60000)
            {
                Snowshoes.Print("Watchdog kill!");
                dependent.HardRestart();
                Thread.Sleep(15000); // not gonna die again in 5 secs are we?
            }
        }

    }
}
