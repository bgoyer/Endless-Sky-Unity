using UnityEngine;

public class GameController : MonoBehaviour
{
    //  private void Start()
    //   {
    //Time.timeScale = 0f;
    //   }
    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void Play()
    {
        Time.timeScale = 1f;
    }
}