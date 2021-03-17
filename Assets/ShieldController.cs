using Assets.Scripts.Ship;
using System.Collections;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public int MaxShieldHealth = 100;
    public int RegenStrength = 10;
    public int RegenDelay = 1;

    private GameObject ship;

    private void Start()
    {
        ship = this.transform.parent.parent.gameObject;
        ship.GetComponent<ShipVariables>().MaxShieldHp = MaxShieldHealth;
        ship.GetComponent<ShipVariables>().ShieldHp = MaxShieldHealth;
        StartCoroutine("RegenStart");
    }

    private IEnumerator RegenStart()
    {
        while (true)
        {
            if (ship.GetComponent<ShipVariables>().CanControl)
            {
                if (ship.GetComponent<ShipVariables>().ShieldHp + RegenStrength < MaxShieldHealth)
                {
                    ship.GetComponent<ShipVariables>().ShieldHp += RegenStrength;
                }
                else { ship.GetComponent<ShipVariables>().ShieldHp = MaxShieldHealth; }
            }
            yield return new WaitForSeconds(RegenDelay);
        }
    }
}