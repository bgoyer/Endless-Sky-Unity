using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class DateKeeper : MonoBehaviour
    {
        public Text SystemTextUI;
        private int[] CurrentDate;

        private bool running = false;
        private string[] months = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

        void Start()
        {
            CurrentDate = new int[3] {1, 1, 2095};
            StartCoroutine("StartTimeLoop");
        }


        public IEnumerator StartTimeLoop()
        {
            running = true;
            while (running)
            {
                CurrentDate[0] += 1;
                if (CurrentDate[0] >= 30)
                {
                    CurrentDate[0] = 1;
                    CurrentDate[1] += 1;
                    if (CurrentDate[1] >= 13)
                    {
                        CurrentDate[1] = 1;
                        CurrentDate[2] += 1;
                    }
                }
                SystemTextUI.text = $"{CurrentDate[0]} {months[CurrentDate[1] - 1]} {CurrentDate[2]}";
                yield return new WaitForSeconds(15f);
            }
        }
        public void StopTimeLoop()
        {
            running = false;
        }
    }
}
