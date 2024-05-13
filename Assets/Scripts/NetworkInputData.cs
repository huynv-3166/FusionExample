using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public struct NetworkInputData : INetworkInput
{
    public Vector3 direction;
    public const byte MOUSEBUTTON0 = 1;

    public NetworkButtons buttons;
}
