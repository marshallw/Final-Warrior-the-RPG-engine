using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStartEvent : DialogueEvent, IEquatable<DialogueStartEvent>
{
    // Start is called before the first frame update
    public DialogueStartEvent()
    {
    }

    public int GetHashCode(DialogueEvent obj)
    {
        throw new NotImplementedException();
    }

    public bool Equals(DialogueStartEvent other)
    {
        return other != null && other == this;
    }
}
