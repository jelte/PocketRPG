using UnityEngine;

namespace RPG
{
    public class Location : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            Character character = other.GetComponent<Character>();
            if (character != null)
            {
                character.Location.Push(this);
            }
        }

        void OnTriggerExit(Collider other)
        {
            Character character = other.GetComponent<Character>();
            if (character != null)
            {
                character.Location.Pop();
            }
        }
    }
}