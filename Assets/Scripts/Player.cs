using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public int speed = 5;

    private NetworkCharacterController characterController;
    private void Awake()
    {
        characterController = GetComponent<NetworkCharacterController>();
    }
    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            data.direction.Normalize();
            characterController.Move(speed * data.direction * Runner.DeltaTime);
        }
    }
}
