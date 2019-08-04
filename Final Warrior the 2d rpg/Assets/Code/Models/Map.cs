using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Map
{
    public BoundsInt Bounds { get; private set; }

    public event Func<Vector3,bool> TileExists;

    public bool Collision(Vector3 coordinates)
    {
        var collisionChecks = TileExists?.GetInvocationList().Select(x => (bool)x.DynamicInvoke(coordinates));
        bool result = false;
        foreach (var collisionCheck in collisionChecks)
        {
            if (collisionCheck)
                result = true;
        }
        return result;
    }

    public void SetBounds(BoundsInt bounds)
    {
        Bounds = bounds;
    }

}
