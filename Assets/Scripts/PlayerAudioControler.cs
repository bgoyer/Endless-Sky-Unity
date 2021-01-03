using System.Collections;
using UnityEngine;

public class PlayerAudioControler : MonoBehaviour
{
    public GameObject SoundHolder;
    public AudioSource HyperDrive;
    public AudioSource WarpHum;

    public void Warp()
    {
        HyperDrive.volume = 1;
        HyperDrive.Play();
    }

    public void InWarp()
    {
        WarpHum.volume = 1;
        WarpHum.Play();
    }

    public void StopSound()
    {
        StartCoroutine("StopSounds");
    }

    private IEnumerator StopSounds()
    {
        for (int SHGB = 0; SHGB < SoundHolder.transform.childCount; SHGB++)
        {
            for (float i = 1; i > -.1; i -= .1f)
            {
                SoundHolder.transform.GetChild(SHGB).GetComponent<AudioSource>().volume = i;
                yield return new WaitForSeconds(.1f);
            }
        }
    }
}