using System;
using System.Collections.Generic;

namespace RPG.Data
{
    [Serializable]
    public class PlayerProfile
    {
        private List<Character> characters = new List<Character>();

        public Character[] Characters
        {
            get { return characters.ToArray(); }
        }
    }
}
