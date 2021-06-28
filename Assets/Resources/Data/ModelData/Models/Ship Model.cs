using Assets.Resources.Data.ModelData.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipModel : ModelBase
{
    public int Mass { get; set; }
    public int HyperDriveFuelSpace { get; set; }
    public int HullHP { get; set; }
    public int CargoSpace { get; set; }
    public int PassengerSpace { get; set; }
}
