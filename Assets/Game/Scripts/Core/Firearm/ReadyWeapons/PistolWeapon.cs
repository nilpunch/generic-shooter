using UnityEngine;

namespace SM.FPS
{
    public class PistolWeapon : Weapon
    {
        [SerializeField] private TriggerPull _triggerPull;
        [SerializeField] private WeaponAimedAttack _weaponAim;
        [SerializeField] private MagazineWeaponAttack _magazine;
        
        public override ITrigger MainFire => _triggerPull;
        public override IWeaponAim Aim => _weaponAim;
        public override IWeaponMagazine Magazine => _magazine;
    }
}
