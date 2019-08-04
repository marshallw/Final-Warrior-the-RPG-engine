using UnityEngine;
public class PixelbyPixelMovement : BaseMovement
{
    public PixelbyPixelMovement(int tileSize) : base(tileSize) { }

    public override Vector3 CancelMove()
    {
        Vector3 result = _lastDirectionMoved * -1 * _screenPixelsPerWindowPixel;
        _lastDirectionMoved = Vector3.zero;

        return result;
    }

    public override Vector3 UpdateLocationDelta(float deltaTime, float speed)
    {
        Vector3 result = Vector3.zero;
        if (_lastDirectionMoved == Vector3.zero)
        {
            result = _lastDirectionMoved * _screenPixelsPerWindowPixel;
            _lastDirectionMoved = Vector3.zero;
        }
        return result;
    }

    public override bool IsMoving()
    {
        return false;
    }
}
