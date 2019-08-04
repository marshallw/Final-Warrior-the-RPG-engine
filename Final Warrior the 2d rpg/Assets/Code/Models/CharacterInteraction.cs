using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public abstract class CharacterInteraction: MonoBehaviour
{
    private ISubject<CharacterEvent> _events;
    public System.IObserver<CharacterEvent> CharacterEventObserver => _events;
    public abstract void Interact();
}
