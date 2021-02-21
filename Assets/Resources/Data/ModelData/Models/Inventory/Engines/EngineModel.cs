namespace Assets.Resources.Data.ModelData.Models.Inventory.Engines
{
    public class EngineModel : InventoryModelBase
    {
        public int EngineCapacity { get; set; }
        public double Energy { get; set; }
        public double Fuel { get; set; }
        public double Heat { get; set; }

        public string Effect { get; set; }
        public string FlareSound { get; set; }
        public string FlareSprite { get; set; }
        public double FrameRate { get; set; }

        public string AfterburnerThrust { get; set; }
        public double AfterburnerFuel { get; set; }
        public double AfterburnerHeat { get; set; }
        public string AfterburnerEffect { get; set; }

        public double Thrust { get; set; }
        public double ThrustingEnergy { get; set; }
        public double  ThrustingHeat { get; set; }

        public int  Turn { get; set; }
        public double  TurningEnergy { get; set; }
        public double  TurningHeat { get; set; }
    }
}
