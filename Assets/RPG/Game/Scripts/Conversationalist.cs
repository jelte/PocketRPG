using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public class Conversationalist : MonoBehaviour, IInteractable
    {
        Character character;
        public List<string> greetings = new List<string>();
        public List<Conversation> conversations = new List<Conversation>();

        public void Interact(Character character)
        {
            character.Talk(this.character, conversations);
        }

        // Start is called before the first frame update
        void Start()
        {
            character = GetComponent<Character>();
        }
    }
}
