using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class SpeechBubble : MonoBehaviour
    {
        public enum Type { TOUGHT, YELL, SAY };

        private Text label;
        private Image image;

        void Start()
        {
            image = GetComponent<Image>();
            label = GetComponentInChildren<Text>();
            image.enabled = false;
            label.enabled = false;

            GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().OnSpeech += OnSpeech;
        }

        void OnSpeech(Character character, Type type, string value)
        {
            CancelInvoke("Clear");
            Debug.Log(value);
            image.enabled = true;
            label.enabled = true;
            label.text = value;
            Invoke("Clear", 2f);
        }

        public void Clear()
        {
            image.enabled = false;
            label.enabled = false;
            label.text = "";
        }
    }
}