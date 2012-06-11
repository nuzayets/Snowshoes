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

        private void RepairAndSell()
        {
            if (!NeedsRepair() && !IsInventoryStuffed()) return;
            GoTown();

            Walk(2897, 2786); // walk to Tashun


            PerformAction(() => UIElement.Get(0xDAECD77F9F0DCFB).Click());
            // open your inventory - necessary for identify

            Thread.Sleep(500); // eh. just in case.

            var inventory = GetData(() => Me.GetContainerItems(Container.Inventory));


            foreach (var item in inventory.Where(item => !item.ItemIdentified))
            {
                PerformAction(() => item.UseItem());
                WaitFor(() => item.ItemIdentified);
            }

            Thread.Sleep(750);

            Interact("Tashun the Miner");

            Thread.Sleep(500);

            RepairAll();


            foreach (var item1 in from item in GetData(() => Me.GetContainerItems(Container.Inventory))
                                  where
                                      item.ItemQuality >= UnitItemQuality.Magic1 &&
                                      item.ItemQuality < UnitItemQuality.Legendary
                                  let wpMax = item.GetAttributeInteger(UnitAttribute.Damage_Weapon_Max_Total_All)
                                  let wpMin = item.GetAttributeInteger(UnitAttribute.Damage_Weapon_Min_Total_All)
                                  let mf = item.GetAttributeReal(UnitAttribute.Magic_Find)
                                  let gf = item.GetAttributeReal(UnitAttribute.Gold_Find)
                                  where !(wpMax/(float) wpMin >= 750 || mf >= 0.19 || gf >= 0.19)
                                  select item)
            {
                PerformAction(item1.SellItem);
                Thread.Sleep(50);
            }


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
                //goTown();
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

            RepairAndSell();

            KillAll();
            PerformAction(() => Me.UsePower(SNOPowerId.DemonHunter_SmokeScreen));

            SnagIt.SnagItems();


            Snowshoes.Print(string.Format("Collected {0}k!", Math.Round((GetData(() => Me.Gold) - goldStart)/1000m, 1)));


            //goTown();


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