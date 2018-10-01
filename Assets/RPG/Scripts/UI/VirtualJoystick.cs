using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace RPG.UI
{
    public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        public float sensitivity = 1.5f;
        public float deathZone = 0.1f;


        private Character character;
        private Image bgImg;
        private Image JoystickImg;
        private Vector2 inputVector;

        void Start()
        {
            character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();

            // Remove the keyboard controller if the virtual joystick is enabled.
           // Destroy(character.GetComponent<KeyboardController>());

            bgImg = GetComponent<Image>();
            JoystickImg = transform.GetChild(0).GetComponent<Image>();
        }

        void Update()
        {
            // Move the character
            character.Move(new Vector3(inputVector.x, 0, inputVector.y) * Time.deltaTime);
        }

        public virtual void OnDrag(PointerEventData eventData)
        {

            // Check if it is this component that is dragged and how far the draggin is.
            Vector2 pos;
            if (
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    bgImg.rectTransform,
                    eventData.position,
                    eventData.pressEventCamera,
                    out pos
                )
            )
            {
                // Determine the draggin distance.
                pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
                pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

                inputVector = pos * sensitivity;
                
                inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

                // Update the visual representation.
                JoystickImg.rectTransform.anchoredPosition = new Vector2(
                    inputVector.x * (bgImg.rectTransform.sizeDelta.x / 3),
                    inputVector.y * (bgImg.rectTransform.sizeDelta.y / 3)
                );

                if (inputVector.magnitude <= deathZone) inputVector = Vector2.zero;
            }
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            // Start Dragging.
            OnDrag(eventData);
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            // Reset the movement.
            inputVector = Vector3.zero;
            JoystickImg.rectTransform.anchoredPosition = Vector3.zero;
        }
    }
}