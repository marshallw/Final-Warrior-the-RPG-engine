using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;

public class CharacterPresenter : MonoBehaviour
{
    protected event Action UpdateLocation;
    public Animator animator;
    public Sprite Portrait;
    protected bool _isMoving = false;

    protected ISubject<CharacterEvent> _characterEvents = new Subject<CharacterEvent>();
    protected IObserver<CharacterEvent> EventObserver => _characterEvents;

    void Start()
    {


    }

    // Update is called once per frame
    protected virtual void Update()
    {
        UpdateLocation?.Invoke();
    }

    public void Setup(Character actor, Map map)
    {
        actor.GetCoordinates += coordinates => transform.position = coordinates;
        Observable.EveryUpdate()
            .Select(deltaTime => Time.deltaTime)
            .Subscribe(deltaTime => actor.UpdateLocation(deltaTime));
        //UpdateLocation += actor.UpdateLocation;

        actor.SetCoordinates(transform.position);

        actor.OnChangeDirection += direction => 
            {
                animator.SetFloat("Direction", direction);
            };

        actor.CheckForCollision += map.Collision;
        actor.Portrait = Portrait;
    }

    protected void ToggleMovementAnimation(bool isMoving)
    {
        animator.SetBool("IsMoving", isMoving);
    }
}
