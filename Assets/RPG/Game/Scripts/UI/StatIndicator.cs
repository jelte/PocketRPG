using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class StatIndicator : MonoBehaviour
    {
        public enum Subject { PLAYER, TARGET };
        public Stat.Name stat;
        public Subject subject;
        private Image image;
        private Text values;

        void Start()
        {
            Character playerCharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
            if (subject == Subject.PLAYER)
            {
                OnTarget(playerCharacter, playerCharacter, null);
            } else if (subject == Subject.TARGET)
            {
                playerCharacter.OnTarget += OnTarget;
            }
            image = GetComponent<Image>();
            values = GetComponentInChildren<Text>();
        }

        void OnUpdate(float oldValue, float newValue, float difference, float maxValue)
        {
            image.fillAmount = newValue / maxValue;
            values.text = newValue + " / " + maxValue;
        }

        void OnTarget(Character character, Targetable target, Targetable oldTarget)
        {
            if (oldTarget != null && oldTarget is Character)
            {
                ((Character) oldTarget).GetStat(stat).OnChange -= OnUpdate;
            }
            if (target is Character)
            {
                ((Character) target).GetStat(stat).OnChange += OnUpdate;
            }
        }
    }
}
