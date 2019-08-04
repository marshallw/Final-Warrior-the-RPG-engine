﻿using System.Collections;
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
    public string[] TalkText = new string[] { };
    public string Name;
    private Queue<string> _talkText = new Queue<string>();

    private ISubject<DialogueEvent> _events = new Subject<DialogueEvent>();
    private ISubject<CharacterInteractionEvent> _characterInteractionEvents = new Subject<CharacterInteractionEvent>();
    public IObserver<DialogueEvent> EventsObserver => _events;
    public IObservable<CharacterInteractionEvent> Events => _characterInteractionEvents;

    [Inject]
    public void Initialize(Dialogue dialogue)
    {
        Events.Subscribe(dialogue.EventObserver);
        InitializeQueue();
    }
    
    private void InitializeQueue()
    {

        if (TalkText != null)
        {
            _talkText = new Queue<string>(TalkText);
        }
    }

    public CharacterInteractionTalk()
    {
    }
    public override void Interact()
    {
        if (_talkText.Count > 0)
        {
            string nextDialogue = _talkText.Dequeue();
            _characterInteractionEvents.OnNext(new CharacterInteractionTalkEvent(Name, nextDialogue));
            _gameState.currentState = PossibleGameStates.Dialogue;
            Debug.Log(nextDialogue);
        }
        else
        {
            InitializeQueue();
            _characterInteractionEvents.OnNext(new CharacterInteractionEndedEvent());
            _gameState.currentState = PossibleGameStates.Map;
        }
        
    }
}