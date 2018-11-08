using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RPG.DialogEditor.Editor.Windows;
using RPG.DialogEditor.Editor.Utils;
using RPG.DialogEditor.Data.Node;
using RPG.DialogEditor.Data;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RPG.DialogEditor.Editor.Views
{
    public class NodeWorkView : AbstractView
    {
        public delegate void Changed(NodeGraph newGraph);
        public event Changed OnGraphChange;

        private bool isSelecting = false;
        private Rect selection;

        private bool isConnecting = false;
        private NodeOutput connectionSource;
        
        public NodeWorkView() : base("Node Graph")
        {
            OnLeftMouseDown += StartSelectionRectangle;
            OnLeftMouseDown += SelectSingleNode;
            OnLeftMouseDown += StartNodeConnection;
            OnLeftMouseDrag += DragNode;
            OnLeftMouseDrag += DrawSelectionRectangle;
            OnLeftMouseUp += EndSelectionRectangle;
            OnLeftMouseUp += EndNodeConnection;
            OnKeyDown += DeleteNode;
            OnRightMouseUp += GraphContextMenu;
            OnRightMouseUp += NodeContextMenu;
        }

        public override void Update(Rect editorRect, Rect precentageRect, Event e, NodeGraph curGraph)
        {
            base.Update(editorRect, precentageRect, e, curGraph);

            viewTitle = curGraph == null ? "No Graph" : curGraph.name;

            GUI.Box(viewRect, viewTitle, viewSkin.GetStyle("View Background"));
            NodeUtils.DrawGrid(editorRect, 50f, .15f, Color.white);

            GUILayout.BeginArea(viewRect);
            curGraph?.UpdateGraphUI(e, viewRect, viewSkin);

            if (isSelecting)
            {
                EditorGUI.DrawRect(selection, Color.yellow);
            }
            if (isConnecting)
            {
                Handles.BeginGUI();
                Handles.DrawBezier(
                    connectionSource.Edge, 
                    e.mousePosition,
                    connectionSource.Edge + Vector2.right * 50,
                    e.mousePosition + Vector2.left * 50,
                    Color.white, 
                    null, 
                    4f
                );
                Handles.EndGUI();
            }

            GUILayout.EndArea();
        }
        
        void StartSelectionRectangle(AbstractView source, Event e)
        {
            if (curGraph?.GetNode(e.mousePosition) != null) return;
            
            isSelecting = true;
            selection = new Rect(e.mousePosition, Vector2.zero);
        }

        void DrawSelectionRectangle(AbstractView source, Event e)
        {
            if (!isSelecting) return;

            selection.size += e.delta;
        }

        void EndSelectionRectangle(AbstractView source, Event e)
        {
            if (!isSelecting) return;

            isSelecting = false;

            List<AbstractNode> nodes = curGraph?.GetNodes(selection);

            if (!e.shift) curGraph.ClearNodeSelection();

            nodes?.ForEach(delegate (AbstractNode node) { node.isSelected = !node.isSelected; });
        }

        void DeleteNode(AbstractView source, Event e)
        {
            if (e.keyCode != KeyCode.Delete) return;

            NodeUtils.DeleteNodes(curGraph?.GetNodeSelection());
        }

        void SelectSingleNode(AbstractView view, Event e)
        {
            // already selecting
            if (isSelecting) return;

            // clear node selection unless shift is pressed
            if (!e.shift) curGraph.ClearNodeSelection();
            
            // clicking on node?
            AbstractNode node = curGraph?.GetNode(e.mousePosition);
            if (node == null || node.Contains(e.mousePosition) != NodeEdge.Node) return;

            // select node
            node.isSelected = true;
        }

        void DragNode(AbstractView view, Event e)
        {
            if (curGraph?.GetNode(e.mousePosition) == null) return;

            curGraph?.DragNodes(e.delta);
        }

        void StartNodeConnection(AbstractView source, Event e)
        {
            if (isSelecting) return;

            // clicking on node input?
            AbstractNode node = curGraph?.GetNode(e.mousePosition);
            connectionSource = node?.GetOutput(e.mousePosition);
            if (connectionSource == null) return;

            isConnecting = true;
        }
        
        void EndNodeConnection(AbstractView source, Event e)
        {
            if (!isConnecting) return;
            isConnecting = false;
            
            // hovering over node output?
            AbstractNode node = curGraph?.GetNode(e.mousePosition);

            connectionSource.Target = node?.GetInput(e.mousePosition);
        }

        void GraphContextMenu(AbstractView view, Event e)
        {
            AbstractNode curNode = curGraph?.GetNode(e.mousePosition);
            if (curNode != null) return;

            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Create Graph"), false, delegate () {
                NodePopupWindow.InitNodePopup(delegate (NodeGraph graph) { OnGraphChange?.Invoke(graph); });
            });
            menu.AddItem(new GUIContent("Load Graph"), false, delegate () { OnGraphChange?.Invoke(NodeUtils.LoadGraph()); });

            if (curGraph != null)
            {
                menu.AddSeparator("");
                menu.AddItem(new GUIContent("Unload Graph"), false, delegate () { OnGraphChange?.Invoke(null); });
                menu.AddSeparator("");
                NodeUtils.GetEnumerableOfType<AbstractNode>().ForEach(delegate (Type t) {
                    menu.AddItem(new GUIContent(t.Name), false, delegate () { NodeUtils.CreateNode(curGraph, t, e.mousePosition); });
                });
            }

            menu.ShowAsContext();
            e.Use();
        }

        void NodeContextMenu(AbstractView view, Event e)
        {
            AbstractNode curNode = curGraph?.GetNode(e.mousePosition);
            if (curNode == null) return;

            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Delete"), false, delegate () {
                NodeUtils.DeleteNode(curNode);
            });
            
            menu.ShowAsContext();
            e.Use();
        }
    }
}
