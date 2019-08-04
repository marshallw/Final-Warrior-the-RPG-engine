using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.Tilemaps;
using Zenject;

public class CameraHandler : MonoBehaviour
{
    [Inject]
    public Player player { get; private set; }
    [Inject]
    public Map map { get; private set; }

    public PixelPerfectCamera pixelPerfectCamera;

    private int endX;
    private int endY;

    private int screenX;
    private int screenY;

    Vector2 cameraSize;

    private int tileSize;

    private Vector3 mainActor = Vector3.zero;
    
    [Inject]
    public void Initialize()
    {
        player.GetCoordinates += coordinates => mainActor = coordinates;
    }

    void Start()
    {
        tileSize = pixelPerfectCamera.assetsPPU;
        //Rect cameraSize = Camera.main.pixelRect;
        cameraSize = new Vector2(pixelPerfectCamera.refResolutionX / tileSize, pixelPerfectCamera.refResolutionY / tileSize);

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void LateUpdate()
    {
        Vector3 coordinates = new Vector3(mainActor.x, mainActor.y - 0.5f, -10);

        if (coordinates.x < map.Bounds.xMin + cameraSize.x/2)
            coordinates.x = map.Bounds.xMin + cameraSize.x/2;
        if (coordinates.y < map.Bounds.yMin + cameraSize.y/2)
            coordinates.y = map.Bounds.yMin + cameraSize.y/2;

        if (coordinates.x > map.Bounds.xMax - cameraSize.x/2)
            coordinates.x = map.Bounds.xMax - cameraSize.x/2;
        
        if (coordinates.y > map.Bounds.yMax - cameraSize.y/2)
            coordinates.y = map.Bounds.yMax - cameraSize.y/2;

        transform.position = coordinates;
    }
}
