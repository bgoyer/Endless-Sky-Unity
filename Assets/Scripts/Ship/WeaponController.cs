using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Ship
{
    public class WeaponController : MonoBehaviour
    {

        public void Shoot(GameObject ship, GameObject weaponGO)
        {
            WeaponVariables weapon;
            weapon = weaponGO.GetComponent<WeaponVariables>();
            if (!(weapon.Lifetime > 1) || weapon.IsReloaded != true) return;
            weapon.IsReloaded = false;
            weapon.Ammo = UnityEngine.Resources.Load<GameObject>(path: $"Prefabs/Projectiles/Cannon");
            var ammoClone = Instantiate(original: weapon.Ammo, GameObject.Find("/GarbageHolder").transform,
                worldPositionStays: true);
            ammoClone.transform.position = weapon.transform.position;
            ammoClone.transform.rotation = ship.transform.rotation;
            ammoClone.GetComponent<SpriteRenderer>().sprite = UnityEngine.Resources.Load<Sprite>(path: $"Sprites/{weapon.ProjectileSprite}");
            ammoClone.tag = $"{ship.transform.parent.tag}Bullet";
            ammoClone.GetComponent<Rigidbody2D>().velocity = ship.GetComponent<Rigidbody2D>().velocity;
            ammoClone.GetComponent<Rigidbody2D>().AddForce(force: ship.transform.up * (float) weapon.Velocity);

            StartCoroutine(reloadGun(weapon));
        }


        private IEnumerator reloadGun(WeaponVariables weapon)
        {
            yield return new WaitForSeconds((float)(weapon.Reload / (double)30));
            weapon.IsReloaded = true;
        }
    }
}
