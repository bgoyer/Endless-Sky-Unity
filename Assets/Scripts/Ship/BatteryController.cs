using Assets.Scripts.Ship;
using System.Collections;
using UnityEngine;

public class BatteryController : MonoBehaviour
{
    public int BatteryCapacity;

    private ShipVariables shipVars;
    private void Start()
    {
        shipVars = this.transform.parent.parent.GetComponent<ShipVariables>();
        shipVars.BatteryCapacity = BatteryCapacity;
    }
}