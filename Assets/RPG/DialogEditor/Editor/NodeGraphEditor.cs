using UnityEngine;
using UnityEditor;
using RPG.DialogEditor.Data.Node;
using System.Collections.Generic;

namespace RPG.DialogEditor.Editor
{
    [CustomEditor(typeof(NodeGraph))]
    public class NodeGraphEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            NodeGraph graph = (NodeGraph) target;

            List<AbstractNode> selection = graph.GetNodeSelection();
            EditorGUILayout.LabelField("Selection ("+selection.Count+")");

            selection.ForEach(delegate (AbstractNode node) {
                node.DrawPropertyPanel();
            });
            
        }
    }
}
