using Assets.Resources.Data.ModelData.Models.Inventory.Engines;

namespace Assets.Resources.Data.ModelData.Services
{
    public class ThrusterService : ServiceBase<ThrusterModel>
    {
/*        public EnginesService() : base("steering")
        {
        }*/

        public ThrusterService(string lang = "en") : base("thruster", lang)
        {
        }
    }
}