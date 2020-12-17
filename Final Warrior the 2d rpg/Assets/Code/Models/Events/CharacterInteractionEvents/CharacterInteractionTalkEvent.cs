using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Models.Events
{
    public class CharacterInteractionTalkEvent: CharacterInteractionEvent, IEquatable<CharacterInteractionTalkEvent>
    {
        public DialogueItem[] DialogueItems;

        public CharacterInteractionTalkEvent(DialogueItem[] dialogueItems)
        {
            DialogueItems = dialogueItems;
        }

        public bool Equals(CharacterInteractionTalkEvent other)
        {
            throw new NotImplementedException();
        }
    }
}
