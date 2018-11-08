using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.DialogEditor.Data.Node;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace RPG.DialogEditor.Editor.Views
{
    public class NodePropertyView : AbstractView
    {

        public NodePropertyView() : base("Property View")
        {

        }

        public override void Update(Rect editorRect, Rect precentageRect, Event e, NodeGraph curGraph)
        {
            base.Update(editorRect, precentageRect, e, curGraph);

            GUI.Box(viewRect, viewTitle, viewSkin.GetStyle("View Background"));
            
            GUILayout.BeginArea(viewRect);

            List<AbstractNode> nodes = curGraph?.GetNodeSelection();
            if (nodes?.Count == 1)
            {

            } else if (nodes?.Count > 1)
            {
                // Multi selections
            }
            GUILayout.EndArea();
        }

        public override void ProcessEvents(Event e)
        {
            if (!viewRect.Contains(e.mousePosition)) return;

            base.ProcessEvents(e);
        }
    }

}