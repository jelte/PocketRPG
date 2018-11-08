using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Inventory
    {
        public delegate void Action(IItem item);
        public event Action OnEquipe;
        public event Action OnUnequipe;

        public event Action OnPickUp;
        public event Action OnDrop;

        public List<IItem> inventory = new List<IItem>();
        public List<IItem> equiped = new List<IItem>();

        public bool Equipe(IItem item)
        {
            OnEquipe?.Invoke(item);
            return true;
        }

        public bool PickUp(IItem item)
        {
            inventory.Add(item);
            OnPickUp?.Invoke(item);
            return true;
        }

        public bool Drop(IItem item)
        {
            inventory.Remove(item);
            OnDrop?.Invoke(item);
            return true;
        }
    }
}
