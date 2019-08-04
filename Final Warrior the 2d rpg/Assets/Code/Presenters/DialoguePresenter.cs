using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.UI;
using UniRx;
using System;

public class DialoguePresenter : MonoBehaviour
{
    public GameObject DialoguePanel;
    public Text NameText;
    public Text DialogueText;
    private Queue<char> _restOfDialogue = new Queue<char>();
    private float _dialogueSpeed;
    private IDisposable _timerSubscription;

    [Inject]
    public void Initialize(Dialogue dialogue)
    {
        dialogue.DialogueEvents.OfType<DialogueEvent, PushDialogueEvent>().Subscribe(_ => PushDialogue(_.Name, _.DialogueText));
        dialogue.DialogueEvents.OfType<DialogueEvent, DialogueEndedEvent>().Subscribe(_ => EndDialogue());
        _dialogueSpeed = 0.1f;
    }

    public void Update()
    {
    }
    public void PushDialogue(string name, string dialogue)
    {
        if (string.IsNullOrEmpty(name))
        {
            name = "???";
        }
        NameText.text = name;
        _restOfDialogue = new Queue<char>(dialogue);
        DialogueText.text = "";

        _timerSubscription = Observable.Interval(TimeSpan.FromMilliseconds(10)).Subscribe(_ => UpdateDialogue());
        ToggleDialogueVisibility(true);
    }

    private void UpdateDialogue()
    {
        if (_restOfDialogue.Count > 0)
        {
            DialogueText.text += _restOfDialogue.Dequeue();
        }
        if (_restOfDialogue.Count == 0)
        {
            _timerSubscription.Dispose();
        }

    }

    public void EndDialogue()
    {
        ToggleDialogueVisibility(false);
    }

    public void ToggleDialogueVisibility(bool isVisible)
    {
        DialoguePanel.SetActive(isVisible);
    }
}
