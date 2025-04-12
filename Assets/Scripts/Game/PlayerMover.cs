using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LogicToolkit;

[System.Serializable]
public class PlayerMover : ServiceComponent, IUpdateReceiver
{
    PlayerInputProvider inputProvider;
    Transform transform;
    [SerializeField] float moveSpeed;

    // OnActivated is called when activated.
    protected override void OnActivated()
    {
        inputProvider = Player.GetComponent<PlayerInputProvider>();
        transform = Player.transform;
    }

    // OnUpdate is called once per frame.
    public void OnUpdate()
    {
        transform.position += inputProvider.MoveVector3() * moveSpeed;
    }
    
    // OnDeactivated is called when it is deactivated.
    protected override void OnDeactivated()
    {
        
    }
}
