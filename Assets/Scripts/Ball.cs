using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : NetworkBehaviour
{
    public int speed = 5;
    public float spawnTime = 5;

    [Networked] private TickTimer life { get; set; }
    public override void FixedUpdateNetwork()
    {
        if (life.Expired(Runner))
        {
            Runner.Despawn(Object);
        }
        else
        {
            transform.position += speed * transform.forward * Runner.DeltaTime;
        }
    }
    public void Init()
    {
        life = TickTimer.CreateFromSeconds(Runner, spawnTime);
    }
}
