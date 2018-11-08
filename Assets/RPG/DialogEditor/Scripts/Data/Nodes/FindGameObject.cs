using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.DialogEditor.Data.Node
{
    [Serializable]
    public class FindGameObject : AbstractNode
    {
        [SerializeField]
        private string path;

        public FindGameObject() : base()
        {
            outputs = new NodeOutput[] { new NodeOutput(this, typeof(GameObject), 0) };
        }

        public override void InitNode()
        {
            base.InitNode();
            name = GetType().Name;
        }
    }
}