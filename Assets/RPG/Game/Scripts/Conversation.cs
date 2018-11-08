using System;
using System.Collections.Generic;

namespace RPG
{
    [Serializable]
    public struct Conversation
    {
        public string Cue;
        public string Reply;
        public List<Conversation> responses;
    }
}