using Assets.Scripts.Ship;
using UnityEngine;

public class GeneratorController : MonoBehaviour
{
    public int GenerationAmount;

    private void Start()
    {
        transform.parent.parent.GetComponent<ShipVariables>().CurrentBatteryEnergy = GenerationAmount;
    }
}