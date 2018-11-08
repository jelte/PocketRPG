using System;

namespace RPG.Data
{
    [Serializable]
    public class Character
    {
        public string Name { get; internal set; }
        public Race Race { get; internal set; }
        public Gender Gender { get; internal set; }

        public Character(string name, Race race, Gender gender)
        {
            Name = name;
            Race = race;
            Gender = gender;
        }
    }
}
