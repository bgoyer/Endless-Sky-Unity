using System.Data;
using System.Data.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor.Compilation;

namespace TaskAtlasNamespace
{
    [InitializeOnLoad]
    [ExecuteInEditMode]
    [CustomEditor(typeof(TaskAtlasType))]
    public class Core : Editor
    {
        #region Variables
        static public int timerManualMinutes = 30;

        #region Reference
        static public TaskAtlasData data;
        #endregion

        #region Icons
        static public Texture2D
            tTrashCan, tEditPen, tSortAZ, tSortDist, tSortDate, tDate, tArrange,
            tArrowLeft, tArrowRight, tArrowUp, tArrowDown, tAdd, tRefresh, tPosition, tGallery,
            tPlus, tMinus,
            tBackgroundPanels, tBackgroundMain, tTransparent,
            tGreenP, tGreen, tTeal, tBlue, tRed, tGrey, tGreenBright, tTealBright, tBlueBright, tRedBright, tGreyBright, tGreenDark, tTealDark, tBlueDark, tRedDark, tGreyDark,
            tWhite, tBlack, tYellow,
            tIconCheck, tIconX, tIconExclaim, tIconPin, tIconTask, tIconTag,
            tAtlasEnable, tAtlasDisable, tAtlasRotLeft, tAtlasRotRight, tAtlasTop, tAtlasSide,
            tAtlasArrowIn, tAtlasArrowOut, tAtlasArrowLeft, tAtlasArrowRight,
            tAtlasEditPen, tAtlasGO, tAtlasSettings,
            tTaskSticky,
            tGradGrey2Clear, tGradClear2Grey,
            tIcon2D, tIcon3D, tIconISO;

        static public Color32
            cBackgroundPanels = new Color32(128, 0, 0, 255),
                        cGreenP = new Color32(0xAB, 0xC8, 0x37, 255),
            cGreen = new Color32(0xAB, 0xC8, 0x37, 64),
            cTeal = new Color32(0x37, 0xC8, 0x9D, 64),
            cBlue = new Color32(0x54, 0x37, 0xC8, 64),
            cRed = new Color32(0xC8, 0x37, 0x62, 64),
            cGrey = new Color32(0xFF, 0xFF, 0xFF, 64);
        #endregion

        #region HelperCams
        static public RenderTexture rtSS, rtStickyText;
        static public GameObject TaskAtlasRoot, StickyTextCam, LandmarkCamera, StickyTextTransform, GizmoHelper;

        static public TaskAtlasGizmos gizmos;
        #endregion
        #endregion

        #region Editor
        static Core()
        {
            EditorSceneManager.sceneSaved += SceneSaved;
            EditorSceneManager.newSceneCreated += NewSceneCreated;
            EditorSceneManager.sceneOpened += SceneOpened;
        }

        private static void OnCompile(object obj)
        {
            Core.RemoveHelperCams();
            Core.SaveData();
        }

        static public int CheckSceneExists()
        {
            for (int i = 0; i < data.scene.Count; i++)
            {
                if (data.scene[i].name == SceneManager.GetActiveScene().path)
                {
                    return i;
                }
            }
            return -1;
        }

        static int CheckSceneExists(string name)
        {
            for (int i = 0; i < data.scene.Count; i++)
            {
                if (data.scene[i].name == name)
                {
                    return i;
                }
            }
            return -1;
        }

