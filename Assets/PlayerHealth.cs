using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Ship;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public GameObject Player;

    private GameObject ship;
    private int hullHealth;
    private int shieldHealth;
    private int maxHullHealth;
    private int maxShieldHealth;

    private ShipVariables shipVar;
    // Start is called before the first frame update
    void Start()
    {
        ship = Player.transform.GetChild(0).gameObject;
        shipVar = ship.GetComponent<ShipVariables>();
    }

    // Update is called once per frame
    void Update()
    {
        hullHealth = shipVar.HullHp;
        maxHullHealth = shipVar.MaxHullHp;
        shieldHealth = shipVar.ShieldHp;
        maxShieldHealth = shipVar.MaxShieldHp;

        if (this.name == "Shield Health")
        {
            this.GetComponent<Image>().fillAmount = (float)shieldHealth / (float)maxShieldHealth;
        }
        if (this.name == "Hull Health")
        {
            this.GetComponent<Image>().fillAmount = (float)hullHealth / (float)maxHullHealth;
        }

    }
}
