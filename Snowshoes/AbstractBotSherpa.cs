using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Snowshoes.Common;

namespace Snowshoes
{
    abstract class AbstractBotSherpa : Sherpa
    {

        protected void startGame()
        {
            if (getBool(() => D3.Game.Ingame))
            {
                exitGame();
            }


            D3.UIElement okButton = getData<D3.UIElement>(() => D3.UIElement.Get(0xB4433DA3F648A992)); ;
            if (okButton != default(D3.UIElement) && getBool(() => okButton.Visible))
            {
                performAction(() => okButton.Click());
            }

            D3.UIElement resumeGame = null;
            while (resumeGame == null || !getBool(() => resumeGame.Visible))
            {
                resumeGame = getData<D3.UIElement>(() => D3.UIElement.Get(0x51A3923949DC80B7));
            }

            performAction(() => resumeGame.Click());

            waitFor(() => D3.Game.Ingame && D3.Me.LevelArea.ToString() != "Axe_Bad_Data");

        }

        protected void exitGame()
        {
            D3.UIElement ui = getData<D3.UIElement>(() => D3.UIElement.Get(0x5DB09161C4D6B4C6));

            if (ui != null)
            {
                
                waitForExclusivelyAfterAction(() => ui.Click(), () => !D3.Game.Ingame && D3.Me.LevelArea.ToString() == "Axe_Bad_Data");
            }
        }

        protected bool needsRepair()
        {
            var indicator = getData<D3.UIElement>(() => D3.UIElement.Get(0xBD8B3C3679D4F4D9));
            return (indicator != default(D3.UIElement) && indicator.Visible);
        }

        protected void goTown()
        {
            if (getBool(() => D3.Me.InTown)) return;

            performAction(() => D3.Me.UsePower(D3.SNOPowerId.UseStoneOfRecall));

            waitFor(() => D3.Me.InTown);
        }

        protected bool takePortal()
        {
            if (getBool(() => !D3.Me.InTown)) { return false; }

            D3.Unit[] units = getData<D3.Unit[]>(() => D3.Unit.Get());

            var unit = (from u in units where u.Type == D3.UnitType.Gizmo && u.ActorId == D3.SNOActorId.hearthPortal select u).FirstOrDefault();

            if (unit == default(D3.Unit))
            {
                return false;
            }

            for (int i = 0; i < 3; i++)
            {
                performAction(() => D3.Me.UsePower(D3.SNOPowerId.Axe_Operate_Gizmo, unit));
                Thread.Sleep(500);

                if (getBool(() => !D3.Me.InTown))
                {
                    break;
                }
            }

            return true;

        }



        protected void walk(float x, float y)
        {
            performAction(() => D3.Me.UsePower(D3.SNOPowerId.Walk, x, y, D3.Me.Z));
            while (GetDistance(x, y) > 10)
            {
                if (getBool(() => D3.Me.Mode != D3.UnitMode.Running))
                {
                    performAction(() => D3.Me.UsePower(D3.SNOPowerId.Walk, x, y, D3.Me.Z));
                }
                Thread.Sleep(100);
            }

        }

        protected void interact(D3.Unit u)
        {
            if (u.Type == D3.UnitType.Gizmo
                || u.Type == D3.UnitType.Monster
                || u.Type == D3.UnitType.Item)
            {
                walk(u.X, u.Y);
                performAction(() => D3.Me.UsePower(u.Type == D3.UnitType.Monster ? D3.SNOPowerId.Axe_Operate_NPC : D3.SNOPowerId.Axe_Operate_Gizmo, u));
            }
        }

        protected void interact(string name)
        {
            var unit = getData<D3.Unit>(() => D3.Unit.Get().Where(x => x.Name.Contains(name)).FirstOrDefault());
            interact(unit);
        }

        protected void repairAll()
        {
            var btn = getData<D3.UIElement>(() => D3.UIElement.Get(0x80F5D06A035848A5));
            if (btn != default(D3.UIElement))
            {
                btn.Click();
            }
        }

        protected void killAll()
        {
            var mobs = waitForMobs(0);
            while (mobs.Length > 0)
            {
                killThese(mobs);
                mobs = waitForMobs(0);
            }
        }

        protected D3.Unit[] waitForMobs(uint timeout)
        {
            var mobs = getData<D3.Unit[]>(() => D3.Unit.Get().Where(x => x.Type == D3.UnitType.Monster && ((uint)x.MonsterType == 0 || (uint)x.MonsterType == 4) && x.Mode != D3.UnitMode.Warping && x.Life > 0
                && x.GetAttributeInteger(D3.UnitAttribute.Is_NPC) == 0 && x.GetAttributeInteger(D3.UnitAttribute.Is_Helper) == 0
                && x.GetAttributeInteger(D3.UnitAttribute.Invulnerable) == 0 && x.ActorId != D3.SNOActorId.DemonHunter_SpikeTrapRune_multiTrap_Proxy).ToArray());
            uint count = 0;
            while (mobs.Length <= 0 && count < 2 * timeout)
            {
                Thread.Sleep(500);
                mobs = mobs = getData<D3.Unit[]>(() => D3.Unit.Get().Where(x => x.Type == D3.UnitType.Monster && ((uint)x.MonsterType == 0 || (uint)x.MonsterType == 4) && x.Mode != D3.UnitMode.Warping && x.Life > 0
                && x.GetAttributeInteger(D3.UnitAttribute.Is_NPC) == 0 && x.GetAttributeInteger(D3.UnitAttribute.Is_Helper) == 0
                && x.GetAttributeInteger(D3.UnitAttribute.Invulnerable) == 0 && x.ActorId != D3.SNOActorId.DemonHunter_SpikeTrapRune_multiTrap_Proxy).ToArray());
                ++count;
            }
            return mobs;
        }

        protected void killThese(D3.Unit[] units)
        {
            units = units.OrderBy(u1 => GetDistance(u1.X, u1.Y, getData<float>(() => D3.Me.X), getData<float>(() => D3.Me.Y))).ToArray();
            for (uint i = 0; i < units.Length; ++i)
            {
                if (getBool(() => (units[i].Valid && units[i].Life > 0)))
                {
                    Attack.AttackUnit(units[i]);
                    Thread.Sleep(154);
                }
            }
        }
    }
}