        static void SceneSaved(Scene scene)
        {
            if (CheckSceneExists() < 0 && CheckSceneExists(data.scene[data.sceneIndex].name) > -1)
            {
                List<TaskAtlasData.Scene.Landmark> newLandmarks = new List<TaskAtlasData.Scene.Landmark>();
                var oldLandmarks = data.scene[CheckSceneExists(data.scene[data.sceneIndex].name)].landmarks;

                for (int i = 0; i < data.scene[data.sceneIndexPrev].landmarks.Count; i++)
                {
                    newLandmarks.Add(new TaskAtlasData.Scene.Landmark());
                    newLandmarks[i].bScreenshot = oldLandmarks[i].bScreenshot;
                    newLandmarks[i].bTotalTasks = oldLandmarks[i].bTotalTasks;
                    newLandmarks[i].bOpenTasks = oldLandmarks[i].bOpenTasks;
                    newLandmarks[i].bOverdue = oldLandmarks[i].bOverdue;
                    newLandmarks[i].bUrgent = oldLandmarks[i].bUrgent;
                    newLandmarks[i].bGoToTask = oldLandmarks[i].bGoToTask;
                    newLandmarks[i].bCompleted = oldLandmarks[i].bCompleted;
                    newLandmarks[i].bMoveSticky = oldLandmarks[i].bMoveSticky;

                    newLandmarks[i].tScreenshot = oldLandmarks[i].tScreenshot;
                    newLandmarks[i].tTotalTasks = oldLandmarks[i].tTotalTasks;
                    newLandmarks[i].tOpenTasks = oldLandmarks[i].tOpenTasks;
                    newLandmarks[i].tOverdue = oldLandmarks[i].tOverdue;
                    newLandmarks[i].tUrgent = oldLandmarks[i].tUrgent;
                    newLandmarks[i].tGoToTask = oldLandmarks[i].tGoToTask;
                    newLandmarks[i].tCompleted = oldLandmarks[i].tCompleted;
                    newLandmarks[i].tMoveSticky = oldLandmarks[i].tMoveSticky;

                    newLandmarks[i].position = oldLandmarks[i].position;
                    newLandmarks[i].floorPosition = oldLandmarks[i].floorPosition;
                    newLandmarks[i].wsPosition = oldLandmarks[i].wsPosition;

                    newLandmarks[i].rotation = oldLandmarks[i].rotation;

                    newLandmarks[i].currentDistance = oldLandmarks[i].currentDistance;
                    newLandmarks[i].orthographicSize = oldLandmarks[i].orthographicSize;

                    newLandmarks[i].title = oldLandmarks[i].title;
                    newLandmarks[i].description = oldLandmarks[i].description;

                    newLandmarks[i].landmarkMoveTo = oldLandmarks[i].landmarkMoveTo;

                    newLandmarks[i].showGizmo = oldLandmarks[i].showGizmo;
                    newLandmarks[i].fadeGizmo = oldLandmarks[i].fadeGizmo;
                    newLandmarks[i].showOptions = oldLandmarks[i].showOptions;
                    newLandmarks[i].thumbnailLoaded = oldLandmarks[i].thumbnailLoaded;
                    newLandmarks[i].fadeStart = oldLandmarks[i].fadeStart;
                    newLandmarks[i].fadeEnd = oldLandmarks[i].fadeEnd;
                    newLandmarks[i].taskFadeStart = oldLandmarks[i].taskFadeStart;
                    newLandmarks[i].taskFadeEnd = oldLandmarks[i].taskFadeEnd;
                    newLandmarks[i].taskFadeMax = oldLandmarks[i].taskFadeMax;

                    newLandmarks[i].GizmoColor = oldLandmarks[i].GizmoColor;
                    newLandmarks[i].tGizmoColor = oldLandmarks[i].tGizmoColor;
                    newLandmarks[i].tGizmoColor50 = oldLandmarks[i].tGizmoColor50;
                    newLandmarks[i].tGizmoColorDark = oldLandmarks[i].tGizmoColorDark;
                    newLandmarks[i].tGizmoColorContrast = oldLandmarks[i].tGizmoColorContrast;

                    newLandmarks[i].progress = oldLandmarks[i].progress;
                    newLandmarks[i].tasksOpen = oldLandmarks[i].tasksOpen;
                    newLandmarks[i].tasksUrgent = oldLandmarks[i].tasksUrgent;
                    newLandmarks[i].tasksOverdue = oldLandmarks[i].tasksOverdue;
                    newLandmarks[i].tasksHasAlert = oldLandmarks[i].tasksHasAlert;

                    newLandmarks[i].timeStamp = oldLandmarks[i].timeStamp;

                    newLandmarks[i].tags = oldLandmarks[i].tags;

                    newLandmarks[i].activeMinutes = oldLandmarks[i].activeMinutes;
                    newLandmarks[i].idleMinutes = oldLandmarks[i].idleMinutes;
                    newLandmarks[i].sleepMinutes = oldLandmarks[i].sleepMinutes;

                    if (oldLandmarks[i].tasks != null)
                        for (int t = 0; t < oldLandmarks[i].tasks.Count; t++)
                        {
                            newLandmarks[i].tasks = new List<TaskAtlasData.Scene.Landmark.Task>();
                            newLandmarks[i].tasks.Add(oldLandmarks[i].tasks[t].Copy());
                        }
                    //Copy Gallery
                    if (oldLandmarks[i].gallery.images != null)
                    {
                        newLandmarks[i].gallery = new TaskAtlasData.Scene.Landmark.Gallery();
                        if (newLandmarks[i].gallery.images == null)
                            newLandmarks[i].gallery.images = new List<TaskAtlasData.Scene.Landmark.Gallery.Image>();
                        for (int img = 0; img < oldLandmarks[i].gallery.images.Count; img++)
                        {
                            newLandmarks[i].gallery.images.Add(new TaskAtlasData.Scene.Landmark.Gallery.Image(oldLandmarks[i].gallery.images[img].image));
                            newLandmarks[i].gallery.images[img].scaleFactor = oldLandmarks[i].gallery.images[img].scaleFactor;
                            newLandmarks[i].gallery.images[img].scaleHeight = oldLandmarks[i].gallery.images[img].scaleHeight;
                            newLandmarks[i].gallery.images[img].scrollPos = oldLandmarks[i].gallery.images[img].scrollPos;
                        }
                    }


                    newLandmarks[i].viewState = oldLandmarks[i].viewState;
                }

                data.scene.Add(new TaskAtlasData.Scene());

                data.sceneIndexPrev = data.sceneIndex;
                data.sceneIndex = data.scene.Count - 1;

                data.scene[data.sceneIndex].name = SceneManager.GetActiveScene().path;
                data.scene[data.sceneIndex].history = new List<TaskAtlasData.Scene.History>();
                data.scene[data.sceneIndex].landmarks = new List<TaskAtlasData.Scene.Landmark>();
                data.scene[data.sceneIndex].landmarks = newLandmarks;
                data.scene[data.sceneIndex].atlas = new TaskAtlasData.Scene.Atlas();
                if (data.scene[data.sceneIndexPrev].name == "")
                {
                    data.scene.RemoveAt(data.sceneIndexPrev);
                    data.sceneIndex = CheckSceneExists();
                }
            }
            else
            {
                data.sceneIndex = CheckSceneExists();
            }

            Init();

            TaskAtlasEditorWindow.scene = null;
            TaskAtlasEditorWindow.Init();
            TaskAtlasEditorWindow.isInit = true;


        }

