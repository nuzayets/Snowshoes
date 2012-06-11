#region

using System;
using System.Linq;
using System.Threading;
using D3;

#endregion

namespace Snowshoes.Common
{
    public static class SnagIt
    {
        public static void SnagItems()
        {
            var items =
                Sherpa.GetData(
                    () =>
                    Unit.Get().Where(
                        x => x.Type == UnitType.Item && x.ItemContainer == Container.Unknown && CheckItemSnag(x)).
                        ToArray());
            while (items.Any())
            {
                var items1 = items;
                Sherpa.PerformAction(
                    () =>
                    Me.UsePower(
                        items1[0].Type == UnitType.Gizmo || items1[0].Type == UnitType.Item
                            ? SNOPowerId.Axe_Operate_Gizmo
                            : SNOPowerId.Axe_Operate_NPC, items1[0]));
                Thread.Sleep(250);
                items =
                    Sherpa.GetData(
                        () =>
                        Unit.Get().Where(
                            x => x.Type == UnitType.Item && x.ItemContainer == Container.Unknown && CheckItemSnag(x)).
                            ToArray());
            }
        }

        public static void IdentifyAll()
        {
            Sherpa.PerformAction(() => UIElement.Get(0xDAECD77F9F0DCFB).Click());
            // open your inventory - necessary for identify

            Thread.Sleep(Game.Ping*2); // eh. just in case.

            var inventory = Sherpa.GetData(() => Me.GetContainerItems(Container.Inventory));

            foreach (var item in inventory.Where(item => !item.ItemIdentified))
            {
                var item1 = item;
                Sherpa.PerformAction(() => item1.UseItem());
                Sherpa.WaitFor(() => item1.ItemIdentified);
            }
        }

        public static bool CheckItemStash(Unit i)
        {
            return i.ItemQuality >= UnitItemQuality.Rare4
                   || i.Name.Contains("Topaz (30)") || i.Name.Contains("Amethyst (30)") ||
                   i.Name.Contains("Emerald (30)") || i.Name.Contains("Ruby (30)")
                   || i.Name.Contains("Essence (100)") || i.Name.Contains("Tear (100)") || i.Name.Contains("Hoof (100)") ||
                   i.Name.Contains("Plan");
        }

        public static bool CheckItemSnag(Unit unit)
        {
            return unit.ActorId == SNOActorId.GoldCoins
                   || unit.ActorId == SNOActorId.GoldLarge
                   || unit.ActorId == SNOActorId.GoldMedium
                   || unit.ActorId == SNOActorId.GoldSmall
                   || unit.Name.Contains("Flawless") // gems
                   || unit.Name.Contains("Plan")
                   || unit.Name.Contains("Design")
                   || unit.Name.Contains("Book") // crafting materials
                   || unit.Name.Contains("Tome")
                   || unit.Name.Contains("Mythic") // Health potions
                   || unit.ItemQuality >= UnitItemQuality.Magic1;
        }

        public static bool CheckItemSalvage(Unit unit)
        {
            return !unit.Name.Contains("Book ") // crafting materials
                   && !unit.Name.Contains("Tome")
                   && !unit.Name.Contains("Plan")
                   && !unit.Name.Contains("Essence")
                   && !unit.Name.Contains("Tear")
                   && !unit.Name.Contains("Hoof")
                   && !unit.Name.Contains("Brimstone")
                   && unit.ItemQuality < UnitItemQuality.Legendary
                   && unit.ItemQuality >= UnitItemQuality.Magic1
                   && unit.ItemLevelRequirement == 60;
        }

        public static bool CheckItemSell(Unit unit)
        {
            return !unit.Name.Contains("Book ") // crafting materials
                   && !unit.Name.Contains("Tome")
                   && !unit.Name.Contains("Plan")
                   && !unit.Name.Contains("Essence")
                   && !unit.Name.Contains("Tear")
                   && !unit.Name.Contains("Hoof")
                   && !unit.Name.Contains("Brimstone")
                   && unit.ItemQuality < UnitItemQuality.Legendary
                   && unit.ItemQuality >= UnitItemQuality.Magic1
                   && unit.ItemLevelRequirement < 60;
        }

        internal static void SalvageItems()
        {
            foreach (
                var item in
                    Sherpa.GetData(() => Me.GetContainerItems(Container.Inventory)).Where(CheckItemSalvage))
            {
                Sherpa.GetBool(item.SalvageItem);
                Thread.Sleep(50);
            }
        }

        internal static void SellItems()
        {
            foreach (
                var item in
                    Sherpa.GetData(() => Me.GetContainerItems(Container.Inventory)).Where(CheckItemSell))
            {
                Sherpa.PerformAction(item.SellItem);
                Thread.Sleep(50);
            }
        }

        internal static void StashItems()
        {
            foreach (
                var item in
                    Sherpa.GetData(() => Me.GetContainerItems(Container.Inventory)).Where(CheckItemStash))
            {
                Sherpa.GetBool(item.UseItem);
                Thread.Sleep(50);
            }
        }
    }
}