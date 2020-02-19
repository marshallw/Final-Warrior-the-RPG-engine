using Assets.Code.Models.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace Assets.Code.Models
{
    public class CharacterInteractionGiveItem : CharacterInteraction
    {
        [Inject]
        public Player player { get; set; }

        public Item item;
        public int count;
        public override void Interact()
        {
            player.Inventory.AddItem(item, count);
            EndInteraction();
        }
    }
}
