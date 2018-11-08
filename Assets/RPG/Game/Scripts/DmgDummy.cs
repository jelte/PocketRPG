using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG {
    public class DmgDummy : MonoBehaviour
    {
        public float amount = 5f;

        void OnTriggerEnter(Collider other)
        {
            other.GetComponent<Character>().GetStat(Stat.Name.HEALTH).Value -= amount;
        }
    }
}
