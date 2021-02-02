using Assets.Scripts.Data.Models.Inventory.Mineral;

namespace Assets.Scripts.Data.Services
{
    public class MineralService: ServiceBase<MineralModel>
    {
        public MineralService() : base("mineral")
        {
        }

        public MineralService(string lang) : base("mineral", lang)
        {
        }
    }
}