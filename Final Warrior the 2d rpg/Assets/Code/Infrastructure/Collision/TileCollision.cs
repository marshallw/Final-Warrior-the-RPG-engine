using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;


public class TileCollision
{ 
    [Inject]
    public Player Player { get; private set; }
    [Inject]
    public Tilemap CollisionTilemap { get; private set; }
    public bool IsCollided(Vector3 coordinates1, Vector3 coordinates1Delta, Vector3 coordinates3)
    {
        coordinates1 += coordinates1Delta;
        return false;
    }

    public bool CollisionWithTerrain(Vector3 coordinates, Vector3 direction)
    {
        Vector3 finalDestination = coordinates + direction;
        Vector3Int destinationTile = new Vector3Int((int)finalDestination.x, (int)finalDestination.y + 1, 0);

        return CollisionTilemap.HasTile(destinationTile);
    }
}
