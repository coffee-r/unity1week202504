using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LogicToolkit;

[System.Serializable]
public class GameStateChangeAction : ActionComponent
{
    [SerializeField] GameMediator gameMediator;
    [SerializeField] GameState toGameState;

    // OnAction is called when the node is executed.
    protected override void OnAction()
    {
        gameMediator.State.Value = toGameState;
    }
}
