using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterInteractedEvent: CharacterEvent, IEqualityComparer<CharacterInteractedEvent>
{
    public Vector3 SourceCoordinates { get; }
    public Vector3 TargetCoordinates { get; }

    public CharacterInteractedEvent(Vector3 sourceCoordinates, Vector3 targetCoordinates)
    {
        SourceCoordinates = sourceCoordinates;
        TargetCoordinates = targetCoordinates;
    }

    public bool Equals(CharacterInteractedEvent other)
    {
        return other != null && other.SourceCoordinates == SourceCoordinates && TargetCoordinates == other.TargetCoordinates;
    }

    public bool Equals(CharacterInteractedEvent x, CharacterInteractedEvent y)
    {
        return x != null && y != null && x.SourceCoordinates == y.SourceCoordinates && x.TargetCoordinates == y.TargetCoordinates;
    }

    public int GetHashCode(CharacterInteractedEvent obj)
    {
        throw new NotImplementedException();
    }
}
