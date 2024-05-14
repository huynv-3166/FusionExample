using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public int speed = 5;
    public int speedForward = 10;

    [SerializeField] private Ball ballPref;
    [SerializeField] private PhysxBall physxBallPref;

    [Networked] private TickTimer delay { get; set; }

    private Vector3 forward = Vector3.forward;
    private NetworkCharacterController characterController;
    private float delayTime = 0.5f;
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

            if (data.direction.sqrMagnitude > 0)
            {
                forward = data.direction;
            }

            if (HasStateAuthority && delay.ExpiredOrNotRunning(Runner))
            {
                if (data.buttons.IsSet(NetworkInputData.MOUSEBUTTON0))
                {
                    delay = TickTimer.CreateFromSeconds(Runner, delayTime);
                    Runner.Spawn(
                        ballPref,
                        transform.position + forward,
                        Quaternion.LookRotation(forward),
                        Object.InputAuthority, 
                        (runner, o) =>
                        {
                            o.GetComponent<Ball>().Init();
                        });
                }
                else if (data.buttons.IsSet(NetworkInputData.MOUSEBUTTON1))
                {
                    delay = TickTimer.CreateFromSeconds(Runner, delayTime);
                    Runner.Spawn(
                        physxBallPref,
                        transform.position + forward,
                        Quaternion.LookRotation(forward),
                        Object.InputAuthority, 
                        (runner, o) =>
                        {
                            o.GetComponent<PhysxBall>().Init(speedForward * forward);
                        });
                }
            }
        }
    }
}
