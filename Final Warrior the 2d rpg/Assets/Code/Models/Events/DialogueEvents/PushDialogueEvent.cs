using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PushDialogueEvent: DialogueEvent, IEquatable<PushDialogueEvent>
{
    public string Name { get; set; }
    public string DialogueText { get; set; }

    public PushDialogueEvent(string name, string dialogueText)
    {
        Name = name;
        DialogueText = dialogueText;
    }

    public int GetHashCode(DialogueEvent obj)
    {
        throw new NotImplementedException();
    }

    public bool Equals(PushDialogueEvent other)
    {
        return other != null && other.Name == Name && other.DialogueText == DialogueText;
    }
}
