using RPG.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public class Character : MonoBehaviour, Targetable
    {
        public delegate void Action(Character character);
        public delegate void Target(Character character, Targetable target, Targetable oldTarget);
        public delegate void Speech(Character character, SpeechBubble.Type type, String value);

        public event Action OnAttack;
        public event Action OnDie;

        public event Action OnRevive;
        public event Action OnMove;

        public event Speech OnSpeech;
        public event Target OnTarget;
        
        private Dictionary<Stat.Name, Stat> stats = new Dictionary<Stat.Name, Stat>();
       
        public float movementSpeed = 2f;
        public string modelName;

        private Rigidbody rigidbody;
        private CapsuleCollider capsuleCollider;
        private Transform model;
        private InteractionContext interactionContext = new InteractionContext();
        private LocationStack location = new LocationStack();
        private Targetable target;
        
        public InteractionContext InteractionContext 
        {
            get
            {
                return interactionContext;
            }
        }

        public LocationStack Location
        {
            get
            {
                return location;
            }
        }

        public Vector3 Forward
        {
            get
            {
                return model.forward;
            }
        }

        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            if (rigidbody == null)
            {
                rigidbody = gameObject.AddComponent<Rigidbody>();
                rigidbody.freezeRotation = true;
                rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            }
            capsuleCollider = GetComponent<CapsuleCollider>();
            if (capsuleCollider == null)
            {
                capsuleCollider = gameObject.AddComponent<CapsuleCollider>();
                capsuleCollider.height = 1.6f;
                capsuleCollider.center = new Vector3(0, 0.8f, 0);
            }
            model = Instantiate(Resources.Load<GameObject>("Models/" + modelName), transform).transform;

            GetStat(Stat.Name.HEALTH).MaxValue = 100f;
            GetStat(Stat.Name.MANA).MaxValue = 100f;
            GetStat(Stat.Name.STAMINA).MaxValue = 100f;
            GetStat(Stat.Name.RAGE).MaxValue = 10f;
            GetStat(Stat.Name.FOCUS).MaxValue = 10f;

            GetStat(Stat.Name.HEALTH).OnChange += delegate (float newValue, float oldValue, float diff, float maxValue)
            {
                if (oldValue >= 0f && newValue <= 0f) OnDie?.Invoke(this);
                else if (oldValue <= 0f && newValue >= 0f) OnRevive?.Invoke(this);
            };
        }

        public void Move(Vector3 direction)
        {
            model.Rotate(Vector3.up * Vector3.Angle(model.forward, direction), Space.Self);
            transform.position += direction * movementSpeed;
            OnMove?.Invoke(this);
        }

        public void Attack()
        {
            OnAttack?.Invoke(this);
        }

        public void Interact()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + transform.up * 1.5f, Forward, out hit, 2f))
            {
                IInteractable interactable = hit.transform.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Interact(this);
                }
            }
        }
        
        public void Think(string though)
        {
            OnSpeech?.Invoke(this, SpeechBubble.Type.TOUGHT, though);
        }

        public Stat GetStat(Stat.Name name)
        {
            Stat stat;
            if (!stats.TryGetValue(name, out stat))
            {
                stat = new Stat(0f);
                stats.Add(name, stat);
            }
            return stat;
        }

        public void ChangeTarget(Targetable target)
        {
            Targetable oldTarget = this.target;
            this.target = target;
            OnTarget?.Invoke(this, target, oldTarget);
        }
    }
}