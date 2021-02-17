using TMPro;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    public class CreateSystem : EditorWindow
    {
        private string[] planetNames = new string[] {"Mignotune", "Zistrathea", "Pizeron", "Echore", "Veutune", "Votov", "Lueyama", "Zeahines", "Creshan 0T3", "Niri Y159", "Songiobos", "Ulleter", "Lesonoe", "Ennore", "Cotera", "Rougawa", "Suonov", "Ligenov", "Done 3C", "Brorth WD8H", "Hinreanus", "Olmeicarro", "Pocciotera", "Vungeabos", "Ribiaria", "Yabbyria", "Cinzippe", "Kenkides", "Canrerth", "Taimia", "Punus", "Nunia", "Hohiri", "Lakunides", "Dritalia", "Neiwei", "Phosie NAL3", "Grippe 4A6", "Coria Q4", "Strara BUCT", "Groth MB9", "Drurn X498", "Cholla L5L", "Lorix 0RG"}; 
        private bool refExists = false;
        private bool live;
        private Vector2 mapCoord = Vector2.zero;
        private GameObject mapGuide;
        private GameObject systemGameObject;
        private GameObject systemUI;
        private GameObject referenceUI;
        private GameObject newSystem;
        private string systemName;
        private GameObject map;
        private GameObject systemsHolder;

        [MenuItem("EndlessSky/CreateSystem")]
        public static void ShowWindow()
        {
            GetWindow<CreateSystem>("System Creator");
        }

        private void OnGUI()
        {
            GUILayout.BeginHorizontal();
            systemName = EditorGUILayout.TextField("System Name", systemName);
            if (GUILayout.Button("Random"))
            {
                systemName = planetNames[Random.Range(0, planetNames.Length)];
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();

            mapCoord.x = EditorGUILayout.FloatField("Map: X", mapCoord.x);
            mapCoord.y = EditorGUILayout.FloatField("Map: Y", mapCoord.y);

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("Toggle Live View"))
            {
                mapGuide = (GameObject)EditorGUIUtility.Load("System.prefab");
                live = !live;
            }
            if (GUILayout.Button("Create System"))
            {
                Debug.Log(systemName);
                if (systemUI == null)
                {
                    systemUI = mapGuide;
                    systemUI.transform.name = systemName;
                }

                if (map.transform.GetChild(0).Find(systemName) != null) return;

                systemUI = Instantiate(mapGuide, mapCoord, map.transform.rotation, map.transform.GetChild(0));

                systemUI.transform.name = systemName;
                systemUI.transform.localPosition = mapCoord;
                systemUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = systemName;

                systemGameObject = (GameObject)EditorGUIUtility.Load("SystemGO.prefab");
                newSystem = Instantiate(systemGameObject, new Vector2(mapCoord.x * 15, mapCoord.y * 15), systemGameObject.transform.rotation, systemsHolder.transform);
                newSystem.name = systemName;
            }

            if (GUILayout.Button("Delete System"))
            {
                //if (map.transform.GetChild(0).Find(systemUI.transform.name) != null)
                //{
                ConfirmDelete window = ScriptableObject.CreateInstance<ConfirmDelete>();
                window.position = new Rect(Screen.width / 2, Screen.height / 2, 250, 150);
                GetWindow<ConfirmDelete>("Confirm Delete");

                // }
            }
            GUILayout.EndHorizontal();
        }

        private void Awake()
        {
            map = GameObject.Find("HUD/Map/Galaxy");
            systemsHolder = GameObject.Find("Systems/ToBeSorted");
        }

        private void Update()
        {
            if (live)
            {
                if (!refExists)
                {
                    refExists = true;
                    referenceUI = Instantiate(mapGuide, mapCoord, map.transform.rotation, map.transform);
                }

                referenceUI.transform.localPosition = mapCoord;
                referenceUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = systemName + "(Preview)";
            }
            else
            {
                if (refExists)
                {
                    refExists = false;
                    DestroyImmediate(referenceUI, true);
                }
            }
        }
    }

    public class ConfirmDelete : EditorWindow
    {
        private string systemName;
        private GameObject map;
        private GameObject systemsHolder;

        private void Awake()
        {
            map = GameObject.Find("HUD/Map/Galaxy/Systems");
            systemsHolder = GameObject.Find("Systems/ToBeSorted");
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Please enter the system you would like to delete!", EditorStyles.wordWrappedLabel);
            GUILayout.Space(70);
            systemName = EditorGUILayout.TextField("System Name", systemName);
            if (GUILayout.Button("Agree!"))
            {
                systemName = EditorGUILayout.TextField("System Name", systemName);
                if (map.transform.Find(systemName))
                {
                    DestroyImmediate(map.transform.Find(systemName).gameObject, true);
                    DestroyImmediate(systemsHolder.transform.Find(systemName).gameObject, true);
                }
                this.Close();
            }
        }
    }
}