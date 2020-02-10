using Assets.Code.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CharacterInteractionList : CharacterInteraction
{
    [Inject]
    public GameState _gameState { get; private set; }
    public CharacterInteraction[] Interactions = new CharacterInteraction[] { };
    public override void Interact()
    {
        
    }
}
