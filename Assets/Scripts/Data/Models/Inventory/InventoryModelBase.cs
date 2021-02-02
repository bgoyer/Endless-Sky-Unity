namespace Assets.Scripts.Data.Models.Inventory
{
    public class InventoryModelBase : ModelBase
    {
        public string Category { get; set; }
        public double Cost { get; set; }
        public string Thumbnail { get; set; }
        public double Mass { get; set; }
        public bool Installable { get; set; }
        public string Description { get; set; }
        public double OutfitSpace { get; set; }
    }
}