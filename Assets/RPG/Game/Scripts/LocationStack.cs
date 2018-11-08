using System.Collections.Generic;

namespace RPG
{
    public class LocationStack
    {
        private Stack<Location> stack = new Stack<Location>();

        public delegate void Changed(Location location);
        public event Changed OnChanged;
       
        public void Push(Location location)
        {
            stack.Push(location);
            OnChanged?.Invoke(location);
        }

        public Location Peek()
        {
            return stack.Peek();
        }

        public void Pop()
        {
            stack.Pop();
            if (stack.Count > 0)
            {
                OnChanged?.Invoke(stack.Peek());
            }
        }
    }
}
