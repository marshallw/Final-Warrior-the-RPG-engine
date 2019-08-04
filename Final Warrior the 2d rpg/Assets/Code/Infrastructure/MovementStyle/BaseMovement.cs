using UnityEngine;

public abstract class BaseMovement 
{
    protected int _tileSize;
    protected float _screenPixelsPerWindowPixel;
    protected Vector3 _lastDirectionMoved;
    protected float _minimumStep = 1f / 60f;

    public BaseMovement(int tileSize)
    {
        _tileSize = tileSize;
        _screenPixelsPerWindowPixel = 1f / (float)_tileSize;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public abstract Vector3 UpdateLocationDelta(float deltaTime, float speed);
    
    
    /// <summary>
    /// Called whenever a direction is chosen to move in.
    /// </summary>
    /// <param name="start">starting location</param>
    /// <param name="direction">Direction to move the coordinates</param>
    public virtual void Move(Vector3 direction)
    {
        _lastDirectionMoved = direction;
    }

    public virtual Vector3Int GetAbsoluteIntendedCoordinates(Vector3 coordinates)
    {
        Vector3Int absCoordinates = new Vector3Int((int)coordinates.x, (int)coordinates.y, 0);

        return absCoordinates;
    }

    public virtual Vector3Int GetAbsoluteCurrentCoordinates(Vector3 coordinates)
    {
        return GetAbsoluteIntendedCoordinates(coordinates);
    }

    public abstract Vector3 CancelMove();

    public abstract bool IsMoving();
    
}
