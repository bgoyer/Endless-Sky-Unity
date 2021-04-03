using Assets.Scripts.Ship;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFuelUIController : MonoBehaviour
{
    public GameObject Player;

    // Update is called once per frame
    private void Update()
    {
        this.GetComponent<Image>().fillAmount = (float)Player.transform.GetChild(0).GetComponent<ShipVariables>().HyperdriveFuel / (float)Player.transform.GetChild(0).GetComponent<ShipVariables>().MaxHyperdriveFuel;
    }
}