using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.DialogEditor
{
    public enum NodeType {
        Float,
        Add,
        ComponentProperty
    }

    public enum NodeEdge
    {
        None,
        Node,
        Input,
        Output
    }
}