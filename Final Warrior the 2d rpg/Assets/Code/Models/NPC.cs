using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using System;
using Assets.Code.Models;
using Assets.Code.Models.Events;
using System.Threading;

public class NPC : Character
{
    public AI movementAi { get; set; }
    public CharacterInteraction Interaction { get; set; }
    private CharacterInteraction _currentInteraction { get; set; }
    private Subject<CharacterInteractionEvent> _characterInteractionEvents = new Subject<CharacterInteractionEvent>();
    private IObserver<CharacterInteractionEvent> _interactionEventObserver => _characterInteractionEvents;


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

        if (_currentInteraction == null)
            _currentInteraction = Interaction;

        _currentInteraction?.Interact();
    }

    public void GotoToNextInteraction()
    {
        if (_currentInteraction == null)
        {
            _currentInteraction = Interaction;
        }
        else
        {
            _currentInteraction = _currentInteraction.GetNextInteraction;
        }

        _currentInteraction?.Interact();
    }

    public void InitializeInteractions()
    {
        Initializeinteraction(Interaction);
        _currentInteraction = Interaction;
        _characterInteractionEvents.OfType<CharacterInteractionEvent, CharacterInteractionEndedEvent>()
                          .Subscribe(_ => GotoToNextInteraction());
    }

    public void Initializeinteraction(CharacterInteraction interaction)
    {
        if (interaction != null)
        {
            SubscribeToInteractionEvents(interaction);
            foreach (var nextInteraction in interaction.GetAllCharacterInteractions)
            {
                Initializeinteraction(nextInteraction);
            }
        }
    }
    public void SubscribeToInteractionEvents(CharacterInteraction interaction)
    {
        if (interaction != null)
        {
            interaction.Events.Subscribe(_interactionEventObserver);
        }
    }
}
