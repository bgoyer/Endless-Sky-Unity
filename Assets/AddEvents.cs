using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

public class AddEvents : MonoBehaviour
{
    public GameObject SystemLink;

    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(OnClickEvents);
    }


    private void OnClickEvents()
    {
        GameObject hud = GameObject.Find("HUD");
        Player.GetComponent<PlayerHyperdriveController>()
            .PlayerAutoPilot(SystemLink.transform);
        hud.transform.GetChild(0).gameObject.SetActive(false);
        hud.transform.GetChild(2).gameObject.SetActive(true);

    }
}
