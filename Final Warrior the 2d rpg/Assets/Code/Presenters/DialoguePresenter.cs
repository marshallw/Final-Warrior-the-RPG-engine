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
    public GameObject PortraitPanel;
    public Image Portrait;
    public Text NameText;
    public Text DialogueText;

    [Inject]
    public void Initialize(Dialogue dialogue)
    {
        dialogue.DialogueEventsObservable.OfType<DialogueEvent, PushDialogueEvent>().Subscribe(_ => PushDialogue(_.Name, _.DialogueText, _.Portrait));
        dialogue.DialogueEventsObservable.OfType<DialogueEvent, DialogueEndedEvent>().Subscribe(_ => EndDialogue());
    }

    public void Update()
    {
    }
    public void PushDialogue(string name, string dialogue, Sprite portrait)
    {
        NameText.text = name;
        DialogueText.text = dialogue;

        ToggleDialogueVisibility(true);

        if (portrait != null)
        {
            TogglePortraitVisibility(true);
            Portrait.sprite = portrait;
        }
        else
            TogglePortraitVisibility(false);
    }

    public void EndDialogue()
    {
        NameText.text = "";
        DialogueText.text = "";
        Portrait.sprite = null;
        ToggleDialogueVisibility(false);
    }

    public void ToggleDialogueVisibility(bool isVisible)
    {
        DialoguePanel.SetActive(isVisible);
    }

    public void TogglePortraitVisibility(bool isVisible)
    {
        
        PortraitPanel.SetActive(isVisible);
    }
}
