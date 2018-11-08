using RPG.DialogEditor.Data.Node;
using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RPG.DialogEditor.Data
{
    [Serializable]
    public class NodeOutput
    {
        [SerializeField]
        private AbstractNode node;
        [SerializeField]
        private Type type;
        [SerializeField]
        private bool isConnected = false;
        [SerializeField]
        private NodeInput target = null;
        [SerializeField]
        private Rect rect;
        [SerializeField]
        private Vector2 anchor;
        [NonSerialized]
        private Rect absoluteRect;

        public NodeOutput(AbstractNode node, Type type, int index)
        {
            this.node = node;
            this.type = type;
            rect = new Rect(new Vector2(node.Size.x - 10f, 5f + index * 25f), Vector2.one * 20f);
            anchor = new Vector2(rect.x + rect.width, rect.y + rect.height * .5f);
        }
        
        public NodeInput Target
        {
            get { return target; }
            set
            {
                if (target == value) return;

                NodeInput oldTarget = target;
                target = value;
                isConnected = target != null;

                if (target != null) {
                    target.Source = this;
                }
                if (oldTarget != null)
                {
                    oldTarget.Source = null;
                }
            }
        }

        public Vector2 Edge
        {
            get {
                return node.Position + anchor;
            }
        }

#if UNITY_EDITOR
        public virtual void UpdateNodeUI(Event e, Rect viewRect, GUISkin viewSkin)
        {
            absoluteRect = new Rect(node.Position + rect.position, rect.size);
            GUI.Box(absoluteRect, "O");
        }

        public void DrawConnection()
        {
            if (!isConnected) return;
            
            Handles.BeginGUI();
            Handles.DrawBezier(
                Edge, 
                target.Edge, 
                Edge + Vector2.right * 50,
                target.Edge + Vector2.left * 50, 
                Color.white,
                null, 
                4f
            );
            Handles.EndGUI();
        }

        internal bool Contains(Vector2 mousePosition)
        {
            return absoluteRect.Contains(mousePosition);
        }
#endif
    }
}