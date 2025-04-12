using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LogicToolkit;
using System;
using R3;

[System.Serializable]
public class OnGameStateChange : EventComponent
{
    [SerializeField] GameMediator gameMediator;
    [SerializeField] GameState onState;

    IDisposable _subscription;

    // OnActivated is called when activated.
    protected override void OnActivated()
    {
        // Subscribe to some event and call OnEvent() from your callback.
        _subscription = gameMediator
                        .State
                        .Where(x => x == onState)
                        .Subscribe(_ => OnEvent());
    }

    // OnDeactivated is called when it is deactivated.
    protected override void OnDeactivated()
    {
        // Unsubscribe from some event
         // Unsubscribe from some event
        _subscription?.Dispose();
        _subscription = null;
    }
}
