using Assets.Scripts.Ship;
using UnityEngine;

public class GeneratorController : MonoBehaviour
{
    public int GenerationAmount;
    private void Start()
    {
        InvokeRepeating("Generate", 0f, .01f);
    }

    private void Generate()
    {
        if (this.transform.parent.parent.GetComponent<ShipVariables>().HullHp > 0)
        {
            if ((this.transform.parent.parent.GetComponent<ShipVariables>().CurrentBatteryEnergy += GenerationAmount) < this.transform.parent.parent.GetComponent<ShipVariables>().BatteryCapacity)
            {
                this.transform.parent.parent.GetComponent<ShipVariables>().CurrentBatteryEnergy += GenerationAmount;
            }
            else
            {
                this.transform.parent.parent.GetComponent<ShipVariables>().CurrentBatteryEnergy = this.transform.parent.parent.GetComponent<ShipVariables>().BatteryCapacity;
            }
        }
    }
}