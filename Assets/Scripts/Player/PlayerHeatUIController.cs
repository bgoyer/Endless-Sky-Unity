using Assets.Scripts.Ship;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeatUIController : MonoBehaviour
{
    public GameObject Player;
    private float oldtemp;
    void FixedUpdate()
    {
        this.GetComponent<Image>().fillAmount = Mathf.Lerp(oldtemp, (float)Player.transform.GetChild(0).GetComponent<ShipVariables>().Temp / Player.transform.GetChild(0).GetComponent<ShipVariables>().OverHeatTemp, 120 * Time.deltaTime);
        oldtemp = Player.transform.GetChild(0).GetComponent<ShipVariables>().Temp / Player.transform.GetChild(0).GetComponent<ShipVariables>().OverHeatTemp;
    }
}