        static void NewSceneCreated(Scene scene, NewSceneSetup setup, NewSceneMode mode)
        {
            Init();
            TaskAtlasEditorWindow.isInit = false;
            TaskAtlasSceneView.isInit = false;
        }

        static void SceneOpened(Scene scene, OpenSceneMode mode)
        {
            Init();
            TaskAtlasEditorWindow.isInit = false;
            TaskAtlasSceneView.isInit = false;
        }

        static public string taPath = "";
        static public bool isData = false, isBuild = false;
        static public void Init()
        {
            string[] allPaths;

            isData = false; isBuild = false;

            if (taPath == "" | !AssetDatabase.IsValidFolder(taPath))
            {
                allPaths = Directory.GetFiles(Application.dataPath, "*", SearchOption.AllDirectories);
                for (int i = 0; i < allPaths.Count(); i++)
                {
                    allPaths[i] = allPaths[i].Replace("\\", "/");
                    if (allPaths[i].Contains("TaskAtlasDataDemo.asset"))
                    {

                        string p = Application.dataPath;
                        p.Replace("Assets", "");
                        taPath = allPaths[i].Substring(allPaths[i].IndexOf("Assets/"));
                        taPath = taPath.Substring(0, taPath.IndexOf("/TaskAtlas/") + 11);
                    }
                    if (allPaths[i].Contains("TaskAtlasDataDemo.asset"))
                    {
                        isData = true;
                    }
                    if (allPaths[i].Contains("TaskAtlasBuildOn"))
                    {
                        isBuild = true;
                    }
                }
            }

            if (!isData & !isBuild)
            {
                Debug.Log("Setting up new Data file for Task Atlas...");
                if (!File.Exists(Application.dataPath + taPath.Replace("Assets", "") + "Data/TaskAtlasDataDemo.asset"))
                    File.Copy(Application.dataPath + taPath.Replace("Assets", "") + "Data/TaskAtlasDataDemo.asset",
                              Application.dataPath + taPath.Replace("Assets", "") + "Data/TaskAtlasData.asset");
                AssetDatabase.Refresh();
            }
            if (data == null)
            {
                data = (TaskAtlasData)AssetDatabase.LoadAssetAtPath(taPath + "Data/TaskAtlasData.asset", typeof(TaskAtlasData));
            }
            if (data == null & !isBuild)
            {
                {
                    TaskAtlasData asset = ScriptableObject.CreateInstance<TaskAtlasData>();

                    AssetDatabase.CreateAsset(asset, taPath + "Data/TaskAtlasData.asset");
                    AssetDatabase.SaveAssets();

                    data = (TaskAtlasData)AssetDatabase.LoadAssetAtPath(taPath + "Data/TaskAtlasData.asset", typeof(TaskAtlasData));
                }
            }
            if (isBuild) return;
            if (data.scene == null)
            {
                {
                    data.scene = new List<TaskAtlasData.Scene>();
                    data.scene.Add(new TaskAtlasData.Scene());
                    data.sceneIndex = 0;
                    data.scene[data.sceneIndex].name = SceneManager.GetActiveScene().path;
                    data.scene[data.sceneIndex].history = new List<TaskAtlasData.Scene.History>();
                    data.scene[data.sceneIndex].landmarks = new List<TaskAtlasData.Scene.Landmark>();
                    data.scene[data.sceneIndex].atlas = new TaskAtlasData.Scene.Atlas();
                    Init();
                    TaskAtlasEditorWindow.isInit = false;
                    TaskAtlasSceneView.isInit = false;
                }
            }
            else if (data.scene.Count == 0)
            {
                {
                    data.scene = new List<TaskAtlasData.Scene>();
                    data.scene.Add(new TaskAtlasData.Scene());
                    data.sceneIndex = 0;
                    data.scene[data.sceneIndex].name = SceneManager.GetActiveScene().path;
                    data.scene[data.sceneIndex].history = new List<TaskAtlasData.Scene.History>();
                    data.scene[data.sceneIndex].landmarks = new List<TaskAtlasData.Scene.Landmark>();
                    data.scene[data.sceneIndex].atlas = new TaskAtlasData.Scene.Atlas();
                }
            }
            else
            {
                if (CheckSceneExists() < 0)
                {
                    data.scene.Add(new TaskAtlasData.Scene());
                    data.sceneIndexPrev = data.sceneIndex;
                    data.sceneIndex = data.scene.Count - 1;
                    data.scene[data.sceneIndex].name = SceneManager.GetActiveScene().path;
                    data.scene[data.sceneIndex].history = new List<TaskAtlasData.Scene.History>();
                    data.scene[data.sceneIndex].landmarks = new List<TaskAtlasData.Scene.Landmark>();
                    data.scene[data.sceneIndex].atlas = new TaskAtlasData.Scene.Atlas();
                }
                else
                {
                    data.sceneIndex = CheckSceneExists();
                }
            }


            for (int i = 0; i < data.scene[data.sceneIndex].landmarks.Count; i++)
            {
                if (data.scene[data.sceneIndex].landmarks[i].tGizmoColor == null) data.scene[data.sceneIndex].landmarks[i].SetColor(data.scene[data.sceneIndex].landmarks[i].GizmoColor);
            }


            GUISkin skin = EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector);
            cBackgroundPanels = EditorGUIUtility.isProSkin ?
                (Color)new Color32(56, 56, 56, 255) :
                (Color)new Color32(194, 194, 194, 255);
            tBackgroundPanels = MakeTex(1, 1, cBackgroundPanels);

