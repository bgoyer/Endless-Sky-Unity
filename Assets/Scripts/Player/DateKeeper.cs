using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class DateKeeper : MonoBehaviour
    {
        public Text SystemTextUI;
        public DateTime CurrentDate { get; set; }

        private bool running = false;
        private string[] months = new string[] { "January", "Febuary", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        void Start()
        {
            CurrentDate = new DateTime(2095, 1, 1);
            StartCoroutine("StartTimeLoop");
        }


        public IEnumerator StartTimeLoop()
        {
            running = true;
            while (running)
            {
                CurrentDate = CurrentDate.AddDays(1);
                SystemTextUI.text = $"{CurrentDate.Day} {months[CurrentDate.Month]} {CurrentDate.Year}";
                yield return new WaitForSeconds(1f);
            }
        }
        public void StopTimeLoop()
        {
            running = false;
        }
    }
}
