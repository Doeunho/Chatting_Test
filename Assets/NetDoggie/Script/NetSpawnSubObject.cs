using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Experimental.GlobalIllumination;

public class NetSpawnSubObject : NetworkBehaviour
{
    public float _destroyAfter = 2.0f;
    public float _force = 1000;

    public Rigidbody Rigidbody_SubObj;
}
