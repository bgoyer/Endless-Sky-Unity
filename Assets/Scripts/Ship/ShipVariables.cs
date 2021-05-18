using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Ship
{
    public class ShipVariables : MonoBehaviour
    {
        public int Mass;
        public int HyperdriveFuel = 22500;
        public int MaxHyperdriveFuel = 22500;
        public int MaxHullHp = 100;
        public int HullHp;
        public int MaxShieldHp = 100;
        public int ShieldHp;
        public int BatteryCapacity;
        public int CurrentBatteryEnergy;
        public int Temp;
        public int OverHeatTemp;
        public bool CanControl = false;
        public bool HasHyperDrive = false;
        public bool HasThrusters = false;
        public bool HasStearing = false;
        public bool HasShields = false;
        public bool HasBattery = false;
        public bool HasGenerator = false;
        public bool HasCooling = false;
        public GameObject CurrentSystem;

        private bool disabled = false;

        private void Start()
        {
            HullHp = MaxHullHp;
            OverHeatTemp = Mathf.CeilToInt((float)(Mass * .8));
            InvokeRepeating("CheckVitals", 0f, .1f);
            CheckShip();
            UpdateShip();
        }
        public void UpdateShip()
        {
            CurrentSystem = this.transform.parent.parent.parent.gameObject;
            this.GetComponent<Rigidbody2D>().mass = Mass;
        }

        public void CheckShip()
        {
            HasHyperDrive = false;
            HasThrusters = false;
            HasStearing = false;
            HasShields = false;
            HasBattery = false;
            HasGenerator = false;
            HasCooling = false;
            var thrusterCount = 0;
            foreach (Transform thruster in this.transform.GetChild(1))
            {
                if (thruster.childCount > 0)
                {
                    thrusterCount += 1;
                }
            }
            if (thrusterCount == 2)
            {
                HasThrusters = true;
            }

            if (this.transform.GetChild(2).childCount > 0)
            {
                HasStearing = true;
            }
            if (this.transform.GetChild(3).childCount > 0)
            {
                HasHyperDrive = true;
            }
            if (this.transform.GetChild(4).childCount > 0)
            {
                HasBattery = true;
            }
            else BatteryCapacity = 0;
            if (this.transform.GetChild(5).childCount > 0)
            {
                HasGenerator = true;
            }
            if (this.transform.GetChild(6).childCount > 0)
            {
                HasCooling = true;
            }
            if (this.transform.GetChild(7).childCount > 0)
            {
                HasShields = true;
            }
            else ShieldHp = 0;
            if (HasThrusters && HasStearing && HasHyperDrive && HasBattery && HasGenerator && HasCooling && !disabled && CanControl != false)
            {
                CanControl = true;
            }
        }
        public void TakeOff()
        {
            CanControl = true;
        }
        private void CheckVitals()
        {
            if (CanControl == true) { 

                if (Temp >= OverHeatTemp && this.gameObject.activeInHierarchy)
                {
                    StartCoroutine("TempDisable");
                }
                if (Temp < 0)
                {
                    Temp = 0;
                }
                if (CurrentBatteryEnergy <= 0 && this.gameObject.activeInHierarchy)
                {
                   StartCoroutine("TempDisable");
                }
                if (CurrentBatteryEnergy > transform.GetChild(5).GetChild(0).GetComponent<GeneratorController>().GenerationAmount)
                {
                    CurrentBatteryEnergy = transform.GetChild(5).GetChild(0).GetComponent<GeneratorController>().GenerationAmount;
                }
        }
    }
        IEnumerator TempDisable()
        {
            disabled = true;
            CanControl = false;
            yield return new WaitForSeconds(10);
            CanControl = true;
            disabled = false;
        }
    }
}