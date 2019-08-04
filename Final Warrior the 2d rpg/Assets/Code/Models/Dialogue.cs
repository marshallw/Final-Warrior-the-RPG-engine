using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using Assets.Code.Models.Events;

public class Dialogue
{
    protected ISubject<CharacterInteractionEvent> _events = new Subject<CharacterInteractionEvent>();
    protected ISubject<DialogueEvent> _dialogueEvents = new Subject<DialogueEvent>();
    public IObserver<CharacterInteractionEvent> EventObserver => _events;
    public IObservable<CharacterInteractionEvent> EventObservable => _events;
    public IObservable<DialogueEvent> DialogueEvents => _dialogueEvents; 

    public Dialogue()
    {
        _events.OfType<CharacterInteractionEvent, CharacterInteractionTalkEvent>().Subscribe(_ => SendDialogue(_.Name, _.DialogueText));
        _events.OfType<CharacterInteractionEvent, CharacterInteractionEndedEvent>().Subscribe(_ => EndDialogue());
    }

    public void SendDialogue(string name, string DialogueText)
    {
        _dialogueEvents.OnNext(new PushDialogueEvent(name, DialogueText));
    }

    public void EndDialogue()
    {
        _dialogueEvents.OnNext(new DialogueEndedEvent());
    }
}
