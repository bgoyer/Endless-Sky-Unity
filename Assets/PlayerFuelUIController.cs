using Assets.Scripts.Ship;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFuelUIController : MonoBehaviour
{
    public GameObject Player;

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Slider>().value = Player.transform.GetChild(0).GetComponent<ShipVariables>().HyperdriveFuel - Player.transform.GetChild(0).GetComponent<ShipVariables>().MaxHyperdriveFuel;
    }
}
