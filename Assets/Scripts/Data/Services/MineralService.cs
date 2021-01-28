using ES.Data.Models.Inventory;

namespace ES.Data.Services
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