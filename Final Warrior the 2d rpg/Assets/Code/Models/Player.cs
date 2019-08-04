using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

public class Player : Character
{

    public Action<Vector3> OnInteract;
    public void Interact()
    {
        if (!Mover.IsMoving())
        {
            Vector3 coordinatesToInteract = new Vector3(coordinates.x, coordinates.y, 0);
        
            switch (direction)
            {
                case DirectionEnum.Up:
                    coordinatesToInteract.y++;
                    break;
                case DirectionEnum.Right:
                    coordinatesToInteract.x++;
                    break;
                case DirectionEnum.Down:
                    coordinatesToInteract.y--;
                    break;
                case DirectionEnum.Left:
                    coordinatesToInteract.x--;
                    break;
            }

            _characterEvents.OnNext(new CharacterInteractedEvent(coordinates, coordinatesToInteract));
        }
    }
} 