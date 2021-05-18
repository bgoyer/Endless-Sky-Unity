using UnityEngine;

namespace Assets.Scripts.Player
{
    public class UIControler : MonoBehaviour
    {
        public GameObject Map;
        public GameObject Player;
        public GameObject Menu;
        public GameObject HideHud;

        private GameObject sceneScripts;
        private KeyMap keyMap;

        private void Start()
        {
            sceneScripts = GameObject.Find("/SceneScripts");
            keyMap = GameObject.Find("/SceneScripts").GetComponent<KeyMap>();
            foreach (Transform uiElement in this.transform)
            {
                if (uiElement.gameObject.name == "MainMenu" || uiElement.name == "Tip")
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
            if (Player.activeSelf && Input.GetKey(keyMap.OpenMap))
            {
                print("");
                if (Map.activeInHierarchy)
                {
                    Map.SetActive(false);
                    HideHud.SetActive(true);
                }
                else
                {
                    Map.SetActive(true);
                    HideHud.SetActive(false);
                }
            }
        }
    }
}