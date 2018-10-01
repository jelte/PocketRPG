using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI {
    public class InteractionPanel : MonoBehaviour
    {
        private Dictionary<InteractionContext.State, GameObject> groups = new Dictionary<InteractionContext.State, GameObject>();
        
        void Start()
        {
            foreach (InteractionGroup group in GetComponentsInChildren<InteractionGroup>())
            {
                groups.Add(group.state, group.gameObject);
                group.gameObject.SetActive(false);
            }


            Character player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
            player.OnTarget += OnChangeTarget;
            InteractionContext context = player.InteractionContext;

            groups[context.Value].SetActive(true);

            context.OnChange += OnContextChange;
        }
        
        void OnContextChange(InteractionContext.State oldState, InteractionContext.State newState)
        {
            groups[oldState].SetActive(false);
            groups[newState].SetActive(true);
        }

        void OnChangeTarget(Character character, Targetable target, Targetable oldTarget)
        {
            character.InteractionContext.Value = target is IInteractable ? InteractionContext.State.INTERACTION : InteractionContext.State.COMBAT;            
        }
    }
}