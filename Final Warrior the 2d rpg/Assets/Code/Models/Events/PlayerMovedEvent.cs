using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovedEvent : PlayerEvent, IEqualityComparer<PlayerMovedEvent>
{
    public Vector3 coordinates { get; }

    public PlayerMovedEvent(Vector3 newCoordinates)
    {
        coordinates = newCoordinates;
    }
    public bool Equals(PlayerMovedEvent x, PlayerMovedEvent y)
    {
        return x.coordinates == y.coordinates;
    }

    public int GetHashCode(PlayerMovedEvent obj)
    {
        throw new System.NotImplementedException();
    }
}
