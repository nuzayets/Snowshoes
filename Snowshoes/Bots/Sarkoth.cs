#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using D3;
using Snowshoes.Common;

#endregion

namespace Snowshoes.Bots
{
    internal class Sarkoth : AbstractBotSherpa
    {
        private readonly List<decimal> _failures = new List<decimal>();
        private readonly List<decimal> _successes = new List<decimal>();

        public Sarkoth()
        {
            new Watchdog(2500, this);
            new DeathMonitor(250, this);
        }


        private static bool IsInventoryStuffed()
        {
            return
                GetBool(() => Me.GetContainerItems(Container.Inventory).Sum(item => item.ItemSizeX*item.ItemSizeY) >= 40);
        }

        private static void TownRun()
        {
            if (!NeedsRepair() && !IsInventoryStuffed()) return;
            
            GoTown();

            SnagIt.IdentifyAll();

            Walk(2966, 2825);
            Walk(2941.5f, 2850.7f);
            Interact("Salvage");
            Thread.Sleep(500); // must we really!!
            SnagIt.SalvageItems();

            Walk(2940, 2813);
            Walk(2895, 2785);
            Interact("Tashun the Miner");
            Thread.Sleep(500); // god the humanity...
            RepairAll();
            SnagIt.SellItems();

            Walk(2933, 2789);
            Walk(2969, 2791);
            Interact("Stash");
            SnagIt.StashItems();


            Walk(2977, 2799);
            TakePortal();
        }



        protected override void Loop()
        {
            StartGame();


            var ticks = Environment.TickCount;

            var goldStart = GetData(() => Me.Gold);

            Walk(1995, 2603);
            PerformAction(() => Me.UsePower(SNOPowerId.DemonHunter_SmokeScreen));
            Walk(2025, 2563);
            PerformAction(() => Me.UsePower(SNOPowerId.DemonHunter_SmokeScreen));
            Walk(2057, 2528);
            PerformAction(() => Me.UsePower(SNOPowerId.DemonHunter_SmokeScreen));
            Walk(2081, 2487);
            var cellar = GetData(() => Unit.Get().FirstOrDefault(u => u.Name.Contains("Dank Cellar")));
            if (cellar == default(Unit))
            {
                ExitGame();
                var runTime = Math.Round((Environment.TickCount - ticks)/1000m, 0);
                _failures.Add(runTime);
                Snowshoes.Print(String.Format("{0} secs failure run ({1} avg); {2}% success rate", runTime,
                                              Math.Round(_failures.Average()),
                                              Math.Round((_successes.Count/
                                                          (_failures.Count + (decimal) _successes.Count))*
                                                         100m)));
                return;
            }


            PerformAction(() => Me.UsePower(SNOPowerId.DemonHunter_SmokeScreen));
            Walk(2081, 2487);
            PerformAction(() => Me.UsePower(SNOPowerId.DemonHunter_Preparation));
            Walk(2066, 2477);
            PerformAction(() => Me.UsePower(SNOPowerId.DemonHunter_Companion));

            Interact(cellar);


            PerformAction(() => Me.UsePower(SNOPowerId.DemonHunter_SmokeScreen));

            Walk(108, 158);
            Walk(129, 143);

            Walk(118, 138);

            TownRun();

            KillAll();
            PerformAction(() => Me.UsePower(SNOPowerId.DemonHunter_SmokeScreen));

            SnagIt.SnagItems();


            Snowshoes.Print(string.Format("Collected {0}k!", Math.Round((GetData(() => Me.Gold) - goldStart)/1000m, 1)));



            var srunTime = Math.Round((Environment.TickCount - ticks)/1000m, 0);
            _successes.Add(srunTime);
            Snowshoes.Print(String.Format("{0} secs success run ({1} avg); {2}% rate ({3}/{4})", srunTime,
                                          Math.Round(_successes.Average()),
                                          Math.Round((_successes.Count/(_failures.Count + (decimal) _successes.Count))*
                                                     100m),
                                          _successes.Count, _successes.Count + _failures.Count));
        }
    }
}