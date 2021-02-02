using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Data.Models.Inventory.Weapon
{
    public class WeaponBase : InventoryModelBase
    { 
        public double WeaponCapacity { get; set; }
        public double GunPorts { get; set; }
        public string Sprite { get; set; }
        public string Sound { get; set; }
        public string HitEffect { get; set; }
        public double Inaccuracy { get; set; }
        public double Velocity { get; set; }
        public double Lifetime { get; set; }
        public double Reload { get; set; }
        public double FiringEnergy { get; set; }
        public double FiringHeat { get; set; }
        public double ShieldDamage { get; set; }
        public double HullDamage { get; set; }
    }
}
