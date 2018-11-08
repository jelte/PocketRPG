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
    public class FloatNode : AbstractNode
    {
        public float nodeValue;

        public FloatNode() : base()
        {
            inputs = new NodeInput[] { new NodeInput(this, 0) };
            outputs = new NodeOutput[] { new NodeOutput(this, typeof(float), 0) };
        }

        public override void InitNode()
        {
            base.InitNode();
        }

#if UNITY_EDITOR
        public override void UpdateNodeUI(Event e, Rect viewRect, GUISkin viewSkin)
        {
            base.UpdateNodeUI(e, viewRect, viewSkin);

            EditorGUI.LabelField(new Rect(nodeRect.position + new Vector2(15f, 25f), new Vector2(120f, 20f)), "Value: " +nodeValue);
        }

        public override void DrawPropertyPanel()
        {
            base.DrawPropertyPanel();

            EditorGUILayout.GetControlRect();
            nodeValue = EditorGUILayout.FloatField("Value", nodeValue);
        }
#endif
    }
}
