using Assets.Scripts.Data.Models.Inventory.Engines;
using ES.Data.Services;

namespace Assets.Scripts.Data.Services.Engines
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
