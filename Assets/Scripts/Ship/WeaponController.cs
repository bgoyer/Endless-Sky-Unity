using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Ship
{
    public class WeaponController : MonoBehaviour
    {
        private ObjectPooler objPool;
        private GameScripts game;

        private void Start()
        {
            objPool = ObjectPooler.Instance;
        }

        public void Shoot(GameObject ship, GameObject weaponGo)
        {
            if (ship.GetComponent<ShipVariables>().CanControl == true)
            {
                WeaponVariables weapon;
                weapon = weaponGo.GetComponent<WeaponVariables>();
                if (weapon.IsReloaded != true) return;
                if ((weapon.Lifetime > 1))
                {
                    weapon.IsReloaded = false;
                    weapon.Ammo = UnityEngine.Resources.Load<GameObject>(path: $"Prefabs/Projectiles/Cannon");
                    var ammoClone = objPool.SpawnFromPool("Bullet", weapon.transform.position, weapon.transform.rotation);
                    ammoClone.transform.Rotate(0, 0, Random.Range(-2.25f, 2.25f));
                    ammoClone.GetComponent<SpriteRenderer>().sprite =
                        UnityEngine.Resources.Load<Sprite>(path: $"Sprites/{weapon.ProjectileSprite}");
                    ammoClone.tag = $"{ship.transform.parent.tag}Bullet";
                    ammoClone.GetComponent<Rigidbody2D>().velocity = ship.GetComponent<Rigidbody2D>().velocity;
                    ammoClone.GetComponent<Rigidbody2D>()
                        .AddRelativeForce(ammoClone.transform.up * (float)weapon.Velocity);
                    ammoClone.GetComponent<BulletController>().ParentShip = ship;
                    StartCoroutine(ReloadGun(weapon));
                }
            }
        }

        private IEnumerator ReloadGun(WeaponVariables weapon)
        {
            yield return new WaitForSeconds((float)(weapon.Reload / (double)30));
            weapon.IsReloaded = true;
        }
    }
}