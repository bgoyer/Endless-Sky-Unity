using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHyperdriveController : MonoBehaviour
{
    
    private GameObject player;
    private GameObject ship;

    private void Start()
    {
        player = GameObject.Find("/Player");
    }
    
    public void PlayerAutoPilot(Transform target)
    {
        if (player.transform.childCount > 0)
        {
            ship = player.transform.GetChild(0).gameObject;
            if (ship.GetComponent<ShipVariables>().HasHyperDrive == true)
            {
                ship.transform.GetChild(3).GetChild(0).GetComponent<HyperDriveController>().AutoPilot(target);
            }
        }
    }
}
