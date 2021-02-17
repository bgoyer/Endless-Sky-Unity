using Assets.Scripts.Ship;
using UnityEngine;

namespace Assets.Scripts.Player
{
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
            foreach (Transform uiElement in this.transform)
            {
                if (uiElement.gameObject.name == "MainMenu" ||uiElement.name == "Tip")
                {
                    uiElement.gameObject.SetActive(true);
                }
                else
                {
                    uiElement.gameObject.SetActive(false);
                }
            }
        }

        private void Update()
        {
            if (player.activeInHierarchy == true && Input.GetKeyDown(keyMap.OpenMap))
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
}