using RPG.DialogEditor.Data.Node;
using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RPG.DialogEditor.Data
{
    [Serializable]
    public class NodeInput
    {
        [SerializeField]
        private AbstractNode node;
        [SerializeField]
        private NodeOutput source = null;
        [SerializeField]
        private Rect rect;
        [SerializeField]
        private Vector2 anchor;

        [NonSerialized]
        private Rect absoluteRect;

        public NodeInput(AbstractNode node, int index)
        {
            this.node = node;
            rect = new Rect(new Vector2(-10f, 5f + index * 25f), Vector2.one * 20f);
            anchor = new Vector2(rect.x, rect.y + rect.height * .5f);
        }

        public NodeOutput Source
        {
            get { return source; }
            set
            {
                if (source == value) return;
                NodeOutput oldSource = source;

                source = value;
                if (source != null)
                {
                    source.Target = this;
                }
                if (oldSource != null)
                {
                    oldSource.Target = null;
                }
            }
        }
        
        public Vector2 Edge
        {
            get
            {
                return node.Position + anchor;
            }
        }
        
#if UNITY_EDITOR
        public virtual void UpdateNodeUI(Event e, Rect viewRect, GUISkin viewSkin)
        {
            absoluteRect = new Rect(node.Position + rect.position, rect.size);
            GUI.Box(absoluteRect, "I");
        }
#endif

        internal bool Contains(Vector2 mousePosition)
        {
            return absoluteRect.Contains(mousePosition);
        }
    }
}