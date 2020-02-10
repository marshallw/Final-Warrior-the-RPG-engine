using Assets.Code.Models.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public abstract class CharacterInteractionLinked : CharacterInteraction
{
    public CharacterInteraction NextInteraction;
    protected ISubject<CharacterInteractionEvent> _linkedEvents = new Subject<CharacterInteractionEvent>();
    public IObserver<CharacterInteractionEvent> EventObserver => _linkedEvents;

    public void SetupLinkedInteraction()
    {
        if (NextInteraction != null)
        {
            Events.Subscribe(EventObserver);
            _linkedEvents.OfType<CharacterInteractionEvent, CharacterInteractionEndedEvent>().Subscribe(_ => NextInteraction.Interact());
        }
    }
}
