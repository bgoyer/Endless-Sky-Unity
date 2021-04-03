using Assets.Scripts.Ship;
using UnityEngine;

public class CoolingController : MonoBehaviour
{
    public int CoolAmount;
    private void Start()
    {
        InvokeRepeating("Cool", 0f, .01f);
    }

    private void Cool()
    {
        if (this.transform.parent.parent.GetComponent<ShipVariables>().HullHp > 0)
        {
            if ((this.transform.parent.parent.GetComponent<ShipVariables>().Temp -= CoolAmount) > 0)
            {
                this.transform.parent.parent.GetComponent<ShipVariables>().Temp -= CoolAmount;
            }
            else
            {
                this.transform.parent.parent.GetComponent<ShipVariables>().Temp = 0;
            }
        }
    }
}