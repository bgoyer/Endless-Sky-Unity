using UnityEngine;

namespace Assets.Scripts.Ship
{
    public class WeaponVariables : MonoBehaviour
    {
        public string Sprite {get; set;}
        public string Sound { get; set; }
        public string ProjectileSprite {get; set;}
        //public string HitEffect;
        public double Inaccuracy {get; set;}
        public double Velocity {get; set;}
        public double Lifetime {get; set;}
        public double Reload {get; set;}
        public double FiringEnergy {get; set;}
        public double FiringHeat {get; set;}
        public double ShieldDamage {get; set;}
        public double HullDamage {get; set;}
        public bool IsReloaded { get; set; }
        public GameObject BulletHolder { get; set; }
        public GameObject Ammo { get; set; }

        void Start()
        {
            IsReloaded = true;
            BulletHolder = GameObject.Find("/GarbageHolder");
        }
    }
}
