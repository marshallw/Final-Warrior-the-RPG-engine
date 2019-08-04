using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using System;
using Assets.Code.Models;

public class NPC : Character
{
    public AI movementAi { get; set; }
    public CharacterInteraction Interaction { get; set; }

    [Inject]
    public GameState _gameState { get; private set; }

    public NPC(): base()
    {
        _characterEvents
            .OfType<CharacterEvent, CharacterInteractedEvent>()
            .Where(e => new Vector3Int((int)e.TargetCoordinates.x, (int)e.TargetCoordinates.y, 0) == GetAbsoluteIntendedCoordinates()) //floortoint
            .Subscribe(_ => Interact(_.SourceCoordinates));

        Observable.EveryUpdate()
            .Where(x => _gameState.currentState == PossibleGameStates.Map)
            .Select(_ => Time.deltaTime)
            .Select(deltaTime => TimeSpan.FromSeconds(deltaTime))
            .Subscribe(deltaTimespan => GetNextMovement(deltaTimespan));
    }
    public void GetNextMovement(TimeSpan deltaTime)
    {
        if (movementAi != null && !Mover.IsMoving())
        {
            Vector3 directionToMove = movementAi.GetNextAction();
            if (directionToMove != Vector3.zero)
                Move(directionToMove);
        }
    }

    public void Interact(Vector3 sourceCoordinates)
    {
        SetDirection(GetDirection(sourceCoordinates - coordinates));
        Interaction?.Interact();
        Debug.Log("In interaction function");
    }
}
