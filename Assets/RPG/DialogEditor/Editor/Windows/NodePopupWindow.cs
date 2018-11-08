using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using RPG.DialogEditor.Editor.Utils;

namespace RPG.DialogEditor.Editor.Windows
{
    public class NodePopupWindow : EditorWindow
    {
        public delegate void Created(NodeGraph newGraph);
        public event Created OnCreate;

        static NodePopupWindow curPopup;
        string curName = "";

        public static void InitNodePopup(Created created)
        {
            curPopup = GetWindow<NodePopupWindow>("");
            curPopup.OnCreate += created;
        }

        void OnGUI()
        {
            GUILayout.Space(20);
            GUILayout.BeginHorizontal();
            GUILayout.Space(20);

            GUILayout.BeginVertical();
            EditorGUILayout.LabelField("Create new Graph:", EditorStyles.boldLabel);
            curName = EditorGUILayout.TextField("Enter name", curName);
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Create Graph", GUILayout.Height(40)))
            {
                if (!string.IsNullOrWhiteSpace(curName))
                {
                    OnCreate?.Invoke(NodeUtils.CreateNewGraph(curName));
                    curPopup.Close();
                } else
                {
                    EditorUtility.DisplayDialog("Node Message:", "Please enter a valid name!", "OK");
                }
            }
            if (GUILayout.Button("Cancel", GUILayout.Height(40)))
            {
                curPopup.Close();
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            GUILayout.Space(20);
            GUILayout.EndHorizontal();
            GUILayout.Space(20);
        }
    }
}