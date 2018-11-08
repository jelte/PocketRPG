using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.DialogEditor.Data.Node;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RPG.DialogEditor
{
    [Serializable]
    public class NodeGraph : ScriptableObject
    {
        List<AbstractNode> nodes;

        void OnEnable()
        {
            if (nodes == null) nodes = new List<AbstractNode>();    
        }

        public void InitGraph()
        {
            nodes.ForEach(delegate (AbstractNode node) {
                node.InitNode();
            });
        }

        public void UpdateGraph()
        {
            if (nodes.Count == 0) return;

        }

#if UNITY_EDITOR
        public void UpdateGraphUI(Event e, Rect viewRect, GUISkin viewSkin)
        {
            EditorUtility.SetDirty(this);

            if (nodes.Count == 0) return;

            ProcessEvents(e, viewRect);

            // Draw all nodes
            nodes.ForEach(delegate (AbstractNode node) { node.UpdateNodeUI(e, viewRect, viewSkin); });

            // Draw all connections
            nodes.ForEach(delegate (AbstractNode node) { node.DrawConnections(); });
        }

        internal AbstractNode GetNodeByID(int nodeID)
        {
            return nodes.Find(delegate (AbstractNode node) { return node.GetInstanceID() == nodeID; });
        }
#endif

        void ProcessEvents(Event e, Rect viewRect)
        {
            if (!viewRect.Contains(e.mousePosition)) return;
        }

        public void Add(AbstractNode node)
        {
            node.parentGraph = this;
            nodes.Add(node);
        }

        public AbstractNode GetNode(Vector2 mousePosition)
        {
            return nodes.Find(delegate (AbstractNode node) { return node.Contains(mousePosition) != NodeEdge.None; });
        }

        public List<AbstractNode> GetNodeSelection()
        {
            return nodes.FindAll(delegate (AbstractNode node) { return node.isSelected; });
        }

        public void ClearNodeSelection()
        {
            nodes.ForEach(delegate (AbstractNode node) { node.isSelected = false;  });
        }

        public void DragNodes(Vector2 delta)
        {
            nodes.ForEach(delegate (AbstractNode selectedNode)
            {
                if (!selectedNode.isSelected) return;

                selectedNode.Move(delta);
            });

        }

        public List<AbstractNode> GetNodes(Rect selection)
        {
            return nodes.FindAll(delegate (AbstractNode node) { return node.Intersect(selection); });
        }

        public void Remove(AbstractNode node)
        {
            nodes.Remove(node);
        }
    }
}