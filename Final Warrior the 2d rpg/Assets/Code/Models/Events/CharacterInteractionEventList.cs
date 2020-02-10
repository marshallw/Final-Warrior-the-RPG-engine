using Assets.Code.Models.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Models.Events
{
    public class CharacterInteractionEventList : CharacterInteractionEvent, IEquatable<CharacterInteractionEventList>
    {
        public List<CharacterInteractionEvent> Interactions { get; set; }

        public bool Equals(CharacterInteractionEventList other)
        {
            throw new NotImplementedException();
        }
    }
}