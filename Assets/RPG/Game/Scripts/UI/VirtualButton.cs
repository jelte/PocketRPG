using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RPG.UI
{
    public class VirtualButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        private Character character;
        private Image image;

        public Color downColor = new Color(255, 255, 255, 50);
        public Color upColor = new Color(255, 255, 255, 150);
        public void OnPointerDown(PointerEventData eventData)
        {
            character.Interact();
            // change the color of the button
            image.color = downColor;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            // Restore the color
            image.color = upColor;
        }

        void Start()
        {
            character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
            image = GetComponent<Image>();
            image.color = upColor;
        }

        
    }
}
