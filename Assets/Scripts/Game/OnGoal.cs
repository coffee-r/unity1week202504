using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LogicToolkit;
using R3;
using System;

[System.Serializable]
public class OnGoal : EventComponent
{
    [SerializeField] GameMediator gameMediator;
    IDisposable _subscription;
    
    // OnActivated is called when activated.
    protected override void OnActivated()
    {
        // Subscribe to some event and call OnEvent() from your callback.
        _subscription = gameMediator.OnGoalReached.Subscribe(_ => OnEvent());
    }

    // OnDeactivated is called when it is deactivated.
    protected override void OnDeactivated()
    {
        // Unsubscribe from some event
        _subscription?.Dispose();
        _subscription = null;
    }
}
