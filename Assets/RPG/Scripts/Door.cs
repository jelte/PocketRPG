using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public class Door : MonoBehaviour, IInteractable
    {
        Animator animator;
        public bool isOpen;

        private int openHash;

        void Start()
        {
            animator = GetComponent<Animator>();
            openHash = Animator.StringToHash("Open");
            animator.SetBool(openHash, isOpen);
        }

        public void Interact(Character character)
        {
            isOpen = !isOpen;
            animator.SetBool(openHash, isOpen);
        }
    }
}