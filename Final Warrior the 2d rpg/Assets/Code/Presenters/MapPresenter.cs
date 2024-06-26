﻿using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class MapPresenter : MonoBehaviour
{
    public Grid grid;
    private List<Tilemap> _tilemaps;
    private List<Tilemap> _collision;
    private List<Tilemap> _water;
    private Tilemap _baseMap;

    // Start is called before the first frame update
    void Start()
    {
    }

    [Inject]
    public void Initialize(Map map)
    {
        _tilemaps = grid.GetComponentsInChildren<Tilemap>().ToList();
        _collision = _tilemaps.Where(x => x.tag == "collision").ToList();
        _baseMap = _tilemaps.SingleOrDefault(x => x.tag == "base layer");
        _water = _tilemaps.Where(x => x.tag == "water").ToList();

        BoundsInt entireBounds = _baseMap.cellBounds;

        foreach (var tileMap in _tilemaps)
        {
            entireBounds.xMin = entireBounds.xMin < tileMap.cellBounds.xMin ? entireBounds.xMin : tileMap.cellBounds.xMin;
            entireBounds.xMax = entireBounds.xMax > tileMap.cellBounds.xMax ? entireBounds.xMax : tileMap.cellBounds.xMax;

            entireBounds.yMin = entireBounds.yMin < tileMap.cellBounds.yMin ? entireBounds.yMin : tileMap.cellBounds.yMin;
            entireBounds.yMax = entireBounds.yMax > tileMap.cellBounds.yMax ? entireBounds.yMax : tileMap.cellBounds.yMax;
        }

        map.TileExists += IsCollision;
        map.TileExists += IsOutOfBounds;
        map.SetBounds(entireBounds);
    }


    private bool IsOutOfBounds(Vector3 coordinates)
    {
        BoundsInt mapBounds = _baseMap.cellBounds;
        Vector3Int coordinatesInt = new Vector3Int((int)coordinates.x, (int)coordinates.y - 1, 0);

        if (mapBounds.Contains(coordinatesInt))
        {
            return false;
        }
        return true;
    }

    private bool IsCollision(Vector3 coordinates)
    {
        //coordinates += direction;
        Vector3Int v3int = new Vector3Int((int)coordinates.x, (int)coordinates.y-1, (int)coordinates.z);
        bool result = false;

        // handling collision with collision layer
        result = _collision.Any(layer => layer.HasTile(v3int));

        // handling water collision (allow to pass if there is a tile there in basemap, or if they have a special "walkwater" flag, using a specific spriteset or something)
        result = result? result: result == false && _water.Any(layer => layer.HasTile(v3int)) && !_baseMap.HasTile(v3int) ? true: false;

        return result;
    }
}
