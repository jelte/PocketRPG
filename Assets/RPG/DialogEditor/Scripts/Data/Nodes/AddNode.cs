using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.DialogEditor.Data.Node
{
    [Serializable]
    public class AddNode : AbstractNode
    {
        public AddNode() : base()
        {
            inputs = new NodeInput[] { new NodeInput(this, 0), new NodeInput(this, 1) };
            outputs = new NodeOutput[] { new NodeOutput(this, typeof(float), 0) };
        }
        
        public override void InitNode()
        {
            base.InitNode();
            name = GetType().Name;
        }
    }

}