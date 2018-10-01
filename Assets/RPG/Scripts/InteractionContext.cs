using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG
{
    public class InteractionContext
    {
        public enum State { COMBAT, INTERACTION };

        private State state = State.COMBAT;

        public delegate void Change(State oldState, State newState);
        public event Change OnChange;

        public State Value
        {
            get { return state; }
            set {
                State oldState = state;
                state = value;
                OnChange?.Invoke(oldState, value);
            }
        }
    }
}
