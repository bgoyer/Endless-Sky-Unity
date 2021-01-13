using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipVariables : MonoBehaviour
{
    public bool CanControl = false;
    public int HyperdriveFuel = 0;
    public int MaxHyperdriveFuel = 0;
    public bool HasHyperDrive = false;
    public bool HasThrusters = false;
    public bool HasStearing = false;
    public bool HasShields = false;
    public bool HasBattery = false;
    public bool HasGenerator = false;
    public bool HasCooling = false;

    private void Start()
    {
        UpdateShip();
    }
    public void UpdateShip()
    {
        HasHyperDrive = false;
        HasThrusters = false;
        HasStearing = false;
        HasShields = false;
        HasBattery = false;
        HasGenerator = false;
        HasCooling = false;
        int thrustercount = 0;

        for (int child = 0; child < this.transform.GetChild(1).childCount; child++)
        {
            if (this.transform.GetChild(1).GetChild(child).childCount > 0)
            {
                thrustercount += 1;
            }
        }
        if (thrustercount == 2)
        {
            HasThrusters = true;
        }

        if (this.transform.GetChild(2).childCount > 0)
        {
            HasStearing = true;
        }
        if (this.transform.GetChild(3).childCount > 0 )
        {
            HasHyperDrive = true;
        }
        if (this.transform.GetChild(4).childCount > 0)
        {
            HasBattery = true;
        }
        if (this.transform.GetChild(5).childCount > 0)
        {
            HasGenerator = true;
        }
        if (this.transform.GetChild(6).childCount > 0)
        {
            HasCooling = true;
        }
        if (this.transform.GetChild(7).childCount > 0)
        {
            HasShields = true;
        }
        if (HasThrusters && HasStearing && HasHyperDrive && HasBattery && HasGenerator && HasCooling)
        {
            CanControl = true;
        }
    }
}
