using Assets.Resources.Data.ModelData.Services;
using Assets.Scripts.Ship;
using UnityEngine;

namespace Assets.Resources.Data.ModelData
{
    public class SetUpAndSwitchShip : MonoBehaviour
    {
        private readonly WeaponService weaponService = new WeaponService();
        private readonly ThrusterService engineService = new ThrusterService();
        private static GameObject _weaponPlaceHolder;
        private static GameObject _thrusterPlaceHolder;
        private GameObject weaponClone;
        private GameObject thrusterClone;

        private void Start()
        {
            _weaponPlaceHolder = UnityEngine.Resources.Load<GameObject>($"Prefabs/Weapons/Weapon");
            _thrusterPlaceHolder = UnityEngine.Resources.Load<GameObject>($"Prefabs/Engines/Thruster");
        }

        public void CreateWeapon(GameObject ship, string gunToCreate)
        {
            var blaster = weaponService.GetByName(gunToCreate);

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

            WeaponVariables weaponCloneVar = weaponClone.GetComponent<WeaponVariables>();

            weaponCloneVar.FiringEnergy = blaster.FiringEnergy;
            weaponCloneVar.FiringHeat = blaster.FiringHeat;
            weaponCloneVar.ProjectileSprite = blaster.Sprite;
            weaponCloneVar.GetComponent<SpriteRenderer>().sprite = UnityEngine.Resources.Load<Sprite>("Sprites/Hardpoint/mod blaster turret");
            weaponCloneVar.Reload = blaster.Reload;
            weaponCloneVar.Inaccuracy = blaster.Inaccuracy;
            weaponCloneVar.Lifetime = blaster.Lifetime;
            weaponCloneVar.ShieldDamage = blaster.ShieldDamage;
            weaponCloneVar.HullDamage = blaster.HullDamage;
            weaponCloneVar.Velocity = blaster.Velocity * 50;
        }

        public void CreateEngine(GameObject ship, string engineToCreate)
        {
            var thruster = engineService.GetByName("Greyhound Plasma Thruster");

            foreach (Transform thrusterSlot in ship.transform.GetChild(1))
            {
                if (thrusterSlot.childCount == 0)
                {
                    thrusterClone = Instantiate(_thrusterPlaceHolder, thrusterSlot.position,
                        thrusterSlot.rotation, thrusterSlot);
                    ThrusterVariables thrusterClonevar = thrusterClone.GetComponent<ThrusterVariables>();
                    thrusterClonevar.ThrustEnergy = thruster.Thrust;
                    thrusterClonevar.MaxSpeed = 10;
                }
            }
        }
    }
}