﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Assets.Code.Models.Events;
using System;

public abstract class CharacterInteraction : MonoBehaviour
{
    protected ISubject<CharacterEvent> _characterEvents;
    protected ISubject<CharacterInteractionEvent> _characterInteractionEvents = new Subject<CharacterInteractionEvent>();
    public System.IObserver<CharacterEvent> CharacterEventObserver => _characterEvents;
    public IObservable<CharacterInteractionEvent> Events => _characterInteractionEvents;
    public IObserver<CharacterInteractionEvent> CharacterInteractionEventsOberver => _characterInteractionEvents;
    public abstract void Interact();
    public CharacterInteraction NextInteraction;
    public virtual CharacterInteraction GetNextInteraction { get => NextInteraction; }
    public virtual CharacterInteraction[] GetAllCharacterInteractions { get => new CharacterInteraction[] { NextInteraction }; }

    public void EndInteraction()
    {
        _characterInteractionEvents.OnNext(new CharacterInteractionEndedEvent());
    }
}
