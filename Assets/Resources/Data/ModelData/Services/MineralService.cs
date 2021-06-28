using Assets.Resources.Data.ModelData.Models.Inventory.Mineral;

namespace Assets.Resources.Data.ModelData.Services
{
    public class MineralService : ServiceBase<MineralModel>
    {
        public MineralService() : base("mineral")
        {
        }

        public MineralService(string lang) : base("mineral", lang)
        {
        }
    }
}