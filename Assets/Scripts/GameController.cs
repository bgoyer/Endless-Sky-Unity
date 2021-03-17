using UnityEngine;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        private void Awake()
        {
            Time.timeScale = 0f;
        }

        public void Pause()
        {
            Time.timeScale = 0f;
        }

        public void Play()
        {
            Time.timeScale = 1f;
        }
    }
}