using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Models.Events
{
    public class CharacterInteractionTalkEvent: CharacterInteractionEvent, IEquatable<CharacterInteractionTalkEvent>
    {
        public string Name { get; set; }
        public string DialogueText { get; set; }

        public CharacterInteractionTalkEvent(string name, string dialogueText)
        {
            Name = name;
            DialogueText = dialogueText;
        }

        public bool Equals(CharacterInteractionTalkEvent other)
        {
            return other != null && other.Name == Name && other.DialogueText == DialogueText;
        }
    }
}
