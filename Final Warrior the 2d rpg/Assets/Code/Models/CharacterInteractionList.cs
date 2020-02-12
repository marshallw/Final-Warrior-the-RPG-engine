using Assets.Code.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using System;
using Assets.Code.Models.Events;

public class CharacterInteractionList : CharacterInteraction
{
    [Inject]
    public GameState _gameState { get; private set; }
    public CharacterInteraction[] Interactions = new CharacterInteraction[] { };
    public ISubject<CharacterInteractionEvent> _characterInteractionListEvents = new Subject<CharacterInteractionEvent>();
    private IObserver<CharacterInteractionEvent> CharacterInteractionListEventObserver => _characterInteractionListEvents;

    private int _currentInteraction = 0;

    [Inject]
    public void Initialize()
    {
        foreach (var interaction in Interactions)
        {
            interaction.Events.Subscribe(CharacterInteractionListEventObserver);
            
        }
    }
    public override void Interact()
    {
        
    }
}
