using Assets.Resources.Data.ModelData.Models.Inventory.Weapon;

namespace Assets.Resources.Data.ModelData.Services
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