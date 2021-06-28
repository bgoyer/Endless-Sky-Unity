﻿namespace Assets.Resources.Data.ModelData.Models.Inventory
{
    public class InventoryModelBase : ModelBase
    {
        public string Type { get; set; }
        public double Cost { get; set; }
        public string Thumbnail { get; set; }
        public double Mass { get; set; }
        public string Description { get; set; }
        public double OutfitSpace { get; set; }
    }
}