using Assets.Code.Models.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;

public class NPCPresenter : CharacterPresenter
{
    [Inject]
    public Player player { get; private set; }

    [Inject]
    public NPC npc { get; private set; }

    public AI movementAi;
    public CharacterInteraction characterInteraction;

    [Inject]
    public void Initialize(Map map, NPCtoNPCCollision npcCollision)
    {
        Setup(npc, map);
        npc.CharacterEvents.OfType<CharacterEvent, CharacterMoveStartEvent>().Subscribe(_ => ToggleMovementAnimation(true));
        npc.CharacterEvents.OfType<CharacterEvent, CharacterMoveEndEvent>().Subscribe(_ => ToggleMovementAnimation(false));
        player.CheckForCollision += coordinates => { return npc.Collision(coordinates); };
        npc.CheckForCollision += coordinates => { return player.Collision(coordinates); };

        if (movementAi != null)
        {
            npc.movementAi = movementAi;
        }
        if (characterInteraction != null)
        {
            npc.Interaction = characterInteraction;
            npc.InitializeInteractions();
        }

        npcCollision.AddNpctoNpcCollision(npc);

        player.OnInteract += coordinates => npc.Interact(coordinates);
    }
}
