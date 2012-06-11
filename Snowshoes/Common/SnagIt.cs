#region

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
            //var items = Sherpa.getData<D3.Unit[]>(() => D3.Unit.Get().Where(x => x.Type == D3.UnitType.Item && x.ItemContainer == D3.Container.Unknown && CheckItem(x)).OrderBy(i => Sherpa.GetDistance(i.X, i.Y, Sherpa.getData<float>(() => D3.Me.X), Sherpa.getData<float>(() => D3.Me.Y))).ToArray());
            //while (items.Any())
            //{
            //    while (items[0].Valid == true && items[0].ItemContainer != D3.Container.Inventory)
            //    {
            //        Sherpa.performAction(() => D3.Me.UsePower(items[0].Type == D3.UnitType.Gizmo || items[0].Type == D3.UnitType.Item ? D3.SNOPowerId.Axe_Operate_Gizmo : D3.SNOPowerId.Axe_Operate_NPC, items[0]));
            //        Thread.Sleep(100);
            //    }
            //    items = items.Where(x => x.Type == D3.UnitType.Item && x.ItemContainer == D3.Container.Unknown).OrderBy(i => Sherpa.GetDistance(i.X, i.Y, Sherpa.getData<float>(() => D3.Me.X), Sherpa.getData<float>(() => D3.Me.Y))).ToArray();
            //}


            var items =
                Sherpa.GetData(
                    () =>
                    Unit.Get().Where(
                        x => x.Type == UnitType.Item && x.ItemContainer == Container.Unknown && CheckItem(x)).ToArray());
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
                            x => x.Type == UnitType.Item && x.ItemContainer == Container.Unknown && CheckItem(x)).
                            ToArray());
            }
        }

        public static bool CheckItem(Unit unit)
        {
            return unit.ActorId == SNOActorId.GoldCoins
                   || unit.ActorId == SNOActorId.GoldLarge
                   || unit.ActorId == SNOActorId.GoldMedium
                   || unit.ActorId == SNOActorId.GoldSmall
                   || unit.Name.Contains("Flawless") // gems
                   || unit.Name.Contains("Plan")
                   || unit.Name.Contains("Design")
                   || unit.Name.Contains("Book ") // crafting materials
                   || unit.Name.Contains("Tome")
                   || unit.Name.Contains("Mythic") // Health potions
                   || unit.ItemQuality >= UnitItemQuality.Magic1;
        }
    }
}