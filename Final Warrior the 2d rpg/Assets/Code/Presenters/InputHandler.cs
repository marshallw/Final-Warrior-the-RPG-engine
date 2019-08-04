using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using UniRx.Triggers;
using System;
using Assets.Code.Models;

public class InputHandler : MonoBehaviour
{

    // Update is called once per frame
    [Inject]
    public Player player { get; private set; }
    [Inject]
    public GameState _gameState { get; private set; }

   // ISubject<>

    [Inject]
    public void Initialize()
    {
        
    }

    public void Start()
    {
        // var inputHandler = this.UpdateAsObservable().Subscribe()
        Observable.EveryUpdate()
            .Where(x => _gameState.currentState == PossibleGameStates.Map)
            .Where(x => Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            .Select(deltaTime => Time.deltaTime)
            .Subscribe(deltaTime => UpdateMovement());

        Observable.EveryUpdate()
            .Where(x => Input.GetButtonDown("Submit"))
            .Subscribe(_ => player.Interact());
    }

    private void UpdateMovement()
    {
       Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0);
       if (movement == Vector3.zero)
           movement += new Vector3(0, Input.GetAxisRaw("Vertical"));
                   
        if (movement != Vector3.zero)
        {
            player.Move(movement);
        }
    }
}
