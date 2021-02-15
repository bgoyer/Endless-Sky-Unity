using Assets.Resources.Data.ModelData.Models.Inventory.Engines;

namespace Assets.Resources.Data.ModelData.Services
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
