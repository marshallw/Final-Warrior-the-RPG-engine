using UnityEngine;

public class PixelbyTileMovement : BaseMovement
{
    private int _updatesRemaining = 0;
    private float _totalDistanceTraveled = 1.0f;
    private float _lastDistancePassed = 0.0f;

    public PixelbyTileMovement(int tileSize) : base(tileSize) { }

    public override Vector3 CancelMove()
    {
        _lastDirectionMoved = _lastDirectionMoved * -1;
        _updatesRemaining = _tileSize - _updatesRemaining;
        return Vector3.zero;
    }

    public override void Move(Vector3 direction)
    {
        if (_totalDistanceTraveled == 1.0)
        {
            base.Move(direction);
            _updatesRemaining = _tileSize;
            _totalDistanceTraveled = 0;
            _lastDistancePassed = 0;
        }

    }

    public override Vector3 UpdateLocationDelta(float deltaTime, float speed)
    {
        Vector3 result = Vector3.zero;
        float coordinatesToReturn = 0;

        if (_totalDistanceTraveled < 1.0)
        {
            _updatesRemaining -= (int)((deltaTime * speed) / _tileSize);
            var absoluteDistance = (deltaTime * speed);
            if (_totalDistanceTraveled + absoluteDistance > 1.0)
            {
                _totalDistanceTraveled = 1.0f;
            }
            else
            {
                _totalDistanceTraveled += absoluteDistance;
            }
            coordinatesToReturn = ((int)((_totalDistanceTraveled - _lastDistancePassed) / _screenPixelsPerWindowPixel)) * _screenPixelsPerWindowPixel;
            _lastDistancePassed += coordinatesToReturn;

            result = _lastDirectionMoved * coordinatesToReturn;
            if (_lastDistancePassed >= 1.0f)
            {
                _lastDistancePassed = 0;
                _lastDirectionMoved = Vector3.zero;
            }
        }

        return result;
    }

    public override Vector3Int GetAbsoluteIntendedCoordinates(Vector3 coordinates)
    {
        //coordinates += _lastDirectionMoved * (_updatesRemaining/ (float)_tileSize);
        coordinates += _lastDirectionMoved * (1.0f - _lastDistancePassed);
        return base.GetAbsoluteIntendedCoordinates(coordinates);
    }

    public override Vector3Int GetAbsoluteCurrentCoordinates(Vector3 coordinates)
    {
        // coordinates += -((_tileSize - _updatesRemaining) / (float)_tileSize) * _lastDirectionMoved;
        coordinates += (-_lastDistancePassed) * _lastDirectionMoved;
        return base.GetAbsoluteIntendedCoordinates(coordinates);
    }

    public override bool IsMoving()
    {
        if (_totalDistanceTraveled == 1.0)
        {
            return false;
        }
        return true;
    }
}
