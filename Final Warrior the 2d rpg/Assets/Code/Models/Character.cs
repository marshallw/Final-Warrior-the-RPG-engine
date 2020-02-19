using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UniRx;
using Assets.Code.Models.Events;
using Assets.Code.Models;

public class Character
{
    protected Vector3 coordinates { get; set; }
    protected BaseMovement Mover { get; set; }
    protected bool _isMoving { get; set; }
    protected DirectionEnum direction { get; set; }
    private static float movementSpeed = 4.0f;

    public event Action<Vector3> GetCoordinates;
    public event Func<Vector3, bool> CheckForCollision;
    public event Action<int> OnChangeDirection;
    private Inventory _inventory { get; set; }
    public Inventory Inventory { get { return _inventory; } }

    protected ISubject<CharacterEvent> _characterEvents = new Subject<CharacterEvent>();
    public IObservable<CharacterEvent> CharacterEvents => _characterEvents;
    public IObserver<CharacterEvent> ObservableEvents => _characterEvents;

    public Character()
    {
        _isMoving = false;
        // TODO: find a better way to initialize this plz
        Mover = new PixelbyTileMovement(16);
        coordinates = new Vector3(0f, 0f);
    }

    /// <summary>
    /// Anytime the characters direction is changed, call its listeners.
    /// </summary>
    /// <param name="direction"></param>
    public void SetDirection(int direction)
    {
        OnChangeDirection?.Invoke(direction);
    }

    /// <summary>
    /// Teleport the character to this location.
    /// </summary>
    /// <param name="newCoordinates"></param>
    public void SetCoordinates(Vector3 newCoordinates)
    {
        coordinates = newCoordinates;
    }

    /// <summary>
    /// Determine whether the character can move in the direction chosen, then set it up to move in that direction.
    /// </summary>
    /// <param name="vector"></param>
    public void Move(Vector3 vector)
    {
        if (!Mover.IsMoving())
        {
            if (vector != Vector3.zero)
            {
                SetDirection(GetDirection(vector));

                var delList = CheckForCollision?.GetInvocationList().Select(x => (bool)x.DynamicInvoke(coordinates + vector));
                bool collision = CollisionDetected(delList);

                if (!collision)
                {
                    if (!_isMoving)
                    {
                        _isMoving = !_isMoving;
                        _characterEvents.OnNext(new CharacterMoveStartEvent());
                    }
                    Mover.Move(vector);
                }
            }

        }
    }

    private bool CollisionDetected(IEnumerable<bool> collisionList)
    {
        bool result = false;
        if (collisionList != null)
        {
            foreach (bool del in collisionList)
            {
                if (del == true)
                    result = del;
            }
        }

        return result;
    }

    protected DirectionEnum GetDirection(Vector3 coordinatesDelta)
    {
        DirectionEnum result = DirectionEnum.Down;
            if (coordinatesDelta.x < 0f)
            {
                result = DirectionEnum.Left;
            }
            else if (coordinatesDelta.x > 0f)
            {
                result = DirectionEnum.Right;
            }
            else if (coordinatesDelta.y < 0f)
            {
                result = DirectionEnum.Down;
            }
            else if (coordinatesDelta.y > 0f)
            {
                result = DirectionEnum.Up;
            }

        return result;
    }

    protected void SetDirection(DirectionEnum dir)
    {
        direction = dir;
        OnChangeDirection?.Invoke((int)direction);
    }

    /// <summary>
    /// Move the Character, and call any listeners with the new coordinates.
    /// </summary>
    public void UpdateLocation(float deltaTime)
    {
        Vector3 oldCoordinates = coordinates;
        coordinates += Mover.UpdateLocationDelta(deltaTime, movementSpeed);

        if (oldCoordinates == coordinates && _isMoving)
        {
            _isMoving = !_isMoving;
            _characterEvents.OnNext(new CharacterMoveEndEvent());
        }
        else
        {
            GetCoordinates?.Invoke(coordinates);
        }
    }

    /// <summary>
    /// Get the characters exact map coordinates (int, int)
    /// </summary>
    /// <returns></returns>
    protected Vector3Int GetAbsoluteIntendedCoordinates()
    {
        return Mover.GetAbsoluteIntendedCoordinates(coordinates);
    }

    protected Vector3Int GetAbsoluteCurrentCoordinates()
    {
        return Mover.GetAbsoluteCurrentCoordinates(coordinates);
    }

    /// <summary>
    /// Determine whether the anything collides with this character.
    /// </summary>
    /// <param name="coordinates"></param>
    /// <returns></returns>
    public bool Collision(Vector3 coordinatesToCheck)
    {
        Vector3Int objectCoordinates = new Vector3Int((int)coordinatesToCheck.x, (int)coordinatesToCheck.y, 0);
        bool result = false;

        result =  (objectCoordinates == GetAbsoluteIntendedCoordinates() || objectCoordinates == GetAbsoluteCurrentCoordinates());
        return result;
    }
}
