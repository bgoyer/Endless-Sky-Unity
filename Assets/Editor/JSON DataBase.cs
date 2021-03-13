using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    public class JSONDataBase : EditorWindow
    {
        string fileName;
        private bool createNewFile = false;
        private bool editFile = false;
        private bool deleteFile = false;
        [MenuItem("EndlessSky/JSON DataBase")]
        public static void ShowWindow()
        {
            GetWindow<JSONDataBase>("JSON Editor", true);
        }

        private void OnGUI()
        {
            if (createNewFile)
            {
                fileName = EditorGUILayout.TextField("File Name", fileName);
                UnifiedIO.Directory.Create(Path.Combine("DataBase", fileName + ".json"));
            }
            if (editFile)
            {

            }
            if (deleteFile)
            {

            }
            GUILayout.FlexibleSpace();
            GUILayout.Space(15);
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            if (GUILayout.Button("Create File"))
            {
                createNewFile = true;
                editFile = false;
                deleteFile = false;
            }
            if (GUILayout.Button("Edit File"))
            {
                createNewFile = false;
                editFile = true;
                deleteFile = false;
            }
            if (GUILayout.Button("Delete File"))
            {
                createNewFile = false;
                editFile = false;
                deleteFile = true;
            }
            GUILayout.Space(10);
            GUILayout.EndHorizontal();
            GUILayout.Space(10);
        }

        private void Awake()
        {

        }

        private void Update()
        {

        }
    }
}