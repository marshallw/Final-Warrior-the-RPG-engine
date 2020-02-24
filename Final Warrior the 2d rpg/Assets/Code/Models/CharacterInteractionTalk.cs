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
    public string Name;
    public Sprite Portrait;
    public string[] TalkText = new string[] { };
    private ISubject<DialogueEvent> _dialogueEvents = new Subject<DialogueEvent>();
    public IObserver<DialogueEvent> DialogueEventsObserver => _dialogueEvents;
    private IDisposable _dialogueDisposable;
    private IDisposable _dialogueDisposable2;

    [Inject]
    public void Initialize(Dialogue dialogue)
    {
        Events.Subscribe(dialogue.CharacterEventsObserver);
    }

    public CharacterInteractionTalk()
    {
    }

    private void ConnectToDialogueEvents()
    {
        _dialogueDisposable = _dialogue.DialogueEventsObservable.Subscribe(DialogueEventsObserver);
        _dialogueDisposable2 = _dialogueEvents.OfType<DialogueEvent, DialogueEndedEvent>().Subscribe(_ => EndCharacterTalkInteraction());
    }

    private void EndCharacterTalkInteraction()
    {
        _dialogueDisposable.Dispose();
        _dialogueDisposable2.Dispose();
        EndInteraction();
    }
    public override void Interact()
    {
        ConnectToDialogueEvents();
        _characterInteractionEvents.OnNext(new CharacterInteractionTalkEvent(Name, TalkText, Portrait));
    }

}
