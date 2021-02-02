using Assets.Scripts.Data.Models.Inventory.Engines;

namespace Assets.Scripts.Data.Services
{
    public class EnginesService : ServiceBase<EngineModel>
    {
        public EnginesService() : base("engine")
        {
        }

        public EnginesService(string lang) : base("engine", lang)
        {
        }
    }
}
