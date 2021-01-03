using UnityEngine;

public class UIControler : MonoBehaviour
{
    public GameObject map;
    public GameObject player;
    public GameObject menu;
    public GameObject HideHud;

    private void Update()
    {
        if (player.GetComponentInChildren<PlayerMovement>().canControl == true && player.activeInHierarchy == true && Input.GetKeyDown(KeyCode.M))
        {
            if (map.activeSelf == true)
            {
                map.SetActive(false);
                HideHud.SetActive(true);
            }
            else
            {
                map.SetActive(true);
                HideHud.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) && player.activeInHierarchy == true)
        {
            if (menu.activeSelf == true)
            {
                menu.SetActive(false);
                HideHud.SetActive(true);
            }
            else
            {
                menu.SetActive(true);
                HideHud.SetActive(false);
            }
        }
    }
}