            var col = EditorGUIUtility.isProSkin ?
                (Color)new Color32(68, 68, 68, 255) :
                (Color)new Color32(233, 233, 233, 255);
            tBackgroundMain = MakeTex(1, 1, col);

            tTransparent = MakeTex(1, 1, Color.clear);

            tGreenP = MakeTex(1, 1, cGreenP);
            tGreen = MakeTex(1, 1, cGreen);
            tTeal = MakeTex(1, 1, cTeal);
            tBlue = MakeTex(1, 1, cBlue);
            tRed = MakeTex(1, 1, cRed);
            tGrey = MakeTex(1, 1, cGrey);

            tGreenBright = DimBackground(1.5f, cGreen);
            tTealBright = DimBackground(1.5f, cTeal);
            tBlueBright = DimBackground(1.5f, cBlue);
            tRedBright = DimBackground(1.5f, cRed);
            tGreyBright = DimBackground(1.5f, cGrey);

            tGreenDark = DimBackground(0.5f, cGreen);
            tTealDark = DimBackground(0.5f, cTeal);
            tBlueDark = DimBackground(0.5f, cBlue);
            tRedDark = DimBackground(0.5f, cRed);
            tGreyDark = DimBackground(0.5f, cGrey);


            tWhite = MakeTex(1, 1, Color.white);
            tBlack = MakeTex(1, 1, Color.black);
            tYellow = MakeTex(1, 1, new Color32(120, 120, 0, 255));

