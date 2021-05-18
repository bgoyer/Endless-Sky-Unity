using Assets.Scripts.Ship;
using UnityEngine;

public class CoolingController : MonoBehaviour
{
    public int CoolAmount;
    private void Start()
    {
        transform.parent.parent.GetComponent<ShipVariables>().OverHeatTemp = CoolAmount;
    }
}