using Assets.Scripts.Ship;
using System.Collections;
using UnityEngine;

public class GeneratorController : MonoBehaviour
{
    public int Delay;
    public int GenerationAmount;

    private ShipVariables shipVars;

    private void Start()
    {
        shipVars = this.transform.parent.parent.GetComponent<ShipVariables>();
        StartCoroutine("Begin");
    }

    private IEnumerator Begin()
    {
        while (true)
        {
            if (shipVars.BatteryCapacity > (shipVars.CurrentBatteryEnergy += GenerationAmount))
            {
                shipVars.CurrentBatteryEnergy += GenerationAmount;
            }else shipVars.CurrentBatteryEnergy = shipVars.BatteryCapacity;

            yield return new WaitForSeconds(Delay);
        }
    }
}