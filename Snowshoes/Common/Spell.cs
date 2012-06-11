#region

using System;
using System.Linq;
using System.Threading;
using D3;

#endregion

namespace Snowshoes.Common
{
    public class Spell
    {
        protected bool Available;
        protected int Cooldown = -1;
        protected bool Forced;
        protected SNOPowerId Id;
        protected int LastTick = Int32.MinValue;
        protected int PrimCost;
        protected int SecCost;

        public Spell(SNOPowerId id, int cooldown, int cost1, int cost2, bool available)
        {
            Id = id;
            Cooldown = cooldown;
            PrimCost = cost1;
            SecCost = cost2;
            Available = available;
            Forced = true;
            Init();
        }

        public Spell(SNOPowerId id, int cooldown, int cost1, int cost2)
        {
            Id = id;
            Cooldown = cooldown;
            PrimCost = cost1;
            SecCost = cost2;
            Init();
        }

        public void Init()
        {
            if (!Forced)
                Available = Sherpa.GetBool(() => Me.Skills.Contains(Id));
        }

        public bool Use(Unit target)
        {
            if (Available && Sherpa.GetBool(() => Me.PrimaryResource >= PrimCost && Me.SecondaryResource >= SecCost) &&
                Environment.TickCount > LastTick + Cooldown*1000 + 100)
            {
                Sherpa.PerformAction(() => Me.UsePower(Id, target));
                LastTick = Environment.TickCount;
                Thread.Sleep(75);
                return true;
            }
            return false;
        }

        public bool Use(float x, float y)
        {
            if (Available && Environment.TickCount > LastTick + Cooldown*1000 + 100)
            {
                Sherpa.PerformAction(() => Me.UsePower(Id, x, y, Me.Z));
                LastTick = Environment.TickCount;
                Thread.Sleep(75);
                return true;
            }
            return false;
        }

        public bool Use()
        {
            if (Available && Environment.TickCount > LastTick + Cooldown*1000 + 100)
            {
                Sherpa.PerformAction(() => Me.UsePower(Id));
                LastTick = Environment.TickCount;
                Thread.Sleep(75);
                return true;
            }
            return false;
        }
    }
}