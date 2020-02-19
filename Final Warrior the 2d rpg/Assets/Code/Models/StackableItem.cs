using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Models
{
    public class StackableItem: Item
    {
        public int Size { get; set; }
        public int MaxSize { get; set; }
    }
}
