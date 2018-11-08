using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RPG.DialogEditor.Editor.Views
{
    [Serializable]
    public abstract class AbstractView
    {
        public delegate void MouseEvent(AbstractView source, Event e);
        public delegate void KeyEvent(AbstractView source, Event e);

        public event MouseEvent OnLeftMouseDown;
        public event MouseEvent OnLeftMouseDrag;
        public event MouseEvent OnLeftMouseUp;
        public event MouseEvent OnRightMouseDown;
        public event MouseEvent OnRightMouseDrag;
        public event MouseEvent OnRightMouseUp;

        public event KeyEvent OnKeyDown;

        public string viewTitle;
        public Rect viewRect;

        protected GUISkin viewSkin;
        protected NodeGraph curGraph;
        
        public AbstractView(string title)
        {
            viewTitle = title;
        }

        public virtual void Update(Rect editorRect, Rect percentageRect, Event e, NodeGraph curGraph)
        {
            if (viewSkin == null) {
                LoadEditorSkin();
                return;
            }

            viewRect = new Rect(
                editorRect.x * percentageRect.x,
                editorRect.y * percentageRect.y,
                editorRect.width * percentageRect.width,
                editorRect.height * percentageRect.height
            );

            this.curGraph = curGraph;

            ProcessEvents(e);
        }

        public virtual void ProcessEvents(Event e)
        {
            if (e.isKey)
            {
                OnKeyDown(this, e);
                return;
            }

            if (!viewRect.Contains(e.mousePosition)) return;
            
            switch (e.button)
            {
                case 0:
                    switch (e.type)
                    {
                        case EventType.MouseDown:
                            OnLeftMouseDown?.Invoke(this, e);
                            break;
                        case EventType.MouseDrag:
                            OnLeftMouseDrag?.Invoke(this, e);
                            break;
                        case EventType.MouseUp:
                            OnLeftMouseUp?.Invoke(this, e);
                            break;
                    }
                    break;
                case 1:
                    switch (e.type)
                    {
                        case EventType.MouseDown:
                            OnRightMouseDown?.Invoke(this, e);
                            break;
                        case EventType.MouseDrag:
                            OnRightMouseDrag?.Invoke(this, e);
                            break;
                        case EventType.MouseUp:
                            OnRightMouseUp?.Invoke(this, e);
                            break;
                    }
                    break;
            }
        }

        protected void LoadEditorSkin()
        {
            viewSkin = Resources.Load<GUISkin>("GUISkins/Editor/DialogEditorSkin");
        }
    }
}