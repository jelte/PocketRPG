using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class LocationIndicator : MonoBehaviour
    {
        Text label;
        // Start is called before the first frame update
        void Start()
        {
            label = GetComponentInChildren<Text>();
            GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().Location.OnChanged += OnChanged;
        }
        
        void OnChanged(Location location)
        {
            label.text = location.gameObject.name;
        }
    }
}