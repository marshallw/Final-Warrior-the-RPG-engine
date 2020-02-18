using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;
using System;
using Assets.Code.Models.Events;
using Assets.Code.Models;

public class CharacterInteractionTalk : CharacterInteraction
{

    [Inject]
    public GameState _gameState { get; private set; }
    [Inject]
    public Dialogue _dialogue { get; private set; }
    public string[] TalkText = new string[] { };
    public string Name;
    private ISubject<DialogueEvent> _dialogueEvents = new Subject<DialogueEvent>();
    public IObserver<DialogueEvent> DialogueEventsObserver => _dialogueEvents;

    [Inject]
    public void Initialize(Dialogue dialogue)
    {
        Events.Subscribe(dialogue.CharacterEventsObserver);
        _dialogue.DialogueEventsObservable.Subscribe(DialogueEventsObserver);
        _dialogueEvents.OfType<DialogueEvent, DialogueEndedEvent>().Subscribe(_ => EndInteraction());

    }

    public CharacterInteractionTalk()
    {
    }
    public override void Interact()
    {
            _characterInteractionEvents.OnNext(new CharacterInteractionTalkEvent(Name, TalkText));
    }

}
