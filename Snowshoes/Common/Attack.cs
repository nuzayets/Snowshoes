#region

using System;
using D3;
using Snowshoes.Classes;

#endregion

namespace Snowshoes.Common
{
    public static class Attack
    {
        #region Delegates

        public delegate bool UnitCheckCallback(Unit unit);

        #endregion

        private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(20);

        public static bool AttackUnit(Unit unit, TimeSpan timeout)
        {
            if (unit.Type == UnitType.Monster
                && unit.GetAttributeInteger(UnitAttribute.Is_NPC) == 0
                && unit.GetAttributeInteger(UnitAttribute.Is_Helper) == 0
                && unit.GetAttributeInteger(UnitAttribute.Invulnerable) == 0)
            {
                switch (Me.SNOId)
                {
                    case SNOActorId.Barbarian_Male:
                    case SNOActorId.Barbarian_Female:
                        //return Classes.Barbarian.AttackUnit(unit, timeout);
                        break;
                    case SNOActorId.WitchDoctor_Male:
                    case SNOActorId.WitchDoctor_Female:
                        //return Classes.WitchDoctor.AttackUnit(unit, timeout);
                        break;
                    case SNOActorId.Wizard_Male:
                    case SNOActorId.Wizard_Female:
                        //return Classes.Wizard.AttackUnit(unit, timeout);
                        break;
                    case SNOActorId.Demonhunter_Male:
                    case SNOActorId.Demonhunter_Female:
                        return DemonHunter.AttackUnit(unit, timeout);
                    case SNOActorId.Monk_Male:
                    case SNOActorId.Monk_Female:
                        return Monk.AttackUnit(unit, timeout);
                }
            }
            return false;
        }

        public static bool AttackUnit(Unit unit)
        {
            return AttackUnit(unit, DefaultTimeout);
        }
    }
}