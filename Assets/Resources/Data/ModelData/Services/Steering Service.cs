using Assets.Resources.Data.ModelData.Models.Inventory.Engines;

namespace Assets.Resources.Data.ModelData.Services
{
    public class SteeringService : ServiceBase<SteeringModel>
    {
/*        public EnginesService() : base("steering")
        {
        }*/

        public SteeringService(string lang = "en") : base("steering", lang)
        {
        }
    }
}