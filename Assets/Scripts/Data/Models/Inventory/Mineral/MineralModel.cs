using System.Collections.Generic;

namespace Assets.Scripts.Data.Models.Inventory.Mineral
{
    public class MineralModel : InventoryModelBase
    {
        public string FlotsamSprite { get; set; }
        public Minable Minable { get; set; }
    }

    public class Minable
    {
        public string Sprite { get; set; }
        public int HullHps { get; set; }
        public int Payload { get; set; }
        public Dictionary<string, int> ExplodeEffects { get; set; }
    }
}