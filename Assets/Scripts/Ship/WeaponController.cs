using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Ship
{
    public class WeaponController : MonoBehaviour
    {
        public string Sprite;
        public string Sound;
        public string ProjectileSprite;
        //public string HitEffect;
        public double Inaccuracy;
        public double Velocity;
        public double Lifetime;
        public double Reload;
        public double FiringEnergy;
        public double FiringHeat;
        public double ShieldDamage;
        public double HullDamage;

        private bool isReloaded;
        private GameObject bulletHolder;
        private GameObject ammo;
        private GameObject ship;

        private void Start()
        {
            isReloaded = true;
            bulletHolder = GameObject.Find("/GarbageHolder");
            ship = this.transform.parent.parent.parent.parent.gameObject;
        }
        public void Shoot(float shipVelocity)
        {
            if (Lifetime > 1 && isReloaded == true)
            {
                isReloaded = false;
                ammo = Resources.Load<GameObject>($"Prefabs/Projectiles/Cannon");
                GameObject ammoClone = Instantiate(ammo, bulletHolder.transform, true);
                ammoClone.transform.position = this.transform.position;
                ammoClone.transform.rotation = this.transform.rotation;
                ammoClone.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Sprites/{ProjectileSprite}");
                ammoClone.tag = "PlayerBullet";
                ammoClone.GetComponent<Rigidbody2D>().velocity = ship.GetComponent<Rigidbody2D>().velocity;
                ammoClone.GetComponent<Rigidbody2D>().AddForce(transform.up * (float)Velocity);

                StartCoroutine("ReloadGun");
            }
        }

        private IEnumerator ReloadGun()
        {
            yield return new WaitForSeconds((float)(Reload / (double)30));
            isReloaded = true;
        }
    }
}
