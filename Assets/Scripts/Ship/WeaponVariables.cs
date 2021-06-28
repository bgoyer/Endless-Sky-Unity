using UnityEngine;

namespace Assets.Scripts.Ship
{
    public class WeaponVariables : MonoBehaviour
    {
        public string Sprite;
        public string Sound;
        public string ProjectileSprite;

        public string HitEffect;
        public double Inaccuracy;

        public double Velocity;
        public double Lifetime;
        public double Reload;
        public double FiringEnergy;
        public double FiringHeat;
        public double ShieldDamage;
        public double HullDamage;
        public bool IsReloaded;
        public GameObject BulletHolder;
        public GameObject Ammo;

        private void Start()
        {
            IsReloaded = true;
            BulletHolder = GameObject.Find("/GarbageHolder");
        }
    }
}