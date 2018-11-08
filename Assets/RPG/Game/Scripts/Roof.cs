using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public class Roof : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.tag != "Player") return;
            foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
            {
                renderer.enabled = false;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.tag != "Player") return;
            foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
            {
                renderer.enabled = true;
            }
        }
    }
}