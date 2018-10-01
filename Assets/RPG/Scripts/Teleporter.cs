using UnityEngine;
using Cinemachine;

namespace RPG
{
    public class Teleporter : MonoBehaviour
    {
        public Transform target;
        private CinemachineVirtualCamera vcam;

        private void Start()
        {
            vcam = FindObjectOfType<CinemachineVirtualCamera>();
        }

        void OnTriggerEnter(Collider other)
        {
            vcam.gameObject.SetActive(false);
            other.transform.position = target.position;

        }

        void OnTriggerExit(Collider other)
        {
            vcam.gameObject.SetActive(true); ;
        }
    }
}