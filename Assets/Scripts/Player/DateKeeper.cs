using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class DateKeeper : MonoBehaviour
    {
        public Text SystemTextUi;
        private int[] currentDate;

        private bool running = false;
        private string[] months = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

        private void Start()
        {
            currentDate = new int[3] { 1, 1, 2095 };
            StartCoroutine("StartTimeLoop");
        }

        public IEnumerator StartTimeLoop()
        {
            running = true;
            while (running)
            {
                currentDate[0] += 1;
                if (currentDate[0] >= 30)
                {
                    currentDate[0] = 1;
                    currentDate[1] += 1;
                    if (currentDate[1] >= 13)
                    {
                        currentDate[1] = 1;
                        currentDate[2] += 1;
                    }
                }
                SystemTextUi.text = $"{currentDate[0]} {months[currentDate[1] - 1]} {currentDate[2]}";
                yield return new WaitForSeconds(15f);
            }
        }

        public void StopTimeLoop()
        {
            running = false;
        }
    }
}