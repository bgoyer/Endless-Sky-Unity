using UnityEngine;

namespace Assets.Scripts.Ship
{
    public class ThrusterVariables : MonoBehaviour
    {
        public double ThrustEnergy;
        public int MaxSpeed;

        void Start()
        {
            if (transform.GetChild(0).childCount > 0 && transform.GetChild(1).childCount > 0)
            {
                ThrustEnergy = 500;
                MaxSpeed = 15;
            }
        }
    }
}
