using UnityEngine;

public class UIControler : MonoBehaviour
{
    public GameObject map;
    public GameObject player;
    public GameObject menu;
    public GameObject HideHud;

    private GameObject sceneScripts;
    private KeyMap keyMap;

    private void Start()
    {
        sceneScripts = GameObject.Find("/SceneScripts");
        keyMap = GameObject.Find("/SceneScripts").GetComponent<KeyMap>();
    }

    private void Update()
    {
        if (player.transform.GetChild(0).GetComponent<ShipVariables>().CanControl == true && player.activeInHierarchy == true && Input.GetKeyDown(keyMap.OpenMap))
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
                sceneScripts.GetComponent<GameController>().Play();
            }
            else
            {
                menu.SetActive(true);
                HideHud.SetActive(false);
                sceneScripts.GetComponent<GameController>().Pause();
            }
        }
    }
}