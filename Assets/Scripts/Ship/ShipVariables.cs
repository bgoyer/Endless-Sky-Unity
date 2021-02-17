using System;
using UnityEngine;

namespace Assets.Scripts.Ship
{
    public class ShipVariables : MonoBehaviour
    {
        public bool CanControl = false;
        public int HyperdriveFuel = 0;
        public int MaxHyperdriveFuel = 0;
        public int MaxHullHP = 100;
        public int HullHP;
        public bool HasHyperDrive = false;
        public bool HasThrusters = false;
        public bool HasStearing = false;
        public bool HasShields = false;
        public bool HasBattery = false;
        public bool HasGenerator = false;
        public bool HasCooling = false;

        private void Start()
        {
            HullHP = MaxHullHP;
            UpdateShip();
        }
        public void UpdateShip()
        {
            HasHyperDrive = false;
            HasThrusters = false;
            HasStearing = false;
            HasShields = false;
            HasBattery = false;
            HasGenerator = false;
            HasCooling = false;
            var thrusterCount = 0; 
            for (var child = 0; child < this.transform.GetChild(1).childCount; child++)
            {
                if (this.transform.GetChild(1).GetChild(child).childCount > 0)
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
            if (this.transform.GetChild(3).childCount > 0 )
            {
                HasHyperDrive = true;
            }
            if (this.transform.GetChild(4).childCount > 0)
            {
                HasBattery = true;
            }
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
            if (HasThrusters && HasStearing && HasHyperDrive && HasBattery && HasGenerator && HasCooling)
            {
                CanControl = true;
            }
        }
    }
}
