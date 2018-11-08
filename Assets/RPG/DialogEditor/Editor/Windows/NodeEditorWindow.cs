using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using RPG.DialogEditor.Editor.Views;

namespace RPG.DialogEditor.Editor.Windows
{
    public class NodeEditorWindow : EditorWindow
    {
        public static NodeEditorWindow curWindow;

        public float viewPercentage = .75f;

        private NodeGraph curGraph = null;

        private NodeWorkView nodeWorkView;
        

        public static void InitEditorWindow()
        {
            if (curWindow == null) curWindow = GetWindow<NodeEditorWindow>("Dialog Node Editor");
            
        }
        
        public NodeEditorWindow()
        {
            nodeWorkView = new NodeWorkView();
            nodeWorkView.OnGraphChange += delegate (NodeGraph graph) {
                curGraph = graph;
                Selection.activeObject = curGraph;
                if (curGraph == null) return;
                curGraph.InitGraph();
            };
        }
        
        void OnEnable()
        {
            Selection.activeObject = curGraph;
        }

        void OnDestroy()
        {
            
        }

        void Update()
        {
            
        }

        void OnGUI()
        {
            Event e = Event.current;
            ProcessEvents(e);

            nodeWorkView.Update(position, new Rect(Vector2.zero, Vector2.one), e, curGraph);

            Repaint();
        }
        
        void ProcessEvents(Event e)
        {
        }
    }
}