using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using Assets.Scripts.Ship;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;
    private Color low;
    private Color high;
    private GameObject ship;

    public Vector3 Offset;


    // Start is called before the first frame update
    void Start()
    {
        low = Color.red;
        high = new Color(0, 175, 0);
        ship = gameObject;
        slider = this.transform.GetChild(10).GetChild(0).GetComponent<Slider>();
        slider.maxValue = ship.GetComponent<ShipVariables>().MaxHullHp;

    }

    // Update is called once per frame
    void Update()
    {
        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slider.normalizedValue);
        slider.gameObject.SetActive(ship.GetComponent<ShipVariables>().HullHp < ship.GetComponent<ShipVariables>().MaxHullHp);
        slider.value = ship.GetComponent<ShipVariables>().HullHp;
        slider.transform.position = Camera.main.WorldToScreenPoint(slider.transform.parent.parent.position + Offset);
    }
}
