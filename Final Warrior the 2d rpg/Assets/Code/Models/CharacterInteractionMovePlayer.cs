using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CharacterInteractionMovePlayer : CharacterInteraction
{
    [Inject]
    public Player player { get; private set; }

    public Vector3 warpCoordinates;
    public override void Interact()
    {
        player.SetCoordinates(warpCoordinates);
        EndInteraction();
    }
}
