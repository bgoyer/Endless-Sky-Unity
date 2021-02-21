using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Editor
{
    public class CreateSystem : EditorWindow
    {
        private bool showSystemCreator = false;
        private readonly string[] planetNames = new string[] { "Mignotune", "Zistrathea", "Pizeron", "Echore", "Veutune", "Votov", "Lueyama", "Zeahines", "Creshan 0T3", "Niri Y159", "Songiobos", "Ulleter", "Lesonoe", "Ennore", "Cotera", "Rougawa", "Suonov", "Ligenov", "Done 3C", "Brorth WD8H", "Hinreanus", "Olmeicarro", "Pocciotera", "Vungeabos", "Ribiaria", "Yabbyria", "Cinzippe", "Kenkides", "Canrerth", "Taimia", "Punus", "Nunia", "Hohiri", "Lakunides", "Dritalia", "Neiwei", "Phosie NAL3", "Grippe 4A6", "Coria Q4", "Strara BUCT", "Groth MB9", "Drurn X498", "Cholla L5L", "Lorix 0RG" };
        private bool live;
        private Vector2 mapCoord = Vector2.zero;
        private Vector2 bodyCoord = Vector2.zero;
        private GameObject mapGuide;
        private GameObject systemGameObject;
        private GameObject systemUi;
        private GameObject referenceUi;
        private GameObject newSystem;
        private string systemName;
        private GameObject map;
        private GameObject systemsHolder;
        private GameObject player;
        private UnityAction onButtonClick;

        private bool showBodyCreator;
        private GameObject systemBeingEdited;
        private GameObject bodyReference;
        private GameObject bodyGuide;
        private string[] options;
        private int selected;
        private int selectedLast;
        private string bodyToPlace;
        private bool planetLive = false;
        private float bodyScale = 1;
        private string bodyName;

        [MenuItem("EndlessSky/CreateSystem")]
        public static void ShowWindow()
        {
            GetWindow<CreateSystem>("System Creator");
        }

        private void Awake()
        {
            selected = 0;
            options = new string[] { "Planet", "Star", "Asteroid Field" };
        }

        private void OnGUI()
        {
            if (!map)
            {
                map = GameObject.Find("HUD/Map");
            }
            if(!systemsHolder)
            {
                systemsHolder = GameObject.Find("Systems");
            }

            GUILayout.BeginScrollView(Vector2.right);
            if (showSystemCreator)
            {
                
                EditorGUILayout.LabelField("System Creator");
                GUILayout.Space(20);
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
                    mapGuide = (GameObject) EditorGUIUtility.Load("SystemUI.prefab");
                    live = !live;
                }

                if (GUILayout.Button("Create System"))
                {
                    player = GameObject.FindGameObjectWithTag("Player");
                    if (map.transform.GetChild(0).GetChild(0).Find(systemName) != null) return;
                    if (systemUi == null)
                    {
                        systemUi = Instantiate(mapGuide, mapCoord, map.transform.rotation, map.transform.GetChild(0).GetChild(0));
                        systemUi.transform.name = systemName;
                        systemUi.transform.localPosition = mapCoord;
                    }


                    systemUi.transform.name = systemName;
                    systemUi.transform.localPosition = mapCoord;
                    systemUi.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = systemName;

                    systemGameObject = (GameObject) EditorGUIUtility.Load("SystemGO.prefab");
                    newSystem = Instantiate(systemGameObject, new Vector2(mapCoord.x * 75, mapCoord.y * 75),
                        systemGameObject.transform.rotation, systemsHolder.transform.GetChild(0));
                    newSystem.name = systemName;
                    systemUi.GetComponent<AddEvents>().Player = player;
                    systemUi.GetComponent<AddEvents>().SystemLink = newSystem;
                }

                if (GUILayout.Button("Delete System"))
                {
                    ConfirmDelete window = ScriptableObject.CreateInstance<ConfirmDelete>();
                    window.position = new Rect(Screen.width / 2, Screen.height / 2, 250, 150);
                    GetWindow<ConfirmDelete>("Confirm Delete");
                }

                GUILayout.EndHorizontal();
            }

            //-------Body Creation Section-------------//
            if (showBodyCreator)
            {
                EditorGUILayout.LabelField("Body Creator");

                EditorGUI.BeginChangeCheck();

                selected = EditorGUILayout.Popup("Body To Place", selected, options);
                bodyToPlace = options[selected];
                EditorGUI.EndChangeCheck();

                bodyCoord.x = EditorGUILayout.FloatField("Map: X", bodyCoord.x);
                bodyCoord.y = EditorGUILayout.FloatField("Map: Y", bodyCoord.y);
                GUILayout.Space(10);
                GUILayout.BeginHorizontal();
                bodyScale = EditorGUILayout.FloatField("Body Scale", bodyScale);
                bodyScale = GUILayout.HorizontalSlider(bodyScale, 0, 200);
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                GUILayout.Label("Body Name");
                bodyName = GUILayout.TextField(bodyName);
                GUILayout.EndHorizontal();
                GUILayout.Space(40);

                GUILayout.BeginHorizontal("box");
                if (GUILayout.Button("Toggle Live View"))
                {
                    planetLive = !planetLive;
                }

                if (GUILayout.Button("Show Selected System"))
                {
                    if (SceneView.lastActiveSceneView != null && Selection.activeTransform != null)
                    {
                        systemBeingEdited = Selection.activeGameObject;
                        SceneView.lastActiveSceneView.AlignViewToObject(Selection.activeTransform);
                        SceneView.lastActiveSceneView.size = 400;
                    }
                }

                if (GUILayout.Button("Create Body"))
                {
                    bodyReference = Instantiate(bodyGuide, mapCoord, map.transform.rotation,
                        systemBeingEdited.transform);
                    bodyReference.transform.position = systemBeingEdited.transform.position + (Vector3) bodyCoord * 10;
                    bodyReference.transform.localScale = new Vector3(bodyScale, bodyScale);
                    bodyReference.name = bodyName;
                }

                GUILayout.EndHorizontal();
            }
            GUILayout.Space(50);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("System"))
            {
                map.SetActive(true);
                planetLive = false;
                updateFolderUI("System Creator");
                SceneView.lastActiveSceneView.AlignViewToObject(map.transform);
                SceneView.lastActiveSceneView.size = 925;
            }
            if(GUILayout.Button("Body"))
            {
                SceneView.lastActiveSceneView.AlignViewToObject(systemsHolder.transform);
                SceneView.lastActiveSceneView.size = 400;
                live = false;
                updateFolderUI("Body Creator");
                map.SetActive(false);
            }

            GUILayout.EndHorizontal();
            GUILayout.EndScrollView();
        }

        private void Update()
        {
            if (live)
            {
                if (!referenceUi)
                {
                    referenceUi = Instantiate(mapGuide, mapCoord, map.transform.rotation, map.transform);
                }

                referenceUi.transform.localPosition = mapCoord;
                referenceUi.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = systemName + "(Preview)";

            }
            else
            {
                if (referenceUi)
                {
                    DestroyImmediate(referenceUi, true);
                }
            }

            //-------------------//
            if (planetLive)
            {
                if (!bodyReference)
                {
                    selectedLast = selected;
                    bodyGuide = (GameObject) EditorGUIUtility.Load($"{options[selected]}.prefab");
                    bodyReference = Instantiate(bodyGuide, mapCoord, map.transform.rotation, systemBeingEdited.transform);
                }
                if (selected != selectedLast)
                {
                    selectedLast = selected;
                    bodyGuide = (GameObject)EditorGUIUtility.Load($"{options[selected]}.prefab");
                    DestroyImmediate(bodyReference, true);
                    bodyReference = Instantiate(bodyGuide, mapCoord, map.transform.rotation, systemBeingEdited.transform);

                }

                bodyReference.transform.position = systemBeingEdited.transform.position + (Vector3)bodyCoord * 10;
                bodyReference.transform.localScale = new Vector3(bodyScale, bodyScale);

            }
            else
            {
                if (bodyReference)
                {
                    DestroyImmediate(bodyReference, true);
                }
            }
        }

        private void updateFolderUI(string tabToStayOpen)
        {
            showSystemCreator = false;
            showBodyCreator = false;
            switch (tabToStayOpen)
            {
                case "System Creator": 
                    showSystemCreator = true;
                    break;
                case "Body Creator":
                    showBodyCreator = true;
                    break;
                default: break;
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