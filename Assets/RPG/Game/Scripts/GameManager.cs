using UnityEngine;
using RPG.Data;

namespace RPG
{
    public class GameManager : MonoBehaviour
    {
        public const int CHARACTER_LIMIT = 5;

        PlayerProfile profile;

        void Start()
        {
            profile = new PlayerProfile();
        }
    }
}