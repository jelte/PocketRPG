using System;

namespace RPG
{
    [Serializable]
    public class Stat
    {
        public enum Name { HEALTH, MANA, STAMINA, RAGE, FOCUS };

        public delegate void Change(float oldValue, float newValue, float difference, float maxValue);
        public event Change OnChange;
        
        private float value;
        private float maxValue;

        public Stat(float value) : this(value, value)
        {

        }

        public Stat(float value, float maxValue)
        {
            this.value = value;
            this.maxValue = maxValue;
        }


        public float Value
        {
            get
            {
                return value;
            }

            set
            {
                OnChange?.Invoke(this.value, value, value - this.value, maxValue);
                this.value = value;
            }
        }

        public float MaxValue
        {
            get
            {
                return maxValue;
            }

            set
            {
                float diff = value - maxValue;
                OnChange?.Invoke(this.value, this.value + diff, diff, value);
                this.value += diff;
                this.maxValue = value;
            }
        }
    }
}
