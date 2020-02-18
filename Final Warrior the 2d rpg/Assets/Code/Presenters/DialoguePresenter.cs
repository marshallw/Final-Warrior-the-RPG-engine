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

    [Inject]
    public void Initialize(Dialogue dialogue)
    {
        dialogue.DialogueEventsObservable.OfType<DialogueEvent, PushDialogueEvent>().Subscribe(_ => PushDialogue(_.Name, _.DialogueText));
        dialogue.DialogueEventsObservable.OfType<DialogueEvent, DialogueEndedEvent>().Subscribe(_ => EndDialogue());
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
        DialogueText.text = dialogue;

        ToggleDialogueVisibility(true);
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
