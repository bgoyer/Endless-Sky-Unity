using Assets.Scripts.Ship;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider hull;
    private Slider shield;
    private GameObject background;
    private Color low;
    private Color high;
    private GameObject ship;

    public Vector3 Offset;

    // Start is called before the first frame update
    private void Start()
    {
        low = Color.red;
        high = new Color(0, 175, 0);
        ship = gameObject;
        background = this.transform.GetChild(10).GetChild(0).gameObject;
        hull = this.transform.GetChild(10).GetChild(1).GetComponent<Slider>();
        hull.maxValue = ship.GetComponent<ShipVariables>().MaxHullHp;
        shield = this.transform.GetChild(10).GetChild(2).GetComponent<Slider>();
        shield.maxValue = ship.GetComponent<ShipVariables>().MaxShieldHp;
    }

    // Update is called once per frame
    private void Update()
    {
        background.gameObject.SetActive((ship.GetComponent<ShipVariables>().HullHp < ship.GetComponent<ShipVariables>().MaxHullHp) || (ship.GetComponent<ShipVariables>().ShieldHp < ship.GetComponent<ShipVariables>().MaxShieldHp));
        background.transform.position = Camera.main.WorldToScreenPoint(ship.transform.position + (Offset - new Vector3(0, 0, 1)));

        hull.gameObject.SetActive((ship.GetComponent<ShipVariables>().HullHp < ship.GetComponent<ShipVariables>().MaxHullHp) || (ship.GetComponent<ShipVariables>().ShieldHp < ship.GetComponent<ShipVariables>().MaxShieldHp));
        hull.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, hull.normalizedValue);
        hull.value = ship.GetComponent<ShipVariables>().HullHp;
        hull.transform.position = Camera.main.WorldToScreenPoint(ship.transform.position + Offset);

        shield.gameObject.SetActive(ship.GetComponent<ShipVariables>().ShieldHp < ship.GetComponent<ShipVariables>().MaxShieldHp);
        shield.value = ship.GetComponent<ShipVariables>().ShieldHp;
        shield.transform.position = Camera.main.WorldToScreenPoint(ship.transform.position + (Offset + new Vector3(0, 0, 1)));
    }
}