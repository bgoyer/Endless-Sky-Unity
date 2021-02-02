using Assets.Scripts.Data.Models.Inventory.Weapon;

namespace Assets.Scripts.Data.Services
{
    public class WeaponService : ServiceBase<WeaponModel>
    {
        public WeaponService() : base("weapon")
        {
        }

        public WeaponService(string lang) : base("weapon", lang)
        {
        }
    }
}
