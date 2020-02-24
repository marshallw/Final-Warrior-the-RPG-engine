using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using Assets.Code.Models.Events;
using Zenject;
using Assets.Code.Models;

public class Dialogue
{
    [Inject]
    public GameState _gameState { get; private set; }
    protected ISubject<CharacterInteractionEvent> _characterEvents = new Subject<CharacterInteractionEvent>();
    protected ISubject<DialogueEvent> _dialogueEvents = new Subject<DialogueEvent>();
    public IObserver<CharacterInteractionEvent> CharacterEventsObserver => _characterEvents;
    public IObservable<CharacterInteractionEvent> CharacterEventsObservable => _characterEvents;
    public IObservable<DialogueEvent> DialogueEventsObservable => _dialogueEvents;

    private IDisposable _timerSubscription;
    private float _dialogueSpeed;

    private Queue<string> _dialogueText { get; set; }
    private string _name { get; set; }
    private Queue<char> _dialogueLeftQueue { get; set; }
    private Sprite portrait { get; set; }
    private string _dialogueFromQueue { get; set; }

    public Dialogue()
    {
        _characterEvents.OfType<CharacterInteractionEvent, CharacterInteractionTalkEvent>().Subscribe(_ => StartDialogue(_.Name, _.DialogueText, _.Portrait));

        _dialogueSpeed = 0.1f;
        _dialogueLeftQueue = new Queue<char>();
    }

    public void StartDialogue(string name, string[] DialogueText)
    {
        StartDialogue(name, DialogueText, null);
    }

    public void StartDialogue(string name, string [] DialogueText, Sprite Portrait)
    {
        _gameState.currentState = PossibleGameStates.Dialogue;

        _name = name;
        _dialogueText = new Queue<string>(DialogueText);
        portrait = Portrait;
        _dialogueEvents.OnNext(new DialogueStartEvent());

        PushNextDialogueString();
    }

    public void EndDialogue()
    {
        _name = "";
        _gameState.currentState = PossibleGameStates.Map;
        _dialogueEvents.OnNext(new DialogueEndedEvent());
    }

    public void AdvanceDialogue()
    {
        if (_dialogueLeftQueue.Count > 0)
        {
            GetRestOfDialogueFromQueue();
        }
        else if (_dialogueText.Count == 0)
        {
            EndDialogue();
        }
        else
        {
            PushNextDialogueString();
        }
    }

    public void PushNextDialogueString()
    {
        _dialogueLeftQueue = new Queue<char>(_dialogueText.Dequeue());
        _dialogueFromQueue = "";
        _timerSubscription = Observable.Interval(TimeSpan.FromMilliseconds(10)).Subscribe(_ => GetNextCharacterFromQueue());
    }

    private void GetNextCharacterFromQueue()
    {
        _dialogueFromQueue += _dialogueLeftQueue.Dequeue();
        
        if (_dialogueLeftQueue.Count == 0)
        {
            _timerSubscription.Dispose();
        }
        UpdateDialogue();
    }

    private void UpdateDialogue()
    {
        _dialogueEvents.OnNext(new PushDialogueEvent(_name, _dialogueFromQueue, portrait));
    }

    private void GetRestOfDialogueFromQueue()
    {
        _timerSubscription.Dispose();
        while(_dialogueLeftQueue.Count > 0)
        {
            _dialogueFromQueue += _dialogueLeftQueue.Dequeue();
        }
        UpdateDialogue();
    }
}
