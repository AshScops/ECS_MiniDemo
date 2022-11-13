using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpdateHealthEvent : UnityEvent<int>{ }
public class UpdateScoreEvent : UnityEvent<int> { }
public class ShowResultEvent : UnityEvent<int> { }

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            return;

        updateHealthEvent = new UpdateHealthEvent();
        updateScoreEvent = new UpdateScoreEvent();
        showResultEvent = new ShowResultEvent();
    }

    public UpdateHealthEvent updateHealthEvent;
    public UpdateScoreEvent updateScoreEvent;
    public ShowResultEvent showResultEvent;

}
