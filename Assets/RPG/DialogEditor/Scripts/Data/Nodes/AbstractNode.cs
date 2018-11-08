using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RPG.DialogEditor.Data.Node
{
    [Serializable]
    public abstract class AbstractNode : ScriptableObject
    {
        public bool isSelected = false;
        public NodeGraph parentGraph;

        public NodeInput[] inputs = new NodeInput[] { };
        public NodeOutput[] outputs = new NodeOutput[] { };

        protected Rect nodeRect;

        protected GUISkin nodeSkin;

        public AbstractNode()
        {
            nodeRect.size = new Vector2(150f, 65f);
        }

        public Vector2 Position
        {
            get { return nodeRect.position; }
            set { nodeRect.position = value;  }
        }

        public Vector2 Size
        {
            get { return nodeRect.size; }
        }

        public virtual void InitNode()
        {
            name = GetType().Name;
        }

        public virtual void UpdateNode(Event e, Rect viewRect)
        {
        }

        public NodeEdge Contains(Vector2 mousePosition)
        {
            foreach (NodeInput input in inputs)
            {
                if (input.Contains(mousePosition)) return NodeEdge.Input;
            }

            foreach (NodeOutput output in outputs)
            {
                if (output.Contains(mousePosition)) return NodeEdge.Output;
            }
            return nodeRect.Contains(mousePosition) ? NodeEdge.Node : NodeEdge.None;
        }

        public NodeInput GetInput(Vector2 mousePosition)
        {
            foreach (NodeInput input in inputs)
            {
                if (input.Contains(mousePosition)) return input;
            }

            return null;
        }

        public NodeOutput GetOutput(Vector2 mousePosition)
        {
            foreach (NodeOutput output in outputs)
            {
                if (output.Contains(mousePosition)) return output;
            }

            return null;
        }

#if UNITY_EDITOR
        public virtual void UpdateNodeUI(Event e, Rect viewRect, GUISkin viewSkin)
        {
            EditorUtility.SetDirty(this);
            
            // TODO: Implement styles
            GUI.Box(nodeRect, name /*, viewSkin.GetStyle((isSelected ? "Selected" : "Default") + " Node")*/);

            for (int i = 0; i < inputs.Length; i++) 
            {
                inputs[i].UpdateNodeUI(e, viewRect, viewSkin);
            }
            for (int i = 0; i < outputs.Length; i++)
            {
                outputs[i].UpdateNodeUI(e, viewRect, viewSkin);
            }
        }

        public virtual void DrawPropertyPanel()
        {
        }

        internal bool Intersect(Rect selection)
        {
            return selection.Overlaps(nodeRect, true);
        }

        internal void Move(Vector2 delta)
        {
            nodeRect.x += delta.x;
            nodeRect.y += delta.y;
        }

        public void Detach()
        {
            foreach (NodeOutput output in outputs)
            {
                output.Target = null;
            }
            foreach (NodeInput input in inputs)
            {
                input.Source = null;
            }
        }

        public void DrawConnections()
        {
            foreach (NodeOutput output in outputs)
            {
                output.DrawConnection();
            }
        }
#endif

    }
}