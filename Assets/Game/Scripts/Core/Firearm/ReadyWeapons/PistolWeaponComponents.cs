using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SM.FPS
{
    public class PistolWeaponComponents : WeaponComponents
    {
        [SerializeField] private TriggerPull _triggerPull;
        [SerializeField] private MagazineWeaponAttack _magazine;
        
        public override ITrigger MainFire => _triggerPull;
        public override ITrigger AlterFire => null;
        public override IWeaponMagazine Magazine => _magazine;
        public override IFiringModeSwitch FiringModeSwitch => null;
    }
}
