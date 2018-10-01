using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public class Sign : MonoBehaviour, IInteractable
    {
        public string location;

        public void Interact(Character character)
        {
            character.Think(location);
        }
    }
}