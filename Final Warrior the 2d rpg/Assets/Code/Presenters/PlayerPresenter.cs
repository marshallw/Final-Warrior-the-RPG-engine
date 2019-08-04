using Assets.Code.Models.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class PlayerPresenter : CharacterPresenter
{
    [Inject]
    public Player player { get; private set; }

    [Inject]
    public void Initialize(Player player, Map map)
    {
        Setup(player, map);
        player.CharacterEvents.OfType<CharacterEvent, CharacterMoveStartEvent>().Subscribe(_ => ToggleMovementAnimation(true));
        player.CharacterEvents.OfType<CharacterEvent, CharacterMoveEndEvent>().Subscribe(_ => ToggleMovementAnimation(false));
    }
}
