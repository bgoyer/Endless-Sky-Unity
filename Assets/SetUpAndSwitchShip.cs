using Assets.Scripts.Data.Services;
using Assets.Scripts.Ship;
using UnityEngine;

namespace Assets
{
    public class SetUpAndSwitchShip : MonoBehaviour
    {
        private readonly WeaponService weaponService = new WeaponService();
        private static GameObject _weaponPlaceHolder;
        private GameObject weaponClone;
        private void Start()
        {
            _weaponPlaceHolder = Resources.Load<GameObject>($"Prefabs/Weapons/Weapon");
        }
        public void CreateWeapon(GameObject ship)
        {
            var blaster = weaponService.GetByName("Energy Blaster");
            
            if (blaster.Category == "Guns")
            {
                foreach (Transform weaponSlot in ship.transform.GetChild(0).GetChild(0))
                {
                    if (weaponSlot.transform.childCount == 0)
                    {
                        weaponClone = Instantiate(_weaponPlaceHolder, weaponSlot.position, weaponSlot.rotation, weaponSlot);
                        break;
                    }
                }
            }

            WeaponController weaponCloneVar = weaponClone.GetComponent<WeaponController>();

            weaponCloneVar.FiringEnergy = blaster.FiringEnergy;
            weaponCloneVar.FiringHeat = blaster.FiringHeat;
            weaponCloneVar.ProjectileSprite = blaster.Sprite;
            weaponCloneVar.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Hardpoint/mod blaster turret");
            weaponCloneVar.Reload = blaster.Reload;
            weaponCloneVar.Inaccuracy = blaster.Inaccuracy;
            weaponCloneVar.Lifetime = blaster.Lifetime;
            weaponCloneVar.ShieldDamage = blaster.ShieldDamage;
            weaponCloneVar.HullDamage = blaster.HullDamage;
            weaponCloneVar.Velocity = blaster.Velocity * 10;
            weaponClone.SetActive(true);
        }
    }
}
