#region

using System.Linq;
using System.Threading;
using D3;
using Snowshoes.Common;

#endregion

namespace Snowshoes
{
    internal abstract class AbstractBotSherpa : Sherpa
    {
        protected static void StartGame()
        {
            if (GetBool(() => Game.Ingame))
            {
                ExitGame();
            }


            var okButton = GetData(() => UIElement.Get(0xB4433DA3F648A992));
            if (okButton != default(UIElement) && GetBool(() => okButton.Visible))
            {
                PerformAction(okButton.Click);
            }

            UIElement[] resumeGame = {null};
            while (resumeGame[0] == null || GetBool(() => !resumeGame[0].Visible))
            {
                resumeGame[0] = GetData(() => UIElement.Get(0x51A3923949DC80B7));
            }

            PerformAction(() => resumeGame[0].Click());

            WaitFor(() => Game.Ingame);
        }

        protected static void ExitGame()
        {
            var ui = GetData(() => UIElement.Get(0x5DB09161C4D6B4C6));

            if (ui != null)
            {
                WaitForExclusivelyAfterAction(ui.Click,
                                              () => !Game.Ingame);
            }
        }

        protected static bool NeedsRepair()
        {
            var indicator = GetData(() => UIElement.Get(0xBD8B3C3679D4F4D9));
            return (indicator != default(UIElement) && indicator.Visible);
        }

        protected static void GoTown()
        {
            if (GetBool(() => Me.InTown)) return;

            PerformAction(() => Me.UsePower(SNOPowerId.UseStoneOfRecall));

            WaitFor(() => Me.InTown);
        }

        protected static bool TakePortal()
        {
            if (GetBool(() => !Me.InTown))
            {
                return false;
            }

            var units = GetData(Unit.Get);

            var unit =
                (from u in units where u.Type == UnitType.Gizmo && u.ActorId == SNOActorId.hearthPortal select u).
                    FirstOrDefault();

            if (unit == default(Unit))
            {
                return false;
            }

            for (var i = 0; i < 3; i++)
            {
                PerformAction(() => Me.UsePower(SNOPowerId.Axe_Operate_Gizmo, unit));
                Thread.Sleep(500);

                if (GetBool(() => !Me.InTown))
                {
                    break;
                }
            }

            return true;
        }


        protected static void Walk(float x, float y)
        {
            PerformAction(() => Me.UsePower(SNOPowerId.Walk, x, y, Me.Z));
            while (GetDistance(x, y) > 10)
            {
                if (GetBool(() => Me.Mode != UnitMode.Running))
                {
                    PerformAction(() => Me.UsePower(SNOPowerId.Walk, x, y, Me.Z));
                }
                Thread.Sleep(100);
            }
        }

        protected static void Interact(Unit u)
        {
            if (u.Type != UnitType.Gizmo && u.Type != UnitType.Monster && u.Type != UnitType.Item) return;

            Walk(u.X, u.Y);
            PerformAction(
                () =>
                Me.UsePower(u.Type == UnitType.Monster ? SNOPowerId.Axe_Operate_NPC : SNOPowerId.Axe_Operate_Gizmo,
                            u));
        }

        protected static void Interact(string name)
        {
            var unit = GetData(() => Unit.Get().Where(x => x.Name.Contains(name)).FirstOrDefault());
            Interact(unit);
        }

        protected static void RepairAll()
        {
            var btn = GetData(() => UIElement.Get(0x80F5D06A035848A5));
            if (btn != default(UIElement))
            {
                btn.Click();
            }
        }

        protected static void KillAll()
        {
            var mobs = WaitForMobs(0);
            while (mobs.Length > 0)
            {
                KillThese(mobs);
                mobs = WaitForMobs(0);
            }
        }

        protected static Unit[] WaitForMobs(uint timeout)
        {
            var mobs =
                GetData(
                    () =>
                    Unit.Get().Where(
                        x =>
                        x.Type == UnitType.Monster && ((uint) x.MonsterType == 0 || (uint) x.MonsterType == 4) &&
                        x.Mode != UnitMode.Warping && x.Life > 0
                        && x.GetAttributeInteger(UnitAttribute.Is_NPC) == 0 &&
                        x.GetAttributeInteger(UnitAttribute.Is_Helper) == 0
                        && x.GetAttributeInteger(UnitAttribute.Invulnerable) == 0 &&
                        x.ActorId != SNOActorId.DemonHunter_SpikeTrapRune_multiTrap_Proxy).ToArray());
            uint count = 0;
            while (mobs.Length <= 0 && count < 2*timeout)
            {
                Thread.Sleep(500);
                mobs =
                    GetData(
                        () =>
                        Unit.Get().Where(
                            x =>
                            x.Type == UnitType.Monster && ((uint) x.MonsterType == 0 || (uint) x.MonsterType == 4) &&
                            x.Mode != UnitMode.Warping && x.Life > 0
                            && x.GetAttributeInteger(UnitAttribute.Is_NPC) == 0 &&
                            x.GetAttributeInteger(UnitAttribute.Is_Helper) == 0
                            && x.GetAttributeInteger(UnitAttribute.Invulnerable) == 0 &&
                            x.ActorId != SNOActorId.DemonHunter_SpikeTrapRune_multiTrap_Proxy).ToArray());
                ++count;
            }
            return mobs;
        }

        protected static void KillThese(Unit[] units)
        {
            units = units.OrderBy(u1 => GetDistance(u1.X, u1.Y, GetData(() => Me.X), GetData(() => Me.Y))).ToArray();
            for (uint i = 0; i < units.Length; ++i)
            {
                var i1 = i;
                if (!GetBool(() => (units[i1].Valid && units[i1].Life > 0))) continue;
                Attack.AttackUnit(units[i]);
                Thread.Sleep(154);
            }
        }
    }
}