#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using D3;
using Snowshoes.Classes;
using Snowshoes.Common;

#endregion

namespace Snowshoes.Bots
{
    internal class Sarkoth : AbstractBotSherpa
    {
        private readonly List<decimal> _failures = new List<decimal>();
        private readonly List<decimal> _successes = new List<decimal>();

        private int ticks = 0;

        public Sarkoth()
        {
            new Watchdog(2500, this);
            new DeathMonitor(1500, this);
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

            MoveReallyFast(2966, 2825);
            MoveReallyFast(2941.5f, 2850.7f);
            Interact("Salvage", false);
            SnagIt.SalvageItems();

            MoveReallyFast(2940, 2813);
            MoveReallyFast(2895, 2782);
            Interact("Tashun the Miner", false);
            RepairAll();
            SnagIt.SellItems();

            MoveReallyFast(2933, 2789);
            MoveReallyFast(2969, 2791);
            Interact("Stash", false);
            SnagIt.StashItems();


            MoveReallyFast(2977, 2799);
            TakePortal();
        }

        protected override void Loop()
        {
            StartGame();


            ticks = Environment.TickCount;

            var goldStart = GetData(() => Me.Gold);


            switch (Me.SNOId)
            {
                case SNOActorId.Barbarian_Male:
                case SNOActorId.Barbarian_Female:
                    throw new NotImplementedException();
                    //break;
                case SNOActorId.WitchDoctor_Male:
                case SNOActorId.WitchDoctor_Female:
                    throw new NotImplementedException();
                    //break;
                case SNOActorId.Wizard_Male:
                case SNOActorId.Wizard_Female:
                    if (!WizardCellarRun()) return;
                    break;
                case SNOActorId.Demonhunter_Male:
                case SNOActorId.Demonhunter_Female:
                    if (!DemonHunterCellarRun()) return;
                    break;
                case SNOActorId.Monk_Male:
                case SNOActorId.Monk_Female:
                    throw new NotImplementedException();
                    //break;
            }


            SnagIt.SnagItems();


            Snowshoes.Print(string.Format("Collected {0}k!", Math.Round((GetData(() => Me.Gold) - goldStart)/1000m, 1)));
            var srunTime = Math.Round((Environment.TickCount - ticks)/1000m, 0);
            _successes.Add(srunTime);
            Snowshoes.Print(String.Format(
                "{0} secs success run ({1} avg); {2}% rate ({3}/{4})", 
                srunTime,
                Math.Round(_successes.Average()),
                Math.Round((_successes.Count/(_failures.Count + (decimal) _successes.Count))*100m),
                _successes.Count, 
                _successes.Count + _failures.Count
            ));
        }
        private bool WizardCellarRun()
        {
            WaitFor(() => Me.UsePower(SNOPowerId.Wizard_DiamondSkin));

            TeleportTo(1994.874f, 2619.641f);
            TeleportTo(2013.623f, 2589.112f);
            TeleportTo(2033.791f, 2560.457f);
            TeleportTo(2054.950f, 2534.024f);
            Walk(2052.285f, 2530.787f);

            Unit cellar = CheckForCellar();
            if (cellar == null) return false;

            Walk(2063.089f, 2480.32f);
            Interact(cellar);
            WaitFor(() => Me.UsePower(SNOPowerId.Wizard_Familiar));
            WaitFor(() => Me.UsePower(SNOPowerId.Wizard_MagicWeapon));

            Walk(122.7955f, 154.2372f);

            KillAll();
            TownRun();

            return true;
        }




        private bool DemonHunterCellarRun()
        {
            WaitFor(() => Me.UsePower(SNOPowerId.DemonHunter_Vault, 1993f, 2603f, Me.Z));
            WaitFor(() => Me.UsePower(SNOPowerId.DemonHunter_SmokeScreen));

            Walk(2026.3f, 2557.1f);

            Thread.Sleep(Game.Ping);
            Unit cellar = CheckForCellar();
            if (cellar == null) return false;

            Walk(2046.2f, 2527.7f);
            WaitFor(() => Me.UsePower(SNOPowerId.DemonHunter_Preparation));

            WaitFor(() => Me.UsePower(SNOPowerId.DemonHunter_SmokeScreen));
            Walk(2078.7f, 2492f);

            Walk(2066, 2477);
            PerformAction(() => Me.UsePower(SNOPowerId.DemonHunter_Companion));

            Interact(cellar);

            WaitFor(() => Me.UsePower(SNOPowerId.DemonHunter_Vault, 121.5f, 157.4f, Me.Z));

            Walk(120.5f, 137.8f);

            TownRun();

            KillAll();
            WaitFor(() => Me.UsePower(SNOPowerId.DemonHunter_SmokeScreen));
            return true;
        }

        private Unit CheckForCellar()
        {
            var cellar = GetData(() => Unit.Get().FirstOrDefault(u => u.Name.Contains("Dank Cellar")));
            if (cellar == default(Unit))
            {
                var runTime = Math.Round((Environment.TickCount - ticks)/1000m, 0);
                _failures.Add(runTime);
                Snowshoes.Print(String.Format("{0} secs failure run ({1} avg); {2}% success rate", runTime,
                                              Math.Round(_failures.Average()),
                                              Math.Round((_successes.Count/
                                                          (_failures.Count + (decimal) _successes.Count))*
                                                         100m)));
                ExitGame();
                return null;
            }
            return cellar;
        }
    }
}