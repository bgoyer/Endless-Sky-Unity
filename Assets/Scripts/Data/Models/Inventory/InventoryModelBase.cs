namespace ES.Data.Models.Inventory
{
    public class InventoryModelBase : ModelBase
    {
        public string Category { get; set; }
        public int Cost { get; set; }
        public string Thumbnail { get; set; }
        public int Mass { get; set; }
        public bool Installable { get; set; }
        public string Description { get; set; }
    }
}