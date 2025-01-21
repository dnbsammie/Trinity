using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[Serializable]
public class Int2Event : UnityEvent<int, int> { }
public class EventManager : MonoBehaviour
{
    #region Singleton
    public static EventManager current;

    public void Awake()
    {
        if(current == null) { current = this; } else if (current != this) {Destroy(this); }
    }
    #endregion
    public Int2Event updateBulletsEvent = new Int2Event();
    public UnityEvent NewGunEvent = new UnityEvent();
}