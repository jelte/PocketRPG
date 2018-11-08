using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG {
    public class Weapon : MonoBehaviour
    {
        Animator animator;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
            transform.parent.GetComponentInParent<Character>().OnAttack += Swing;
        }

        // Update is called once per frame
        void Swing(Character character)
        {
            animator.SetTrigger("Swing");
        }
    }
}