            ClearTaskAtlasCache();
            RefreshHelperCams();
        }


        static public void SaveData()
        {
            EditorUtility.SetDirty(data);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        #endregion

        #region HelperCameras
        static public void EnableLandmarkCamera()
        {
            if (LandmarkCamera)
            {
                LandmarkCamera.SetActive(true);
            }
        }
        static public void DisableLandmarkCamera()
        {
            if (LandmarkCamera)
            {
                LandmarkCamera.SetActive(false);
            }
        }

        static public void ClearTaskAtlasCache()
        {
            List<GameObject> gos = FindGameObjectsWithName("TaskAtlas");
            for (int i = 0; i < gos.Count; i++)
            {
                DestroyImmediate(gos[i]);
            }
        }

        static List<GameObject> FindGameObjectsWithName(string name)
        {
            GameObject[] gos = GameObject.FindObjectsOfType<GameObject>();
            List<GameObject> spo = new List<GameObject>();
            for (int i = 0; i < gos.Length; i++)
            {
                if (gos[i].name.Contains(name))
                {
                    spo.Add(gos[i]);
                }
            }
            return spo;
        }


        #endregion

        #region DataLoading
        static public void RefreshHelperCams()
        {
            if (!LayerExists("TaskAtlas")) CreateLayer("TaskAtlas");
            if (TaskAtlasRoot == null) TaskAtlasRoot = new GameObject("TaskAtlas");
            if (LandmarkCamera == null)
            {
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(taPath + "Prefabs/TaskAtlasLandmarkCamera.prefab");
                if (prefab == null) return;
                LandmarkCamera = Instantiate(prefab);
                LandmarkCamera.name = LandmarkCamera.name.Replace("(Clone)", "");
                LandmarkCamera.layer = LayerMask.NameToLayer("TaskAtlas");
                LandmarkCamera.transform.SetParent(TaskAtlasRoot.transform);
                LandmarkCamera.SetActive(false);
            }

            if (StickyTextCam == null)
            {
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(taPath + "Prefabs/TaskAtlasStickyTextCamera.prefab");
                if (prefab == null) return;
                StickyTextCam = Instantiate(prefab);

                StickyTextCam.name = StickyTextCam.name.Replace("(Clone)", "");
                StickyTextCam.GetComponent<Camera>().cullingMask = 1 << LayerMask.NameToLayer("TaskAtlas");
                StickyTextCam.layer = LayerMask.NameToLayer("TaskAtlas");
                StickyTextCam.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("TaskAtlas");
                StickyTextCam.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("TaskAtlas");
                StickyTextCam.transform.position = new Vector3(10000, 10000, 10000);
                StickyTextCam.transform.SetParent(TaskAtlasRoot.transform);
                StickyTextCam.SetActive(false);
            }

            if (GizmoHelper == null)
            {
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(taPath + "Prefabs/TaskAtlasGizmoHelper.prefab");
                if (prefab == null) return;
                GizmoHelper = Instantiate(prefab);
                GizmoHelper.name = GizmoHelper.name.Replace("(Clone)", "");
                GizmoHelper.layer = LayerMask.NameToLayer("TaskAtlas");
                GizmoHelper.transform.position = new Vector3(10000, 10000, 10000);
                GizmoHelper.transform.SetParent(TaskAtlasRoot.transform);
            }

            if (GizmoHelper != null && gizmos == null)
            {
                gizmos = GizmoHelper.GetComponent<TaskAtlasGizmos>();
            }

            if (rtStickyText == null) rtStickyText = (RenderTexture)AssetDatabase.LoadAssetAtPath(taPath + "UI/rtStickyText.renderTexture", typeof(RenderTexture));
            if (rtSS == null) rtSS = (RenderTexture)AssetDatabase.LoadAssetAtPath(taPath + "UI/rtSS.renderTexture", typeof(RenderTexture));
        }

        static public void RemoveHelperCams()
        {
            ClearTaskAtlasCache();
        }

        static public void RefreshIcons()
        {
            if (tGreen == null) Init();

            string s = "";
            if (EditorGUIUtility.isProSkin) s = "DarkMode";
            if (tTrashCan == null) tTrashCan = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasTrashCan" + s + ".png", typeof(Texture2D));
            if (tEditPen == null) tEditPen = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasEditPen" + s + ".png", typeof(Texture2D));
            if (tSortAZ == null) tSortAZ = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasAZ" + s + ".png", typeof(Texture2D));
            if (tSortDate == null) tSortDate = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasClock" + s + ".png", typeof(Texture2D));
            if (tSortDist == null) tSortDist = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasDist" + s + ".png", typeof(Texture2D));
            if (tDate == null) tDate = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasDate" + s + ".png", typeof(Texture2D));
            if (tArrange == null) tArrange = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasArrange" + s + ".png", typeof(Texture2D));

            if (tArrowLeft == null) tArrowLeft = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasLeftArrow" + s + ".png", typeof(Texture2D));
            if (tArrowRight == null) tArrowRight = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasRightArrow" + s + ".png", typeof(Texture2D));
            if (tArrowUp == null) tArrowUp = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasUpArrow" + s + ".png", typeof(Texture2D));
            if (tArrowDown == null) tArrowDown = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasDownArrow" + s + ".png", typeof(Texture2D));

            if (tAdd == null) tAdd = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasAdd.png", typeof(Texture2D));
            if (tRefresh == null) tRefresh = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasRefresh" + s + ".png", typeof(Texture2D));
            if (tGallery == null) tGallery = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasGallery" + ".png", typeof(Texture2D));
            if (tPosition == null) tPosition = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasPosition" + s + ".png", typeof(Texture2D));
            if (tPlus == null) tPlus = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasPlus.png", typeof(Texture2D));
            if (tMinus == null) tMinus = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasMinus.png", typeof(Texture2D));

            if (tIconCheck == null) tIconCheck = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasIconCheck.png", typeof(Texture2D));
            if (tIconX == null) tIconX = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasIconX.png", typeof(Texture2D));
            if (tIconExclaim == null) tIconExclaim = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasIconExclaim.png", typeof(Texture2D));
            if (tIconPin == null) tIconPin = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasIconPin.png", typeof(Texture2D));
            if (tIconTask == null) tIconTask = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasTask" + s + ".png", typeof(Texture2D));
            if (tIconTag == null) tIconTag = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasTag" + s + ".png", typeof(Texture2D));

            if (tAtlasEnable == null) tAtlasEnable = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasIconAtlasEnable.png", typeof(Texture2D));
            if (tAtlasDisable == null) tAtlasDisable = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasIconAtlasDisable.png", typeof(Texture2D));
            if (tAtlasRotLeft == null) tAtlasRotLeft = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasIconAtlasRotLeft.png", typeof(Texture2D));
            if (tAtlasRotRight == null) tAtlasRotRight = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasIconAtlasRotRight.png", typeof(Texture2D));
            if (tAtlasTop == null) tAtlasTop = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasIconAtlasTop.png", typeof(Texture2D));
            if (tAtlasSide == null) tAtlasSide = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasIconAtlasSide.png", typeof(Texture2D));

            if (tAtlasArrowIn == null) tAtlasArrowIn = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasIconAtlasArrowIn.png", typeof(Texture2D));
            if (tAtlasArrowOut == null) tAtlasArrowOut = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasIconAtlasArrowOut.png", typeof(Texture2D));
            if (tAtlasArrowLeft == null) tAtlasArrowLeft = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasIconAtlasArrowLeft.png", typeof(Texture2D));
            if (tAtlasArrowRight == null) tAtlasArrowRight = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasIconAtlasArrowRight.png", typeof(Texture2D));
            if (tAtlasEditPen == null) tAtlasEditPen = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasIconAtlasEditPen.png", typeof(Texture2D));
            if (tAtlasSettings == null) tAtlasSettings = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasIconSettings.png", typeof(Texture2D));
            if (tAtlasGO == null) tAtlasGO = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasIconAtlasGO.png", typeof(Texture2D));

            if (tTaskSticky == null) tTaskSticky = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskSticky.png", typeof(Texture2D));

            if (tGradGrey2Clear == null) tGradGrey2Clear = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasGradGrey2Clear.png", typeof(Texture2D));
            if (tGradClear2Grey == null) tGradClear2Grey = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasGradClear2Grey.png", typeof(Texture2D));

            if (tIcon2D == null) tIcon2D = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasIcon2D.png", typeof(Texture2D));
            if (tIcon3D == null) tIcon3D = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasIcon3D.png", typeof(Texture2D));
            if (tIconISO == null) tIconISO = (Texture2D)AssetDatabase.LoadAssetAtPath(taPath + "UI/TaskAtlasIconISO.png", typeof(Texture2D));
        }
        #endregion

        #region Utils
        public class ColorTexture
        {
            public Texture2D texture;
            public Color color;
        }

        static public List<ColorTexture> colorTextures;

        static public Texture2D MakeTex(int width, int height, Color col)
        {
            if (colorTextures == null) colorTextures = new List<ColorTexture>();
            for (int i = 0; i < colorTextures.Count; i++)
            {
                if (col == colorTextures[i].color && colorTextures[i].texture != null)
                {
                    return colorTextures[i].texture;
                }
            }
            Color[] pix = new Color[width * height];

            for (int i = 0; i < pix.Length; i++)
                pix[i] = col;

            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();

            colorTextures.Add(new ColorTexture() { texture = result, color = col });

            return result;
        }

        static Texture2D DimBackground(float pct, Color color, float alpha = .3f)
        {
            float r, g, b;
            r = (color.r * pct);
            g = (color.g * pct);
            b = (color.b * pct);

            return MakeTex(1, 1, new Color(r, g, b, alpha));
        }

        private static int maxLayers = 31;

        public void AddNewLayer(string name)
        {
            CreateLayer(name);
        }

        public void DeleteLayer(string name)
        {
            RemoveLayer(name);
        }

        public static bool CreateLayer(string layerName)
        {
            SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            SerializedProperty layersProp = tagManager.FindProperty("layers");
            if (!PropertyExists(layersProp, 0, maxLayers, layerName))
            {
                SerializedProperty sp;
                for (int i = 8, j = maxLayers; i < j; i++)
                {
                    sp = layersProp.GetArrayElementAtIndex(i);
                    if (sp.stringValue == "")
                    {
                        sp.stringValue = layerName;
                        tagManager.ApplyModifiedProperties();
                        return true;
                    }
                }
            }
            return false;
        }

        public static string NewLayer(string name)
        {
            if (name != null || name != "")
            {
                CreateLayer(name);
            }

            return name;
        }

        public static bool RemoveLayer(string layerName)
        {
            SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            SerializedProperty layersProp = tagManager.FindProperty("layers");

            if (PropertyExists(layersProp, 0, layersProp.arraySize, layerName))
            {
                SerializedProperty sp;

                for (int i = 0, j = layersProp.arraySize; i < j; i++)
                {

                    sp = layersProp.GetArrayElementAtIndex(i);

                    if (sp.stringValue == layerName)
                    {
                        sp.stringValue = "";
                        tagManager.ApplyModifiedProperties();
                        return true;
                    }
                }
            }

            return false;

        }

        public static bool LayerExists(string layerName)
        {
            SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            SerializedProperty layersProp = tagManager.FindProperty("layers");
            return PropertyExists(layersProp, 0, maxLayers, layerName);
        }

        private static bool PropertyExists(SerializedProperty property, int start, int end, string value)
        {
            for (int i = start; i < end; i++)
            {
                SerializedProperty t = property.GetArrayElementAtIndex(i);
                if (t.stringValue.Equals(value))
                {
                    return true;
                }
            }
            return false;
        }

        public static Vector3 GetSVCameraPosition()
        {
            var cameras = SceneView.GetAllSceneCameras();
            Vector3 r = Vector3.zero;
            for (int i = 0; i < cameras.Length; i++)
            {
                if (SceneView.currentDrawingSceneView != null)
                {
                    if (SceneView.currentDrawingSceneView.camera.transform.position == cameras[i].transform.position) r = cameras[i].transform.position;
                    break;
                }

                if (SceneView.lastActiveSceneView != null)
                    if (SceneView.lastActiveSceneView.camera.transform.position == cameras[i].transform.position) r = cameras[i].transform.position;

            }
            return r;
        }

        public static Quaternion GetSVCameraRotation()
        {
            var cameras = SceneView.GetAllSceneCameras();
            Quaternion r = Quaternion.identity;
            for (int i = 0; i < cameras.Length; i++)
            {
                if (SceneView.currentDrawingSceneView != null)
                {
                    if (SceneView.currentDrawingSceneView.camera.transform.position == cameras[i].transform.position) r = cameras[i].transform.rotation;
                    break;
                }

                if (SceneView.lastActiveSceneView != null)
                    if (SceneView.lastActiveSceneView.camera.transform.position == cameras[i].transform.position) r = cameras[i].transform.rotation;
            }
            return r;
        }

        public static float GetSVCOrthographicSize()
        {
            var cameras = SceneView.GetAllSceneCameras();
            float r = 0;
            for (int i = 0; i < cameras.Length; i++)
            {
                if (SceneView.currentDrawingSceneView != null)
                {
                    if (SceneView.currentDrawingSceneView.camera.transform.position == cameras[i].transform.position) r = SceneView.currentDrawingSceneView.camera.orthographicSize;
                    break;
                }

                if (SceneView.lastActiveSceneView != null)
                    if (SceneView.lastActiveSceneView.camera.transform.position == cameras[i].transform.position) r = SceneView.lastActiveSceneView.camera.orthographicSize;
            }
            return r;
        }

        public static SceneView GetSceneView()
        {
            if (SceneView.lastActiveSceneView != null) return SceneView.lastActiveSceneView;
            if (SceneView.currentDrawingSceneView != null) return SceneView.currentDrawingSceneView;
            return null;
        }
        #endregion
    }
}