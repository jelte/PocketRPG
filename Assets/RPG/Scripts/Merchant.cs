using UnityEngine;

namespace RPG
{
    class Merchant : MonoBehaviour, IInteractable
    {
        public void Interact(Character character)
        {
            Debug.Log("Call Merchant UI");
        }
    }
}
