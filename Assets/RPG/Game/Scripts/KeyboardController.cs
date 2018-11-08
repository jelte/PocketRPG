using UnityEngine;
using static RPG.InteractionContext;

namespace RPG
{
    public class KeyboardController : MonoBehaviour
    {
        Character character;

        // Start is called before the first frame update
        void Start()
        {
            character = GetComponent<Character>();
        }

        // Update is called once per frame
        void Update()
        {
            character.Move(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Time.deltaTime);

            if (Input.GetButtonDown("Attack"))
            {
                character.Attack();
            }
        }
    }
}
