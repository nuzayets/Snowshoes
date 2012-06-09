using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Snowshoes.Common;

namespace Snowshoes.Bots
{
    class Sarkoth : AbstractBotSherpa
    {

        List<decimal> successes = new List<decimal>();
        List<decimal> failures = new List<decimal>();

        public Sarkoth()
        {
            new Common.Watchdog(2500, this);
            new Common.DeathMonitor(250, this);
        }


        private bool isInventoryStuffed()
        {
            return getBool(() => D3.Me.GetContainerItems(D3.Container.Inventory).Sum(item => item.ItemSizeX * item.ItemSizeY) >= 40);
        }

        private void repairAndSell()
        {
            if (needsRepair() || isInventoryStuffed())
            {

                goTown();

                walk(2897, 2786); // walk to Tashun


                performAction(() => D3.UIElement.Get(0xDAECD77F9F0DCFB).Click()); // open your inventory - necessary for identify

                Thread.Sleep(500); // eh. just in case.

                D3.Unit[] inventory = getData<D3.Unit[]>(() => D3.Me.GetContainerItems(D3.Container.Inventory));



                foreach (D3.Unit item in inventory)
                {

                    if (!item.ItemIdentified)
                    {
                        performAction(() => item.UseItem());
                        waitFor(() => item.ItemIdentified);
                    }

                }

                Thread.Sleep(750);

                interact("Tashun the Miner");

                Thread.Sleep(500);

                repairAll();






                foreach (D3.Unit item in getData<D3.Unit[]>(() => D3.Me.GetContainerItems(D3.Container.Inventory)))
                {
                    if (item.ItemQuality >= D3.UnitItemQuality.Magic1 && item.ItemQuality < D3.UnitItemQuality.Legendary)
                    {
                        float wpMax = item.GetAttributeInteger(D3.UnitAttribute.Damage_Weapon_Max_Total_All);
                        float wpMin = item.GetAttributeInteger(D3.UnitAttribute.Damage_Weapon_Min_Total_All);
                        float mf = item.GetAttributeReal(D3.UnitAttribute.Magic_Find);
                        float gf = item.GetAttributeReal(D3.UnitAttribute.Gold_Find);


                        if (!(wpMax / wpMin >= 750 || mf >= 0.19 || gf >= 0.19))
                        {
                            performAction(() => item.SellItem());
                            Thread.Sleep(50);
                        }
                    }

                }

            
            walk(2977, 2799);
            takePortal();

            }
        }

        protected override void loop()
        {
            
            startGame();

    
            int ticks = System.Environment.TickCount;

            int gold_start = getData<int>(() => D3.Me.Gold);

            walk(1995, 2603);
            performAction(() => D3.Me.UsePower(D3.SNOPowerId.DemonHunter_SmokeScreen));
            walk(2025, 2563);
            performAction(() => D3.Me.UsePower(D3.SNOPowerId.DemonHunter_SmokeScreen));
            walk(2057, 2528);
            performAction(() => D3.Me.UsePower(D3.SNOPowerId.DemonHunter_SmokeScreen));
            walk(2081, 2487);
            var cellar = getData<D3.Unit>(() => D3.Unit.Get().FirstOrDefault(u => u.Name.Contains("Dank Cellar")));
            if (cellar == default(D3.Unit))
            {
                //goTown();
                exitGame();
                decimal runTime = Math.Round((System.Environment.TickCount - ticks) / 1000m, 0);
                failures.Add(runTime);
                Snowshoes.Print(String.Format("{0} secs failure run ({1} avg); {2}% success rate", runTime, Math.Round(failures.Average()), Math.Round(((decimal)successes.Count/((decimal)failures.Count+(decimal)successes.Count))*100m),0));
                return;
            }


            performAction(() => D3.Me.UsePower(D3.SNOPowerId.DemonHunter_SmokeScreen));
            walk(2081, 2487);
            performAction(() => D3.Me.UsePower(D3.SNOPowerId.DemonHunter_Preparation));
            walk(2066, 2477);
            performAction(() => D3.Me.UsePower(D3.SNOPowerId.DemonHunter_Companion));

            interact(cellar);

            

            performAction(() => D3.Me.UsePower(D3.SNOPowerId.DemonHunter_SmokeScreen));

            walk(108, 158);
            walk(129, 143);
            
            walk(118, 138);

            repairAndSell();

            killAll();
            performAction(() => D3.Me.UsePower(D3.SNOPowerId.DemonHunter_SmokeScreen));

            SnagIt.SnagItems();


            Snowshoes.Print(string.Format("Collected {0}k!", Math.Round((getData<int>(()=>D3.Me.Gold) - gold_start) / 1000m, 1)));
            

            //goTown();
            

            decimal srunTime = Math.Round((System.Environment.TickCount - ticks) / 1000m, 0);
            successes.Add(srunTime);
            Snowshoes.Print(String.Format("{0} secs success run ({1} avg); {2}% rate ({3}/{4})", srunTime, Math.Round(successes.Average()) , Math.Round(((decimal)successes.Count / ((decimal)failures.Count + (decimal)successes.Count)) * 100m), successes.Count, successes.Count + failures.Count));
            

        }

    }
}
