using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using RPG.DialogEditor.Data.Node;
using System;
using System.Reflection;
using System.Linq;

namespace RPG.DialogEditor.Editor.Utils
{
    public static class NodeUtils
    {
        private static string graphDatabasePath = "RPG/DialogEditor/Database";
        private static string graphExtension = "asset";

        public static NodeGraph CreateNewGraph(string name)
        {
            NodeGraph nodeGraph = ScriptableObject.CreateInstance<NodeGraph>();
            if (nodeGraph == null) return null;
            nodeGraph.name = name;
            nodeGraph.InitGraph();

            AssetDatabase.CreateAsset(nodeGraph, "Assets/" + graphDatabasePath + "/" + name + "." + graphExtension);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return nodeGraph;
        }

        public static NodeGraph LoadGraph()
        {
            string graphPath = EditorUtility.OpenFilePanel("Load Graph", Application.dataPath  + graphDatabasePath, graphExtension);
            return AssetDatabase.LoadAssetAtPath<NodeGraph>(graphPath.Substring(Application.dataPath.Length - 6));
        }

        public static AbstractNode CreateNode(NodeGraph graph, Type nodeType, Vector2 position)
        {
            if (graph == null) return null;

            AbstractNode node = (AbstractNode) ScriptableObject.CreateInstance(nodeType);
            if (node == null) return null;

            node.InitNode();
            node.Position = position;
            graph.Add(node);

            AssetDatabase.AddObjectToAsset(node, graph);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            return node;
        }

        public static void DeleteNode(AbstractNode node)
        {
            node.parentGraph.Remove(node);
            node.Detach();
            AssetDatabase.RemoveObjectFromAsset(node);
            UnityEngine.Object.DestroyImmediate(node, true);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public static void DeleteNodes(List<AbstractNode> list)
        {
            list.ForEach(delegate (AbstractNode node) {
                node.parentGraph.Remove(node);
                node.Detach();
                AssetDatabase.RemoveObjectFromAsset(node);
                UnityEngine.Object.DestroyImmediate(node, true);
            });
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public static void DrawGrid(Rect viewRect, float gridSpacing, float gridOpacity, Color gridColour)
        {
            int widthDivs = Mathf.CeilToInt(viewRect.width / gridSpacing);
            int heightDivs = Mathf.CeilToInt(viewRect.height / gridSpacing);

            Handles.BeginGUI();
            Handles.color = new Color(gridColour.r, gridColour.g, gridColour.b, gridOpacity);
            for (int x = 0; x < widthDivs; x++)
            {
                Handles.DrawLine(new Vector2(gridSpacing * x, 0f), new Vector2(gridSpacing * x, viewRect.height)); 
            }
            for (int y = 0; y < heightDivs; y++)
            {
                Handles.DrawLine(new Vector2(0f, gridSpacing * y), new Vector2(viewRect.width, gridSpacing * y));
            }
            Handles.color = Color.white;
            Handles.EndGUI();
        }

        public static List<Type> GetEnumerableOfType<T>() where T : class
        {
            List<Type> objects = new List<Type>();
            foreach (Type type in
                Assembly.GetAssembly(typeof(T)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T))))
            {
                objects.Add(type);
            }
            return objects;
        }
    }
}