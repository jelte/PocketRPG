using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchButton : MonoBehaviour
{
    public List<Value> values = new List<Value>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject group = new GameObject("Group");
        group.transform.SetParent(this.transform, false);
        group.transform.localScale = Vector3.one;
        RectTransform transform = group.AddComponent<RectTransform>(); 
        transform.anchorMin = new Vector2(0f, 0f);
        transform.anchorMax = new Vector2(1f, 1f);
        transform.pivot = new Vector2(.5f, .5f);

        HorizontalLayoutGroup layout = group.AddComponent<HorizontalLayoutGroup>();
        layout.childForceExpandHeight = false;
        layout.childForceExpandWidth = false;
        layout.childControlWidth = false;
        layout.childControlHeight = false;
        layout.childAlignment = TextAnchor.MiddleCenter;

        values.ForEach(delegate (Value value)
        {
            GameObject icon = new GameObject(value.name);
            icon.transform.SetParent(group.transform, false);
            icon.AddComponent<Image>().sprite = value.icon;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Serializable]
    public class Value
    {
        public string name;
        public Sprite icon;
    }
}
