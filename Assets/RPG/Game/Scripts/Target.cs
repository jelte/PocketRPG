using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG
{
    public class Target : MonoBehaviour
    {
        GameObject panel;
        Text labelName;
        GameObject bars;
        GameObject portrait;

        // Start is called before the first frame update
        void Start()
        {
            Character character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
            character.OnMove += OnMove;
            character.OnTarget += OnTarget;
            panel = transform.GetChild(0).gameObject;
            panel.SetActive(false);
            bars = panel.transform.Find("Bars").gameObject;
            portrait = panel.transform.Find("Portrait").gameObject;
            labelName = panel.transform.Find("Name").GetComponentInChildren<Text>();
        }

        void OnTarget(Character character, Targetable target, Targetable oldTarget)
        {
            panel.SetActive(target != null);
            if (target != null)
            {
                labelName.text = ((MonoBehaviour)target).gameObject.name;
            }
            bars.SetActive(target is Character);
            portrait.SetActive(target is Character);
        }

        void OnMove(Character character)
        {
            RaycastHit hit;
            Targetable target = null;
            if (Physics.Raycast(character.transform.position + character.transform.up * 1.5f, character.Forward, out hit, 2f))
            {
                target = hit.transform.GetComponent<Targetable>();
            }
            character.ChangeTarget(target);
        }
    }
}
