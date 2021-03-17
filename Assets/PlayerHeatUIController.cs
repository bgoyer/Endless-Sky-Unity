using Assets.Scripts.Ship;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeatUIController : MonoBehaviour
{
    public GameObject Player;
    void Update()
    {
        this.GetComponent<Image>().fillAmount = (float)Player.transform.GetChild(0).GetComponent<ShipVariables>().Temp / (float)Player.transform.GetChild(0).GetComponent<ShipVariables>().OverHeatTemp;
    }
}
