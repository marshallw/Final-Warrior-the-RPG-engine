using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Models
{
    public class Inventory
    {
        public  Dictionary<Item, int> _inventory { get; set; }

        public Inventory()
        {
            _inventory = new Dictionary<Item, int>();
        }

        public void AddItem(Item item, int count)
        {
            if (item != null)
            {
                if (_inventory.ContainsKey(item))
                {
                    _inventory[item] += count;
                }
                else
                {
                    _inventory.Add(item, count);
                }
            }
        }
    }
}
