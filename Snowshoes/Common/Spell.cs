#region

using D3;

#endregion

namespace Snowshoes.Common
{
    public class Spell
    {
        protected SNOPowerId Id;
        protected int PrimCost;
        protected int SecCost;

        public Spell(SNOPowerId id, int cost1, int cost2)
        {
            Id = id;
            PrimCost = cost1;
            SecCost = cost2;
        }


        public bool IsAvailableNow()
        {
            return Sherpa.GetBool(
                       () => Me.PrimaryResource >= PrimCost && Me.SecondaryResource >= SecCost && Me.IsSkillReady(Id));
        }

        public bool Use(Unit target)
        {
            return IsAvailableNow() && Sherpa.GetBool(() => Me.UsePower(Id, target));
        }

        public bool Use(float x, float y)
        {
            return IsAvailableNow() && Sherpa.GetBool(() => Me.UsePower(Id, x, y, Me.Z));
        }

        public bool Use()
        {
            return IsAvailableNow() && Sherpa.GetBool(() => Me.UsePower(Id));
        }
    }
}