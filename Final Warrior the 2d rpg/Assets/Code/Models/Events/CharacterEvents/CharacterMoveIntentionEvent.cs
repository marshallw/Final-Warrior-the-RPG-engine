using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterMoveIntentionEvent : CharacterEvent, IEquatable<CharacterMoveIntentionEvent>
{
    public Vector3 Coordinates { get; }
    public CharacterMoveIntentionEvent(Vector3 oordinates)
    {
        Coordinates = Coordinates;
    }

    public bool Equals(CharacterMoveIntentionEvent other)
    {
        return other != null &&
            Coordinates == other.Coordinates;
    }
}
