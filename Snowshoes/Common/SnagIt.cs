using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

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


            var items = Sherpa.getData<D3.Unit[]>(() => D3.Unit.Get().Where(x => x.Type == D3.UnitType.Item && x.ItemContainer == D3.Container.Unknown && CheckItem(x)).ToArray());
            while (items.Any()) 
            {
                Sherpa.performAction(() => D3.Me.UsePower(items[0].Type == D3.UnitType.Gizmo || items[0].Type == D3.UnitType.Item ? D3.SNOPowerId.Axe_Operate_Gizmo : D3.SNOPowerId.Axe_Operate_NPC, items[0]));
                Thread.Sleep(250);
                items = Sherpa.getData<D3.Unit[]>(() => D3.Unit.Get().Where(x => x.Type == D3.UnitType.Item && x.ItemContainer == D3.Container.Unknown && CheckItem(x)).ToArray());
            }
        }

        public static bool CheckItem(D3.Unit unit)
        {
            return unit.ActorId == D3.SNOActorId.GoldCoins
                || unit.ActorId == D3.SNOActorId.GoldLarge
                || unit.ActorId == D3.SNOActorId.GoldMedium
                || unit.ActorId == D3.SNOActorId.GoldSmall
                || unit.Name.Contains("Flawless") // gems
                || unit.Name.Contains("Plan")
                || unit.Name.Contains("Design")
                || unit.Name.Contains("Book ") // crafting materials
                || unit.Name.Contains("Tome")
                || unit.Name.Contains("Mythic") // Health potions
                || unit.ItemQuality >= D3.UnitItemQuality.Magic1;
        }
    }
}
