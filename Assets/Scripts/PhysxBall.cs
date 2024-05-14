using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PhysxBall : NetworkBehaviour
{
    [Networked] private TickTimer life { get; set; }

    public float spawnTime = 2;
    public void Init(Vector3 forward)
    {
        life = TickTimer.CreateFromSeconds(Runner, spawnTime);
        GetComponent<Rigidbody>().velocity = forward;
    }
    public override void FixedUpdateNetwork()
    {
        if (life.Expired(Runner))
        {
            Runner.Despawn(Object);
        }
    }
}
