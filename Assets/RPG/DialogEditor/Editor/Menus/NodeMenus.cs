using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using RPG.DialogEditor.Editor.Windows;

namespace RPG.DialogEditor.Editor.Menus
{
    public class NodeMenus
    {
        [MenuItem("Dialog Editor/Launch Editor")]
        public static void InitDialogEditor()
        {
            NodeEditorWindow.InitEditorWindow();
        }
    }
}