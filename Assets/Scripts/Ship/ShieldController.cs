using Assets.Scripts.Ship;
using System.Collections;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public int MaxShieldHealth = 100;
    public int RegenStrength = 10;

    private GameObject ship;

    private void Start()
    {
        ship = this.transform.parent.parent.gameObject;
        ship.GetComponent<ShipVariables>().MaxShieldHp = MaxShieldHealth;
        ship.GetComponent<ShipVariables>().ShieldHp = MaxShieldHealth;
        InvokeRepeating("RegenShield", 0f, .01f);
    }

    private void RegenShield()
    {
        if (ship.GetComponent<ShipVariables>().CanControl)
        {
            if (ship.GetComponent<ShipVariables>().ShieldHp + RegenStrength < MaxShieldHealth)
            {
                ship.GetComponent<ShipVariables>().ShieldHp += RegenStrength;
            }
            else { ship.GetComponent<ShipVariables>().ShieldHp = MaxShieldHealth; }
        }
    }
}