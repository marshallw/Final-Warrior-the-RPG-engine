using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Models
{
    public class Item: MonoBehaviour, IEquatable<Item>, IEqualityComparer<Item>
    {
        public string Description { get; private set; }
        public string Name { get; private set; }

        public static Item Create(string name, string description)
        {
            return new Item()
            {
                Name = name,
                Description = description
            };
        }

        public bool Equals(Item other)
        {
            return this.Name == other.Name;
        }

        public bool Equals(Item left, Item right)
        {
            return left.Equals(right);
        }

        public int GetHashCode(Item item)
        {
            return item.Name.GetHashCode();
        }
    }
}
